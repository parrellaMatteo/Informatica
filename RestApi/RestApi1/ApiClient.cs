using System.Net.Http.Json;
using System.Text.Json;
using RestApi1;

namespace _03_RestApiClient;
 
public class ApiClient
{
    //classe che incapsula tutta la logica per effettuare le richieste verso il server
    //base url
    private string _baseUrl; //punta al base url
    private HttpClient _httpClient; //client che effettua le richieste
    private JsonSerializerOptions _jsonOptions; //opzioni per la serializzazione del JSON
 
    //costruttore che inizializza il client
 
    public ApiClient()
    {
        _baseUrl = "http://localhost:3000";
 
        //inizializza una istanza di httpClient con il base address
        _httpClient = new()
        {
            BaseAddress = new Uri(_baseUrl)
        };
 
        //definiamo le opzioni per serializzazione e deserializzazzione
        _jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true, //serve per avere un output formattato
            PropertyNameCaseInsensitive = true //
        };
    }
 
    public async Task<List<Product>?> GetAllProductsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>(_jsonOptions);
 
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Errore nella richiesta: {ex.Message}");
            return null;
        }
       
    }
 
 
    public async Task<List<Product>?> GetAllProductsAsync2()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("/products");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Errore nella richiesta: {ex.Message}");
            return null;
        }
    }
 
    public async Task<Product?> GetProductByIdAsync(long productId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Product>($"/products/{productId}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Errore nella richiesta: {ex.Message}");
            return null;
        }
    }
}