using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main()
    {
        Server server = new Server();

        string ipAddress = "127.0.0.1";

        int port = 8888;

        await server.StartServer(IPAddress.Parse(ipAddress), port);
    }
}

public class Server
{
    protected List<CurrencyExchange> currencyExchangeList = new List<CurrencyExchange>
    {
        new CurrencyExchange(){valueName1 = "USD", valueName2 = "EURO", exchangeRate = 1.13},
        new CurrencyExchange(){valueName1 = "EURO", valueName2 = "UAH", exchangeRate = 50.55},
        new CurrencyExchange(){valueName1 = "USD", valueName2 = "UAH", exchangeRate = 44.99}
    };
    List<Client> currentClients = new List<Client>();

    TcpListener tcpListener;

    public async Task StartServer(IPAddress iPAddress, int port)
    {
        try
        {
            tcpListener = new TcpListener(iPAddress, port);

            tcpListener.Start();

            while (true)
            {
                Client connectedClient = new Client(await tcpListener.AcceptTcpClientAsync(),this);
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

    async Task DisconnectAll()
    {
        Log($"Server closed at: {DateTime.Now}");
        foreach (var client in currentClients)
        {
            client.Close();
        }
        tcpListener.Stop();
    }
    class Client
    {
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
                await writer.WriteLineAsync("Connected! Enter Username");
                await writer.FlushAsync();
                string userName = await reader.ReadLineAsync();

                try
                {

                    server.Log($"{userName} Connected");

                    while (true)
                    {
                        await writer.WriteLineAsync("Enter 2 currency names to see the exchange rate (eg: \"USD EURO\"), type EXIT to EXIT");
                        await writer.FlushAsync();
                        string message = await reader.ReadLineAsync();
                        if (message == "EXIT")
                            break;

                        await writer.WriteLineAsync("Enter amount:");
                        await writer.FlushAsync();
                        double amount = double.Parse(await reader.ReadLineAsync());

                        server.Log($"{userName} Made a currency exchange request at: {DateTime.Now}");

                        string[] currencyNameDivided = message.Split(' ', 2);

                        var selectedCurrencyExchange = server.currencyExchangeList.Where(e => (e.valueName1 == currencyNameDivided[0] || e.valueName1 == currencyNameDivided[1]) && (e.valueName2 == currencyNameDivided[0] || e.valueName2 == currencyNameDivided[1])).FirstOrDefault();

                        double transformedAmount;

                        if (selectedCurrencyExchange.valueName1 == currencyNameDivided[0])
                            transformedAmount = amount / selectedCurrencyExchange.exchangeRate;
                        else
                            transformedAmount = amount * selectedCurrencyExchange.exchangeRate;

                        await writer.WriteLineAsync($"{currencyNameDivided[0]}:{amount.ToString()}->{currencyNameDivided[1]}:{transformedAmount.ToString()}");
                        await writer.FlushAsync();

                        server.Log($"Transaction data has been sent to {userName}");
                    }
                }
                finally
                {
                    server.Log($"{userName} Disconnected");
                }
            }
            finally
            {
                await server.DisconnectClient(Id);
            }

        }

        protected internal void Close()
        {
            writer.Close();
            reader.Close();
            client.Close();
        }
    }
    protected struct CurrencyExchange
    {
        public string valueName1 { get; set; }
        public string valueName2 { get; set; }
        public double exchangeRate { get; set; }
    }
}