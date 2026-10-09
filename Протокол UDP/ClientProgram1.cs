using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

class Program
{
    static HttpClient httpClient = new HttpClient();
    static async Task Main()
    {
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
                    StringContent content = new StringContent(command);
                    using (var response = await httpClient.PostAsync("https://localhost:7138/", content))
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseText);
                        break;
                    }
            }

            
        }
    }
}