// Example of using HttpClient extension methods to send and receive JSON data.
//https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace HttpClientExtensionMethods
{
    // serve un modello dati in C#
    public class Address
    {
        [JsonPropertyName("street")]
        public string Street { get; set; }
 
        [JsonPropertyName("suite")]
        public string Suite { get; set; }
 
        [JsonPropertyName("city")]
        public string City { get; set; }
 
        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }
 
        [JsonPropertyName("geo")]
        public Geo Geo { get; set; }
    }
 
    public class Company
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
 
        [JsonPropertyName("catchPhrase")]
        public string CatchPhrase { get; set; }
 
        [JsonPropertyName("bs")]
        public string Bs { get; set; }
    }
 
    public class Geo
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; }
 
        [JsonPropertyName("lng")]
        public string Lng { get; set; }
    }
 
    public class User
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
 
        [JsonPropertyName("name")]
        public string Name { get; set; }
 
        [JsonPropertyName("username")]
        public string Username { get; set; }
 
        [JsonPropertyName("email")]
        public string Email { get; set; }
 
        [JsonPropertyName("address")]
        public Address Address { get; set; }
 
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
 
        [JsonPropertyName("website")]
        public string Website { get; set; }
 
        [JsonPropertyName("company")]
        public Company Company { get; set; }
    }
 
    public class Program
    {
        public static async Task Main()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
            };
            // Get the user information.
            // usiamo un metodo di estensione
            // il metodo vuole tra le parentesi il tipo che ci si aspetta, ovvero l'oggetto in cui si vuole trasformare il formato Json
            // il metodo prima tira giù la stringa e poi deserealizza
            //GET https://jsonplaceholder.typicode.com/users/1
            User? user = await client.GetFromJsonAsync<User>("users/1");
            Console.WriteLine($"Id: {user?.Id}");
            Console.WriteLine($"Name: {user?.Name}");
            Console.WriteLine($"Username: {user?.Username}");
            Console.WriteLine($"Email: {user?.Email}");
            

            System.Console.WriteLine("metodo alternativo - uso GetStringAsync");
            //versione alternativa con GetAsync
            var Jsonstring = await client.GetStringAsync("users/1");
            if(Jsonstring is not null)
            {
                User? user2 = JsonSerializer.Deserialize<User>(Jsonstring);
                Console.WriteLine($"Id: {user2?.Id}");
                Console.WriteLine($"Name: {user2?.Name}");
                Console.WriteLine($"Username: {user2?.Username}");
                Console.WriteLine($"Email: {user2?.Email}");
            }
            // Post a new user.
            HttpResponseMessage response = await client.PostAsJsonAsync("users", user);
            Console.WriteLine(
            $"{(response.IsSuccessStatusCode ? "Success" : "Error")} - {response.StatusCode}");
        }
    }
}
// Produces output like the following example but with different names:
// Id: 1
// Name: Leanne Graham
// Username: Bret
// Email: Sincere @april.biz
// Success - Created