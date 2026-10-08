using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main()
    {
        string iPAddress = "127.0.0.1";
        int port = 8888;

        Client client = new Client();

        await client.JoinServer(iPAddress, port);
    }
}

class Client
{
    TcpClient tcpClient = new TcpClient();

    List<int> createdOrdersId = new List<int>();

    public async Task JoinServer(string ipAddress, int port)
    {
        try
        {

            await tcpClient.ConnectAsync(ipAddress, port);

            StreamWriter writer = new StreamWriter(tcpClient.GetStream());
            StreamReader reader = new StreamReader(tcpClient.GetStream());

            Task.Run(() => PassiveReading(reader));

            await ActiveWriting(writer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    async Task PassiveReading(StreamReader reader)
    {
        while (true)
        {
            try
            {
                string? message = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(message)) continue;
                //Print(message);
                if (message == "SAVEORDER")
                {
                    int OrderId = int.Parse(await reader.ReadLineAsync());
                    createdOrdersId.Add(OrderId);
                    Print(OrderId.ToString());
                }
                else if (message == "PRINTORDERLIST")
                {
                    while (true)
                    {
                        message = await reader.ReadLineAsync();
                        if (message == "STOPPRINTORDERLIST") break;
                        Print($"Description:{message}");
                        Print($"Readyness:{await reader.ReadLineAsync()}");
                        Print($"Time left (minutes):{await reader.ReadLineAsync()}\n");
                    }
                }
                else if (message == "NOTIFICATIONONORDER")
                {
                    Print("An order is ready!");
                }
            }
            catch
            {
                break;
            }
        }
    }

    async Task ActiveWriting(StreamWriter writer)
    {
        while (true)
        {
            Console.WriteLine("""
                1 - Place an order
                2 - View orders
                3 - Edit order
                4 - Remove order
                EXIT - exit
                """);
            string message = Console.ReadLine();
            switch (message)
            {
                case "1":
                    SendOrder(writer);
                    break;
                case "2":
                    RequestOrderList(writer);
                    break;
                case "3":
                    EditOrder(writer);
                    break;
                case "4":
                    RemoveOrder(writer);
                    break;
                case "EXIT":
                    break;
                    
            }

        }

    }

    async Task SendOrder(StreamWriter writer)
    {
        Console.WriteLine("Write the order decription to send to server:");
        string Description = Console.ReadLine();
        await writer.WriteLineAsync("READYTOORDER");
        await writer.WriteLineAsync(Description);
        await writer.FlushAsync();
    }

    async Task RequestOrderList(StreamWriter writer)
    {
        await writer.WriteLineAsync("SHOWLIST");
        foreach(var orderId in createdOrdersId)
        {
            await writer.WriteLineAsync(orderId.ToString());
        }
        await writer.WriteLineAsync("ENDOFLIST");
        await writer.FlushAsync();
    }

    async Task EditOrder(StreamWriter writer)
    {
        await writer.WriteLineAsync("REQUESTEDIT");
        int orderId;
        Print("Enter order Id");
        do
        {
            orderId = int.Parse(Console.ReadLine());
        }
        while (!createdOrdersId.Contains(orderId));
        await writer.WriteLineAsync(orderId.ToString());
        Print("Enter new description:");
        await writer.WriteLineAsync(Console.ReadLine());
        await writer.FlushAsync();
    }

    async Task RemoveOrder(StreamWriter writer)
    {
        await writer.WriteLineAsync("REQUESTDELETE");
        int orderId;
        Print("Enter order Id");
        do
        {
            orderId = int.Parse(Console.ReadLine());
        }
        while (!createdOrdersId.Contains(orderId));
        await writer.WriteLineAsync(orderId.ToString());
        await writer.FlushAsync();
    }

    async Task Print(string message)
    {
        Console.WriteLine(message);
    }
}
