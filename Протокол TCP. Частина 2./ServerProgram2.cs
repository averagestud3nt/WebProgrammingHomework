using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;

class Program
{

    static async Task Main()
    {
        int port = 8888;
        Server server = new Server();
        await server.StartServer(IPAddress.Any, port);
    }
}

public class Server
{
    protected List<Order> orders = new List<Order>();
    List<Client> currentClients = new List<Client>();

    TcpListener tcpListener;

    public async Task StartServer(IPAddress iPAddress, int port)
    {

        try
        {
            tcpListener = new TcpListener(iPAddress, port);

            tcpListener.Start();

            Task.Run(() => OrderOperationSystem());

            while (true)
            {
                Client connectedClient = new Client(await tcpListener.AcceptTcpClientAsync(), this);
                currentClients.Add(connectedClient);

                Task.Run(connectedClient.Process);
            }
        }
        finally
        {
            DisconnectAll();
        }

    }

    async Task Log(string message)
    {
        Console.WriteLine(message);
    }

    async Task DisconnectClient(string id)
    {
        Client client = currentClients.FirstOrDefault(e => e.Id == id);

        if (client != null) currentClients.Remove(client);

        client?.Close();
    }

    async Task RemoveClientsOrders(List<Order> clientsOrders)
    {
        orders.RemoveAll(e => clientsOrders.Contains(e));
    }

    async Task DisconnectAll()
    {
        Log($"Server closed at: {DateTime.Now}");
        foreach (var client in currentClients)
        {
            client.Close();
        }
        tcpListener.Stop();
    }

    async Task OrderOperationSystem()
    {
        while (true)
        {
            Thread.Sleep(3000);

            if (orders.Count() > 0)
            {
                int currentWorkedOnOrders = orders.Count(e => e.readyness == Readyness.WorkedOn);

                List<Order> processedOrders = orders.Where(e => e.readyness == Readyness.WorkedOn).ToList();

                processedOrders.AddRange(orders.Where(e => e.readyness == Readyness.NotWorkedOn).OrderBy(e => e.timeLeft).Take(((3 - currentWorkedOnOrders > 0)) ? (3 - currentWorkedOnOrders) : 0).ToList());

                foreach(var item in processedOrders)
                {
                    if (item.timeLeft == 0)
                    {
                        Order order = processedOrders[processedOrders.FindIndex(e => e.id == item.id)];

                        order.readyness = Readyness.Ready;

                        Client client = currentClients.Where(e => e.clientsOrders.Select(e=>e.id).Contains(order.id)).FirstOrDefault();

                        orders[orders.FindIndex(e => e.id == order.id)] = order;
                        client.clientsOrders[client.clientsOrders.FindIndex(e => e.id == order.id)]= order;

                        client.SendNotification();
                    }
                    else
                    {
                        Order order = processedOrders[processedOrders.FindIndex(e => e.id == item.id)];

                        if (order.readyness == Readyness.NotWorkedOn)
                        {
                            order.readyness = Readyness.WorkedOn;
                        }

                        order.timeLeft = (order.timeLeft - 1 > 0) ? (order.timeLeft - 1) : 0;

                        Client client = currentClients.Where(e => e.clientsOrders.Select(e => e.id).Contains(order.id)).FirstOrDefault();

                        orders[orders.FindIndex(e => e.id == order.id)] = order;
                        client.clientsOrders[client.clientsOrders.FindIndex(e => e.id == order.id)] = order;
                    }
                }
            }
        }

       
    }

    class Client
    {
        protected internal List<Order> clientsOrders = new List<Order>();

        protected internal string Id = Guid.NewGuid().ToString();
        TcpClient client { get; set; }

        Server server { get; set; }

        StreamWriter writer;
        StreamReader reader;

        public Client(TcpClient tcpClient, Server tcpServer)
        {
            client = tcpClient;

            server = tcpServer;

            var stream = tcpClient.GetStream();

            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
        }

        public async Task Process()
        {
            try
            {
                writer.WriteLineAsync("Successfully Connected!");
                await writer.FlushAsync();

                while (true)
                {
                    string command = await reader.ReadLineAsync();
                    //server.Log(command);

                    if (command == "READYTOORDER")
                    {
                        string orderDescription = await reader.ReadLineAsync();
                        int minutes = Random.Shared.Next(1, 12);
                        Order order = new Order(orderDescription,minutes);

                        //server.Log(order.orderDescription);
                        //server.Log(order.readyness.ToString());
                        //server.Log(order.timeLeft.ToString());


                        clientsOrders.Add(order);
                        server.orders.Add(order);
                        writer.WriteLineAsync("SAVEORDER");
                        writer.WriteLineAsync(order.id.ToString());
                        writer.WriteLineAsync(order.id.ToString());
                        await writer.FlushAsync();
                    }
                    else if (command == "SHOWLIST")
                    {
                        List<int> tempArray = new List<int>();
                        while (true)
                        {
                            command = await reader.ReadLineAsync();
                            if (command == "ENDOFLIST") break;
                            tempArray.Add(int.Parse(command));
                        }

                        var list = clientsOrders.IntersectBy(tempArray,e => e.id);

                        await writer.WriteLineAsync("PRINTORDERLIST");
                        foreach (var item in list)
                        {
                            await writer.WriteLineAsync(item.orderDescription);
                            await writer.WriteLineAsync(item.readyness.ToString());
                            await writer.WriteLineAsync(item.timeLeft.ToString());
                        }
                        await writer.WriteLineAsync("STOPPRINTORDERLIST");
                        await writer.FlushAsync();
                    }
                    else if (command == "REQUESTEDIT")
                    {
                        int fetchedId = int.Parse(await reader.ReadLineAsync());
                        var item = clientsOrders.FirstOrDefault(e => e.id == fetchedId);
                        item.orderDescription = await reader.ReadLineAsync();
                        clientsOrders[clientsOrders.FindIndex(e => e.id == item.id)] = item;
                        server.orders[server.orders.FindIndex(e => e.id == item.id)] = item;
                    }
                    else if (command == "REQUESTDELETE")
                    {
                        int fetchedId = int.Parse(await reader.ReadLineAsync());
                        var item = clientsOrders.FirstOrDefault(e => e.id == fetchedId);
                        clientsOrders.Remove(item);
                        server.orders.Remove(item);
                    }
                }
            }
            finally
            {
                await server.RemoveClientsOrders(clientsOrders);
                await server.DisconnectClient(Id);
            }

        }

        public async Task SendNotification()
        {
            await writer.WriteLineAsync("NOTIFICATIONONORDER");
            await writer.FlushAsync();
        }

        protected internal void Close()
        {
            writer.Close();
            reader.Close();
            client.Close();
        }
    }
    protected class Order
    {
        private static int idCounter { get; set; }
        public int id { get; set; }

        public string orderDescription { get; set; }

        public int timeLeft { get; set; }

        public Readyness readyness { get; set; }
        
        public Order(string description,int setTime)
        {
            
            id = idCounter++;
            orderDescription = description;
            timeLeft = setTime;
            readyness = Readyness.NotWorkedOn;
        }
    }

    protected enum Readyness
    {
        NotWorkedOn = 0,
        WorkedOn = 1,
        Ready = 2
    }
}