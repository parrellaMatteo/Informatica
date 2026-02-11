// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Text.Json.Serialization;
namespace OpenMeteoAPIClient.model.minimal;

public class Current
{
    [JsonProperty("time")]
    public int? Time { get; set; }

    [JsonProperty("interval")]
    public int? Interval { get; set; }

    [JsonProperty("temperature_2m")]
    public double? Temperature2m { get; set; }

    [JsonProperty("weather_code")]
    public int? WeatherCode { get; set; }

    [JsonProperty("wind_speed_10m")]
    public double? WindSpeed10m { get; set; }

    [JsonProperty("wind_direction_10m")]
    public int? WindDirection10m { get; set; }
}

public class CurrentUnits
{
    [JsonProperty("time")]
    public string Time { get; set; }

    [JsonProperty("interval")]
    public string Interval { get; set; }

    [JsonProperty("temperature_2m")]
    public string Temperature2m { get; set; }

    [JsonProperty("weather_code")]
    public string WeatherCode { get; set; }

    [JsonProperty("wind_speed_10m")]
    public string WindSpeed10m { get; set; }

    [JsonProperty("wind_direction_10m")]
    public string WindDirection10m { get; set; }
}

public class Daily
{
    [JsonProperty("time")]
    public List<int?> Time { get; set; }

    [JsonProperty("weather_code")]
    public List<int?> WeatherCode { get; set; }

    [JsonProperty("temperature_2m_max")]
    public List<double?> Temperature2mMax { get; set; }

    [JsonProperty("temperature_2m_min")]
    public List<double?> Temperature2mMin { get; set; }

    [JsonProperty("apparent_temperature_max")]
    public List<double?> ApparentTemperatureMax { get; set; }

    [JsonProperty("apparent_temperature_min")]
    public List<double?> ApparentTemperatureMin { get; set; }
}

public class DailyUnits
{
    [JsonProperty("time")]
    public string Time { get; set; }

    [JsonProperty("weather_code")]
    public string WeatherCode { get; set; }

    [JsonProperty("temperature_2m_max")]
    public string Temperature2mMax { get; set; }

    [JsonProperty("temperature_2m_min")]
    public string Temperature2mMin { get; set; }

    [JsonProperty("apparent_temperature_max")]
    public string ApparentTemperatureMax { get; set; }

    [JsonProperty("apparent_temperature_min")]
    public string ApparentTemperatureMin { get; set; }
}

public class Hourly
{
    [JsonProperty("time")]
    public List<int?> Time { get; set; }

    [JsonProperty("temperature_2m")]
    public List<double?> Temperature2m { get; set; }

    [JsonProperty("relative_humidity_2m")]
    public List<int?> RelativeHumidity2m { get; set; }

    [JsonProperty("dew_point_2m")]
    public List<double?> DewPoint2m { get; set; }

    [JsonProperty("apparent_temperature")]
    public List<double?> ApparentTemperature { get; set; }

    [JsonProperty("precipitation_probability")]
    public List<int?> PrecipitationProbability { get; set; }

    [JsonProperty("precipitation")]
    public List<double?> Precipitation { get; set; }

    [JsonProperty("rain")]
    public List<double?> Rain { get; set; }

    [JsonProperty("showers")]
    public List<double?> Showers { get; set; }

    [JsonProperty("weather_code")]
    public List<int?> WeatherCode { get; set; }

    [JsonProperty("wind_speed_10m")]
    public List<double?> WindSpeed10m { get; set; }

    [JsonProperty("wind_direction_10m")]
    public List<int?> WindDirection10m { get; set; }
}

public class HourlyUnits
{
    [JsonProperty("time")]
    public string Time { get; set; }

    [JsonProperty("temperature_2m")]
    public string Temperature2m { get; set; }

    [JsonProperty("relative_humidity_2m")]
    public string RelativeHumidity2m { get; set; }

    [JsonProperty("dew_point_2m")]
    public string DewPoint2m { get; set; }

    [JsonProperty("apparent_temperature")]
    public string ApparentTemperature { get; set; }

    [JsonProperty("precipitation_probability")]
    public string PrecipitationProbability { get; set; }

    [JsonProperty("precipitation")]
    public string Precipitation { get; set; }

    [JsonProperty("rain")]
    public string Rain { get; set; }

    [JsonProperty("showers")]
    public string Showers { get; set; }

    [JsonProperty("weather_code")]
    public string WeatherCode { get; set; }

    [JsonProperty("wind_speed_10m")]
    public string WindSpeed10m { get; set; }

    [JsonProperty("wind_direction_10m")]
    public string WindDirection10m { get; set; }
}

public class Root
{
    [JsonProperty("latitude")]
    public double? Latitude { get; set; }

    [JsonProperty("longitude")]
    public double? Longitude { get; set; }

    [JsonProperty("generationtime_ms")]
    public double? GenerationtimeMs { get; set; }

    [JsonProperty("utc_offset_seconds")]
    public int? UtcOffsetSeconds { get; set; }

    [JsonProperty("timezone")]
    public string Timezone { get; set; }

    [JsonProperty("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; }

    [JsonProperty("elevation")]
    public int? Elevation { get; set; }

    [JsonProperty("current_units")]
    public CurrentUnits CurrentUnits { get; set; }

    [JsonProperty("current")]
    public Current Current { get; set; }

    [JsonProperty("hourly_units")]
    public HourlyUnits HourlyUnits { get; set; }

    [JsonProperty("hourly")]
    public Hourly Hourly { get; set; }

    [JsonProperty("daily_units")]
    public DailyUnits DailyUnits { get; set; }

    [JsonProperty("daily")]
    public Daily Daily { get; set; }
}

