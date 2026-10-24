using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main()
    {
        int localPort = 8889;
        int port = 8888;
        IPAddress iPAddress = IPAddress.Parse("127.0.0.1");

        UdpClient client = new UdpClient(localPort);

        UdpClient sender = new UdpClient();

        bool boolSwitch = true;
        Console.WriteLine("Enter exit to exit");
        while (boolSwitch)
        {

            Console.WriteLine("Enter computer part name:");
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case ("exit"):
                    boolSwitch = false;
                    break;
                default:
                    byte[] data = Encoding.UTF8.GetBytes(command);
                    await sender.SendAsync(data, new IPEndPoint(iPAddress, port));
                    string message = Encoding.UTF8.GetString((await client.ReceiveAsync()).Buffer);
                    Console.WriteLine(message);
                    break;
            }


        }
    }
}