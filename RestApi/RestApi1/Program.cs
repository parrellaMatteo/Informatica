using _03_RestApiClient;
using RestApi1;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //creo un oggetto di tipo ApiClient
        //andrebbe usato un solo oggetto ApiClient
        ApiClient apiClient = new();
 
        List<Product>? products = await apiClient.GetAllProductsAsync2();
 
        if (products is not null && products.Count > 0)
        {
            foreach (var prodotto in products)
            {
                Console.WriteLine($"Id: {prodotto.Id}, Name: {prodotto.Name}, Price: {prodotto.Price}, CompanyId: {prodotto.CompanyId}");
            }
        }
        System.Console.WriteLine("-----------------------------" );
        //test per il recupero di uno specifico prodotto
        Product? unProdotto = await apiClient.GetProductByIdAsync(1);
        if (unProdotto is not null)
        {
            Console.WriteLine($"Id: {unProdotto.Id}, Name: {unProdotto.Name}, Price: {unProdotto.Price}, CompanyId: {unProdotto.CompanyId}");
        }
    }
}