using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    static HttpClient httpClient = new HttpClient();
    static async Task Main()
    {
        using FileStream fileStream = File.OpenRead("../../../APIkey.json");
        string APIkey = JsonSerializer.Deserialize<APIKeyHandler>(fileStream).APIkey;
        string url = "https://api.themoviedb.org/3";

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIkey);

        StringBuilder httpRequest = new StringBuilder();

        while (true)
        {
            Console.WriteLine("Enter the name of the movie to search it:");
            httpRequest.Append($"{url}/search/movie?query={Console.ReadLine()}");

            try
            {

                var response = await httpClient.GetAsync(httpRequest.ToString());

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<HttpResponseHandler>();

                List<MovieInfo> movieInfos = result.results;

                foreach (var item in movieInfos)
                {
                    Console.WriteLine($"""
                        ---
                        Title: {item.title}
                        Original Title: {item.original_title}
                        Overview: {item.overview}
                        Release date: {item.release_date}
                        Genres: {string.Join(",",item.genre_ids)}
                        Movie id: {item.id}
                        ---
                        """);
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            httpRequest.Clear();
        }
    }
}

public class APIKeyHandler
{
    public string APIkey { get; set; }
}

public class HttpResponseHandler
{
    public int page { get; set; }
    public List<MovieInfo> results { get; set; }
    public int total_pages { get; set; }
    public int total_results { get; set; }
}


public class MovieInfo
{
    public bool adult { get; set; }
    public string backdrop_path { get; set; }
    public int[] genre_ids { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string original_language { get; set; }
    public string original_title { get; set; }
    public string overview { get; set; }
    public float popularity { get; set; }
    public string poster_path { get; set; }
    public string release_date { get; set; }
    public bool softcore { get; set; }
    public bool video { get; set; }
    public float vote_average { get; set; }
    public int vote_count { get; set; }
}


//public class MovieInfo
//{

//    public bool adult { get; set; }
//    public string backdrop_path { get; set; }
//    public Belongs_To_Collection belongs_to_collection { get; set; }
//    public int budget { get; set; }
//    public Genre[] genres { get; set; }
//    public string homepage { get; set; }
//    public int id { get; set; }
//    public string imdb_id { get; set; }
//    public string[] origin_country { get; set; }
//    public string original_language { get; set; }
//    public string original_title { get; set; }
//    public string overview { get; set; }
//    public float popularity { get; set; }
//    public string poster_path { get; set; }
//    public Production_Companies[] production_companies { get; set; }
//    public Production_Countries[] production_countries { get; set; }
//    public string release_date { get; set; }
//    public int revenue { get; set; }
//    public int runtime { get; set; }
//    public bool softcore { get; set; }
//    public Spoken_Languages[] spoken_languages { get; set; }
//    public string status { get; set; }
//    public string tagline { get; set; }
//    public string title { get; set; }
//    public bool video { get; set; }
//    public float vote_average { get; set; }
//    public int vote_count { get; set; }

//    public class Belongs_To_Collection
//    {
//        public int id { get; set; }
//        public string name { get; set; }
//        public string poster_path { get; set; }
//        public string backdrop_path { get; set; }
//    }

//    public class Genre
//    {
//        public int id { get; set; }
//        public string name { get; set; }
//    }

//    public class Production_Companies
//    {
//        public int id { get; set; }
//        public string logo_path { get; set; }
//        public string name { get; set; }
//        public string origin_country { get; set; }
//    }

//    public class Production_Countries
//    {
//        public string iso_3166_1 { get; set; }
//        public string name { get; set; }
//    }

//    public class Spoken_Languages
//    {
//        public string english_name { get; set; }
//        public string iso_639_1 { get; set; }
//        public string name { get; set; }
//    }
//}


