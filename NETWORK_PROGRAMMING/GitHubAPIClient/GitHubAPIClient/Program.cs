using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using GithubAPIClient.Models;

internal class Program
{
    static async Task Main(string[] args)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        var repositories = await ProcessRepositoriesAsync2(client);

        foreach (var repo in repositories)
        {
            Console.WriteLine($"Name: {repo.Name}");
            Console.WriteLine($"Homepage: {repo.Homepage}");
            Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
            Console.WriteLine($"Description: {repo.Description}");
            Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
            Console.WriteLine($"{repo.LastPush}");
            Console.WriteLine();
        }

        // VERSIONE 1: GetStreamAsync + JsonSerializer.DeserializeAsync
        // PRO: Efficiente con la memoria - deserializza in streaming
        //      Controllo manuale sullo stream se necessario
        // CONTRO: Più verbosa rispetto a GetFromJsonAsync
        // QUANDO USARLA: Quando serve controllo diretto sullo stream (es. cancellazione, progress)
        static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
        {
            await using Stream stream =
                await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories =
                await JsonSerializer.DeserializeAsync<List<Repository>>(stream);
            return repositories ?? [];
        }

        // VERSIONE 2: GetFromJsonAsync ⭐ CONSIGLIATA
        // PRO: Concisa e leggibile - combina download e deserializzazione
        //      Efficiente con la memoria - deserializza in streaming
        //      Gestione automatica degli errori HTTP
        //      Usa la configurazione JSON centralizzata dell'HttpClient
        // CONTRO: Meno controllo sulla risposta HTTP
        // QUANDO USARLA: Per la maggior parte dei casi - è la scelta preferita
        static async Task<List<Repository>> ProcessRepositoriesAsync2(HttpClient client)
        {
            var repositories =
                await client.GetFromJsonAsync<List<Repository>>("https://api.github.com/orgs/dotnet/repos");
            return repositories ?? [];
        }

        // VERSIONE 3: GetStringAsync + JsonSerializer.Deserialize ❌ SCONSIGLIATA
        // PRO: Semplice da capire - due passaggi distinti
        // CONTRO: Inefficiente - carica tutto il JSON in memoria come stringa
        //         Doppio consumo di memoria (stringa + oggetto deserializzato)
        //         Non streaming - problematico per file JSON di grandi dimensioni
        // QUANDO USARLA: Solo per JSON molto piccoli o per debugging/logging della risposta
        static async Task<List<Repository>> ProcessRepositoriesAsync3(HttpClient client)
        {
            string jsonString =
                await client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories =
                JsonSerializer.Deserialize<List<Repository>>(jsonString);
            return repositories ?? [];
        }

        // VERSIONE 4: GetAsync + ReadFromJsonAsync
        // PRO: Massimo controllo - accesso completo a HttpResponseMessage
        //      Possibilità di ispezionare header, status code, ecc.
        //      Gestione personalizzata degli errori
        //      Efficiente - deserializzazione in streaming
        // CONTRO: Più verbosa
        // QUANDO USARLA: Quando serve accedere agli header di risposta o gestire
        //                gli status code in modo personalizzato
        static async Task<List<Repository>> ProcessRepositoriesAsync4(HttpClient client)
        {
            HttpResponseMessage response =
                await client.GetAsync("https://api.github.com/orgs/dotnet/repos");
            response.EnsureSuccessStatusCode();
            var repositories =
                await response.Content.ReadFromJsonAsync<List<Repository>>();
            return repositories ?? [];
        }

    }

}