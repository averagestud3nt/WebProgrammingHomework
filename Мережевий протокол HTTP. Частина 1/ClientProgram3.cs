using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

class Program
{
    static HttpClient httpClient = new HttpClient();

    static async Task Main()
    {
        string url = "https://zenquotes.io/api/random";

        Console.WriteLine("Press enter to get a quote");

        try
        {
            while (true)
            {
                Console.ReadLine();

                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Quote[]>();

                foreach (var item in result)
                {
                    Console.WriteLine(item.q);
                    Console.WriteLine(item.a);
                }
            }

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
        }

    }
}

public class Quote
{
    public string q { get; set; }
    public string a { get; set; }
    public string h { get; set; }
}
