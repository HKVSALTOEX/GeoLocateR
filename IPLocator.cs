using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Veuillez entrer l'adresse IP: ");
        string ip = Console.ReadLine();
        var location = await GetGeolocationAsync(ip);

        if (location != null)
        {
            Console.WriteLine($"Pays: {location.Country}\nRégion: {location.RegionName}\nVille: {location.City}\nLatitude: {location.Lat}\nLongitude: {location.Lon}");
        }
        else
        {
            Console.WriteLine("Adresse IP non trouvée ou invalide.");
        }

        Console.WriteLine("Appuyez sur Entrée pour quitter...");
        Console.ReadLine();
    }

    static async Task<Geolocation> GetGeolocationAsync(string ip)
    {
        try
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"http://ip-api.com/json/{ip}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var location = Newtonsoft.Json.JsonConvert.DeserializeObject<Geolocation>(responseBody);
            return location;
        }
        catch
        {
            return null;
        }
    }

    class Geolocation
    {
        public string Status { get; set; }
        public string Country { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
    }
}