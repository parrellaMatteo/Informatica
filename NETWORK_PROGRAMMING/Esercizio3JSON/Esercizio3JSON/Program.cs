using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
 
internal class Program
{
    private static async Task Main(string[] args)
    {
        HttpClient client = new()
        {
            BaseAddress = new Uri("https://dummyjson.com/")
        };
        RecipeResponse? recipeResponse = await client.GetFromJsonAsync<RecipeResponse>("/recipes");
        List<Recipe>? recipes = recipeResponse?.Recipes.Take(10).ToList();
        //definiamo il percorso dove andremo a salvare le immagini scaricate
 
        var imageDirectory = Path.Combine(AppContext.BaseDirectory,"../../../","cachedPhotos");
        Directory.CreateDirectory(imageDirectory);
        Console.WriteLine(recipes);
        //stampa delle ricette
        if(recipes is not null)
        {
            foreach(var recipe in recipes)
            {
                Console.WriteLine($"RICETTA : {recipe.Name}, TIPOLOGIA : {recipe.MealType}, DIFFICOLTA : {recipe.Difficulty}");
                Console.WriteLine($"URL IMMAGINE : {recipe.Image}");
                Console.WriteLine($"LISTA INGREDIENTI : ");
                if(recipe.Ingredients is not null)
                {
                    foreach(var ingrediente in recipe.Ingredients)
                    {
                        Console.WriteLine(ingrediente);
                    }
                }
                if(recipe.Image is not null)
                {
                    var fileName = GetFileNameFromUrl(recipe.Image);
                    var filePath = Path.Combine(imageDirectory, fileName);
                    if(!File.Exists(filePath))
                    {
                        await DownloadImageAsync(client, recipe.Image, filePath);
                    }
                }
            }
        }
    }
}