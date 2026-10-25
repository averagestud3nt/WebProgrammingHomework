using HomeworkWebApplication1.Hubs;
using Microsoft.AspNetCore.SignalR;


class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSignalR();

        var app = builder.Build();

        app.MapHub<ExchangeValueHub>("/ExchangeValue");
        StartCurrencyUpdates(app.Services);
        app.Run();
    }

    private static void StartCurrencyUpdates(IServiceProvider services)
    {
        var random = new Random();
        var hubContext = services.GetService<IHubContext<ExchangeValueHub>>();

        Task.Run(() =>
        {
            while (true)
            {
                var usdToUah = Math.Round(Random.Shared.NextDouble() * (100) + 10.0, 4);
                var usdToEur = Math.Round(Random.Shared.NextDouble() * (100) + 10.0, 4);

                hubContext.Clients.All.SendAsync("ReceiveCurrencyUpdate", $"usd to uah: {usdToUah}");
                hubContext.Clients.All.SendAsync("ReceiveCurrencyUpdate", $"usd to eur: {usdToEur}");

                Thread.Sleep(5000);
            }
        });
    }
}