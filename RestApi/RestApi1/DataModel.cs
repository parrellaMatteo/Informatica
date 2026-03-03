
namespace RestApi1;
    using System.Text.Json.Serialization;

    public partial class Product
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }=string.Empty;

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("company_id")]
        public long CompanyId { get; set; }
    }
    public class Company
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }=string.Empty;

        [JsonPropertyName("revenue")]
        public double Revenue { get; set; }

        [JsonPropertyName("headquarter")]
        public Headquarter Headquarter { get; set; }=new();

        [JsonPropertyName("locations")]
        public List<Location> Locations { get; set; }=[];
    }

    public partial class Headquarter
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }
    }

    public partial class Location
    {
        [JsonPropertyName("city")]
        public string City { get; set; }=String.Empty;

        [JsonPropertyName("employee_number")]

        public long EmployeeNumber { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }=string.Empty;
    }
