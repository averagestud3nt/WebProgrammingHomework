using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main()
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7174/ExchangeValue")
            .Build();

        connection.On<string>("ReceiveCurrencyUpdate", (message) =>
        {
            Console.WriteLine("Currency exchange rate for " + message);
        });

        await connection.StartAsync();
        Console.WriteLine("Connected");
        Console.ReadLine();
    }
}