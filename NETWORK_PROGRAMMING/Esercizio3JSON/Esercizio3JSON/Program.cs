using System.Net.Http.Json;
internal class Program
{
    private static async Task Main(string[] args)
    {
        //crearere un oggetto http client
        HttpClient client = new()
        {
            BaseAddress = new Uri("https://dummyjson.com/")
        };

        var Recipe=await client.GetFromJsonAsync<RecipeResponse>("recipes");
        List<Recipe> recipes = Recipe?.Recipes?.Take(10).ToList() ?? new List<Recipe>();
        //stampa delle ricette  nome ed ingredienti
        foreach (var recipe in recipes)
        {
            System.Console.WriteLine($"Recipe: {recipe.Name}");
            System.Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients ?? new List<string>())
            {
                System.Console.WriteLine($"  - {ingredient}");
            }
        }
    }
}