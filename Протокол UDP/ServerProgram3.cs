using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main()
    {
        int localPort = 8888;
        int port = 8889;
        IPAddress iPAddress = IPAddress.Parse("127.0.0.1");


        List<ComputerComponent> computerComponents = new List<ComputerComponent>()
        {
            new ComputerComponent(){Name = "GeForce RTX 5070", ComponentType = ComputerComponent.Type.GPU, Price = 5000.0},
            new ComputerComponent(){Name = "AMD Ryzen 7 5700X", ComponentType = ComputerComponent.Type.CPU, Price = 5000.0},
            new ComputerComponent(){Name = "Kingston FURY 32 GB (2x16GB) DDR5 6000 MHz Beast EXPO", ComponentType = ComputerComponent.Type.RAM, Price = 5000.0}
        };

        UdpClient server = new UdpClient(localPort);

        UdpClient sender = new UdpClient();

        while (true)
        {
            string message = Encoding.UTF8.GetString((await server.ReceiveAsync()).Buffer);

            var selectedComputerComponents = computerComponents.Where(e => e.Name.Contains(message));

            StringBuilder fullList = new StringBuilder();

            foreach (var item in selectedComputerComponents)
            {
                fullList.Append($"{item.Name}\nType: {item.ComponentType}\nPrice: {item.Price}\n--\n");
            }

            byte[] data = Encoding.UTF8.GetBytes(fullList.ToString());

            await sender.SendAsync(data, new IPEndPoint(iPAddress, port));
        }
    }
}

struct ComputerComponent
{
    public string Name { get; set; }

    public Type ComponentType { get; set; }

    public double Price { get; set; }
    public enum Type
    {
        CPU,
        GPU,
        RAM
    }
}