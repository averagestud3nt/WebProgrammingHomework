using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main()
    {
        string ipAddress = "127.0.0.1";

        int port = 8888;

        Client client = new Client();

        await client.JoinServer(ipAddress, port);
    }
}

class Client
{
    TcpClient tcpClient = new TcpClient();

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
                    Print(message);
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
            string message = Console.ReadLine();
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
            if (message == "EXIT")
                break;
        }

    }

    async Task Print(string message)
    {
        Console.WriteLine(message);
    }
}