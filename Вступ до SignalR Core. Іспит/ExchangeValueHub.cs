using Microsoft.AspNetCore.SignalR;

namespace HomeworkWebApplication1.Hubs
{
    public class ExchangeValueHub : Hub
    {
        public async Task SendCurrencyUpdate(string message)
        {
            {
                await Clients.All.SendAsync("ReceiveCurrencyUpdate", message);
            }
        }
    }
}