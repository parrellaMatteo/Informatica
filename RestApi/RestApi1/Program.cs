using _03_RestApiClient;
using RestApi1;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //creo un oggetto di tipo ApiClient
        //andrebbe usato un solo oggetto ApiClient
        ApiClient apiClient = new();
        System.Console.WriteLine("----------------------------- GET ALL");

        List<Product>? products = await apiClient.GetAllProductsAsync2();

        if (products is not null && products.Count > 0)
        {
            foreach (var prodotto in products)
            {
                Console.WriteLine($"Id: {prodotto.Id}, Name: {prodotto.Name}, Price: {prodotto.Price}, CompanyId: {prodotto.CompanyId}");
            }
        }
        System.Console.WriteLine("----------------------------- GET BY ID");
        //test per il recupero di uno specifico prodotto
        Product? unProdotto = await apiClient.GetProductByIdAsync(1);
        if (unProdotto is not null)
        {
            Console.WriteLine($"Id: {unProdotto.Id}, Name: {unProdotto.Name}, Price: {unProdotto.Price}, CompanyId: {unProdotto.CompanyId}");
        }
        //test di creazione di un nuovo prodotto tramite POST
        System.Console.WriteLine("-----------------------------   POST");
        Product ilProdotto = new()
        {
            //id non va inserito dal client, lo genera il server
            Name = "Memoria RAM DDR5",
            Price = 300.0,
            CompanyId = 5
        };
        Product? prodottoCreato = await apiClient.CreateProductAsync(ilProdotto);
        if (prodottoCreato is not null)
        {
            Console.WriteLine($"Id: {prodottoCreato.Id}, Name: {prodottoCreato.Name}, Price: {prodottoCreato.Price}, CompanyId: {prodottoCreato.CompanyId}");
        }
        //test di aggiornamento di un prodotto tramite PUT
        System.Console.WriteLine("-----------------------------   PUT");
        if (unProdotto is not null)
        {
            unProdotto.Name += " Xiuderone";
            unProdotto.Price += 50.0; //aumento il prezzo di 50 euro
            Product? prodottoAggiornato = await apiClient.UpdateProductAsync(unProdotto.Id, unProdotto);
            if (prodottoAggiornato is not null)
            {
                Console.WriteLine($"Id: {prodottoAggiornato.Id}, Name: {prodottoAggiornato.Name}, Price: {prodottoAggiornato.Price}, CompanyId: {prodottoAggiornato.CompanyId}");
            }
        }
        //test di aggiornamento di un prodotto tramite PATCH
        System.Console.WriteLine("-----------------------------   PATCH");
            if(unProdotto is not null)
            {
                var partialUpdate = new {Price = 127.11, Name = "Prodotto aggiornato tramite patch"};
                var patchedProduct = await apiClient.PatchProductAsync(unProdotto.Id,partialUpdate);
                if(patchedProduct is not null)
                {
                    Console.WriteLine("Il prodotto è stato aggiornato correttamente con la patch");
                    Console.WriteLine($"Prodotto trovato: ID: {patchedProduct.Id}, Name: {patchedProduct.Name}, Price: {patchedProduct.Price}");
    
                }
            }
        //test di cancellazione di un prodotto tramite DELETE
        System.Console.WriteLine("-----------------------------   DELETE");
        if (prodottoCreato is not null)
        {
            var deleteResult = await apiClient.DeleteProductAsync(prodottoCreato.Id);
            Console.WriteLine($"Cancellazione del prodotto con Id {prodottoCreato.Id} {(deleteResult ? "riuscita" : "fallita")}");
        }
    }
}