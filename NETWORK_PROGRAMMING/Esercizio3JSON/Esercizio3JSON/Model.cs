using System.Text.Json.Serialization;
 
public class Recipe
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
 
        [JsonPropertyName("name")]
        public string? Name { get; set; }
 
        [JsonPropertyName("ingredients")]
        public List<string>? Ingredients { get; set; }
 
        [JsonPropertyName("instructions")]
        public List<string>? Instructions { get; set; }
 
        [JsonPropertyName("prepTimeMinutes")]
        public int? PrepTimeMinutes { get; set; }
 
        [JsonPropertyName("cookTimeMinutes")]
        public int? CookTimeMinutes { get; set; }
 
        [JsonPropertyName("servings")]
        public int? Servings { get; set; }
 
        [JsonPropertyName("difficulty")]
        public string? Difficulty { get; set; }
 
        [JsonPropertyName("cuisine")]
        public string? Cuisine { get; set; }
 
        [JsonPropertyName("caloriesPerServing")]
        public int? CaloriesPerServing { get; set; }
 
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }
 
        [JsonPropertyName("userId")]
        public int? UserId { get; set; }
 
        [JsonPropertyName("image")]
        public string? Image { get; set; }
 
        [JsonPropertyName("rating")]
        public double? Rating { get; set; }
 
        [JsonPropertyName("reviewCount")]
        public int? ReviewCount { get; set; }
 
        [JsonPropertyName("mealType")]
        public List<string>? MealType { get; set; }
    }
 
    public class RecipeResponse
    {
        [JsonPropertyName("recipes")]
        public List<Recipe>? Recipes { get; set; }
 
        [JsonPropertyName("total")]
        public int? Total { get; set; }
 
        [JsonPropertyName("skip")]
        public int? Skip { get; set; }
 
        [JsonPropertyName("limit")]
        public int? Limit { get; set; }
    }
 