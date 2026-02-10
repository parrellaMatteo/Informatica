// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Current
    {
        [JsonProperty("time")]
        public int? Time { get; set; }

        [JsonProperty("interval")]
        public int? Interval { get; set; }

        [JsonProperty("temperature_2m")]
        public double? Temperature2m { get; set; }

        [JsonProperty("relative_humidity_2m")]
        public int? RelativeHumidity2m { get; set; }

        [JsonProperty("apparent_temperature")]
        public double? ApparentTemperature { get; set; }

        [JsonProperty("is_day")]
        public int? IsDay { get; set; }

        [JsonProperty("precipitation")]
        public int? Precipitation { get; set; }

        [JsonProperty("rain")]
        public int? Rain { get; set; }

        [JsonProperty("showers")]
        public int? Showers { get; set; }

        [JsonProperty("snowfall")]
        public int? Snowfall { get; set; }

        [JsonProperty("weather_code")]
        public int? WeatherCode { get; set; }

        [JsonProperty("cloud_cover")]
        public int? CloudCover { get; set; }

        [JsonProperty("pressure_msl")]
        public double? PressureMsl { get; set; }

        [JsonProperty("surface_pressure")]
        public int? SurfacePressure { get; set; }

        [JsonProperty("wind_speed_10m")]
        public double? WindSpeed10m { get; set; }

        [JsonProperty("wind_direction_10m")]
        public int? WindDirection10m { get; set; }

        [JsonProperty("wind_gusts_10m")]
        public double? WindGusts10m { get; set; }
    }

    public class CurrentUnits
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("temperature_2m")]
        public string Temperature2m { get; set; }

        [JsonProperty("relative_humidity_2m")]
        public string RelativeHumidity2m { get; set; }

        [JsonProperty("apparent_temperature")]
        public string ApparentTemperature { get; set; }

        [JsonProperty("is_day")]
        public string IsDay { get; set; }

        [JsonProperty("precipitation")]
        public string Precipitation { get; set; }

        [JsonProperty("rain")]
        public string Rain { get; set; }

        [JsonProperty("showers")]
        public string Showers { get; set; }

        [JsonProperty("snowfall")]
        public string Snowfall { get; set; }

        [JsonProperty("weather_code")]
        public string WeatherCode { get; set; }

        [JsonProperty("cloud_cover")]
        public string CloudCover { get; set; }

        [JsonProperty("pressure_msl")]
        public string PressureMsl { get; set; }

        [JsonProperty("surface_pressure")]
        public string SurfacePressure { get; set; }

        [JsonProperty("wind_speed_10m")]
        public string WindSpeed10m { get; set; }

        [JsonProperty("wind_direction_10m")]
        public string WindDirection10m { get; set; }

        [JsonProperty("wind_gusts_10m")]
        public string WindGusts10m { get; set; }
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

        [JsonProperty("sunrise")]
        public List<int?> Sunrise { get; set; }

        [JsonProperty("sunset")]
        public List<int?> Sunset { get; set; }

        [JsonProperty("daylight_duration")]
        public List<double?> DaylightDuration { get; set; }

        [JsonProperty("sunshine_duration")]
        public List<double?> SunshineDuration { get; set; }

        [JsonProperty("uv_index_max")]
        public List<double?> UvIndexMax { get; set; }

        [JsonProperty("uv_index_clear_sky_max")]
        public List<double?> UvIndexClearSkyMax { get; set; }

        [JsonProperty("precipitation_sum")]
        public List<double?> PrecipitationSum { get; set; }

        [JsonProperty("rain_sum")]
        public List<double?> RainSum { get; set; }

        [JsonProperty("showers_sum")]
        public List<double?> ShowersSum { get; set; }

        [JsonProperty("snowfall_sum")]
        public List<int?> SnowfallSum { get; set; }

        [JsonProperty("precipitation_hours")]
        public List<int?> PrecipitationHours { get; set; }

        [JsonProperty("precipitation_probability_max")]
        public List<int?> PrecipitationProbabilityMax { get; set; }

        [JsonProperty("wind_speed_10m_max")]
        public List<double?> WindSpeed10mMax { get; set; }

        [JsonProperty("wind_gusts_10m_max")]
        public List<double?> WindGusts10mMax { get; set; }

        [JsonProperty("wind_direction_10m_dominant")]
        public List<int?> WindDirection10mDominant { get; set; }

        [JsonProperty("shortwave_radiation_sum")]
        public List<double?> ShortwaveRadiationSum { get; set; }

        [JsonProperty("et0_fao_evapotranspiration")]
        public List<double?> Et0FaoEvapotranspiration { get; set; }
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

        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("daylight_duration")]
        public string DaylightDuration { get; set; }

        [JsonProperty("sunshine_duration")]
        public string SunshineDuration { get; set; }

        [JsonProperty("uv_index_max")]
        public string UvIndexMax { get; set; }

        [JsonProperty("uv_index_clear_sky_max")]
        public string UvIndexClearSkyMax { get; set; }

        [JsonProperty("precipitation_sum")]
        public string PrecipitationSum { get; set; }

        [JsonProperty("rain_sum")]
        public string RainSum { get; set; }

        [JsonProperty("showers_sum")]
        public string ShowersSum { get; set; }

        [JsonProperty("snowfall_sum")]
        public string SnowfallSum { get; set; }

        [JsonProperty("precipitation_hours")]
        public string PrecipitationHours { get; set; }

        [JsonProperty("precipitation_probability_max")]
        public string PrecipitationProbabilityMax { get; set; }

        [JsonProperty("wind_speed_10m_max")]
        public string WindSpeed10mMax { get; set; }

        [JsonProperty("wind_gusts_10m_max")]
        public string WindGusts10mMax { get; set; }

        [JsonProperty("wind_direction_10m_dominant")]
        public string WindDirection10mDominant { get; set; }

        [JsonProperty("shortwave_radiation_sum")]
        public string ShortwaveRadiationSum { get; set; }

        [JsonProperty("et0_fao_evapotranspiration")]
        public string Et0FaoEvapotranspiration { get; set; }
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

        [JsonProperty("snowfall")]
        public List<int?> Snowfall { get; set; }

        [JsonProperty("snow_depth")]
        public List<int?> SnowDepth { get; set; }

        [JsonProperty("weather_code")]
        public List<int?> WeatherCode { get; set; }

        [JsonProperty("pressure_msl")]
        public List<double?> PressureMsl { get; set; }

        [JsonProperty("surface_pressure")]
        public List<double?> SurfacePressure { get; set; }

        [JsonProperty("cloud_cover")]
        public List<int?> CloudCover { get; set; }

        [JsonProperty("cloud_cover_low")]
        public List<int?> CloudCoverLow { get; set; }

        [JsonProperty("cloud_cover_mid")]
        public List<int?> CloudCoverMid { get; set; }

        [JsonProperty("cloud_cover_high")]
        public List<int?> CloudCoverHigh { get; set; }

        [JsonProperty("visibility")]
        public List<int?> Visibility { get; set; }

        [JsonProperty("evapotranspiration")]
        public List<int?> Evapotranspiration { get; set; }

        [JsonProperty("et0_fao_evapotranspiration")]
        public List<double?> Et0FaoEvapotranspiration { get; set; }

        [JsonProperty("vapour_pressure_deficit")]
        public List<double?> VapourPressureDeficit { get; set; }

        [JsonProperty("wind_speed_10m")]
        public List<double?> WindSpeed10m { get; set; }

        [JsonProperty("wind_speed_80m")]
        public List<double?> WindSpeed80m { get; set; }

        [JsonProperty("wind_speed_120m")]
        public List<double?> WindSpeed120m { get; set; }

        [JsonProperty("wind_speed_180m")]
        public List<double?> WindSpeed180m { get; set; }

        [JsonProperty("wind_direction_10m")]
        public List<int?> WindDirection10m { get; set; }

        [JsonProperty("wind_direction_80m")]
        public List<int?> WindDirection80m { get; set; }

        [JsonProperty("wind_direction_120m")]
        public List<int?> WindDirection120m { get; set; }

        [JsonProperty("wind_direction_180m")]
        public List<int?> WindDirection180m { get; set; }

        [JsonProperty("wind_gusts_10m")]
        public List<double?> WindGusts10m { get; set; }

        [JsonProperty("temperature_80m")]
        public List<double?> Temperature80m { get; set; }

        [JsonProperty("temperature_120m")]
        public List<double?> Temperature120m { get; set; }

        [JsonProperty("temperature_180m")]
        public List<double?> Temperature180m { get; set; }

        [JsonProperty("soil_temperature_0cm")]
        public List<double?> SoilTemperature0cm { get; set; }

        [JsonProperty("soil_temperature_6cm")]
        public List<double?> SoilTemperature6cm { get; set; }

        [JsonProperty("soil_temperature_18cm")]
        public List<double?> SoilTemperature18cm { get; set; }

        [JsonProperty("soil_temperature_54cm")]
        public List<double?> SoilTemperature54cm { get; set; }

        [JsonProperty("soil_moisture_0_to_1cm")]
        public List<double?> SoilMoisture0To1cm { get; set; }

        [JsonProperty("soil_moisture_1_to_3cm")]
        public List<double?> SoilMoisture1To3cm { get; set; }

        [JsonProperty("soil_moisture_3_to_9cm")]
        public List<double?> SoilMoisture3To9cm { get; set; }

        [JsonProperty("soil_moisture_9_to_27cm")]
        public List<double?> SoilMoisture9To27cm { get; set; }

        [JsonProperty("soil_moisture_27_to_81cm")]
        public List<double?> SoilMoisture27To81cm { get; set; }
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

        [JsonProperty("snowfall")]
        public string Snowfall { get; set; }

        [JsonProperty("snow_depth")]
        public string SnowDepth { get; set; }

        [JsonProperty("weather_code")]
        public string WeatherCode { get; set; }

        [JsonProperty("pressure_msl")]
        public string PressureMsl { get; set; }

        [JsonProperty("surface_pressure")]
        public string SurfacePressure { get; set; }

        [JsonProperty("cloud_cover")]
        public string CloudCover { get; set; }

        [JsonProperty("cloud_cover_low")]
        public string CloudCoverLow { get; set; }

        [JsonProperty("cloud_cover_mid")]
        public string CloudCoverMid { get; set; }

        [JsonProperty("cloud_cover_high")]
        public string CloudCoverHigh { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("evapotranspiration")]
        public string Evapotranspiration { get; set; }

        [JsonProperty("et0_fao_evapotranspiration")]
        public string Et0FaoEvapotranspiration { get; set; }

        [JsonProperty("vapour_pressure_deficit")]
        public string VapourPressureDeficit { get; set; }

        [JsonProperty("wind_speed_10m")]
        public string WindSpeed10m { get; set; }

        [JsonProperty("wind_speed_80m")]
        public string WindSpeed80m { get; set; }

        [JsonProperty("wind_speed_120m")]
        public string WindSpeed120m { get; set; }

        [JsonProperty("wind_speed_180m")]
        public string WindSpeed180m { get; set; }

        [JsonProperty("wind_direction_10m")]
        public string WindDirection10m { get; set; }

        [JsonProperty("wind_direction_80m")]
        public string WindDirection80m { get; set; }

        [JsonProperty("wind_direction_120m")]
        public string WindDirection120m { get; set; }

        [JsonProperty("wind_direction_180m")]
        public string WindDirection180m { get; set; }

        [JsonProperty("wind_gusts_10m")]
        public string WindGusts10m { get; set; }

        [JsonProperty("temperature_80m")]
        public string Temperature80m { get; set; }

        [JsonProperty("temperature_120m")]
        public string Temperature120m { get; set; }

        [JsonProperty("temperature_180m")]
        public string Temperature180m { get; set; }

        [JsonProperty("soil_temperature_0cm")]
        public string SoilTemperature0cm { get; set; }

        [JsonProperty("soil_temperature_6cm")]
        public string SoilTemperature6cm { get; set; }

        [JsonProperty("soil_temperature_18cm")]
        public string SoilTemperature18cm { get; set; }

        [JsonProperty("soil_temperature_54cm")]
        public string SoilTemperature54cm { get; set; }

        [JsonProperty("soil_moisture_0_to_1cm")]
        public string SoilMoisture0To1cm { get; set; }

        [JsonProperty("soil_moisture_1_to_3cm")]
        public string SoilMoisture1To3cm { get; set; }

        [JsonProperty("soil_moisture_3_to_9cm")]
        public string SoilMoisture3To9cm { get; set; }

        [JsonProperty("soil_moisture_9_to_27cm")]
        public string SoilMoisture9To27cm { get; set; }

        [JsonProperty("soil_moisture_27_to_81cm")]
        public string SoilMoisture27To81cm { get; set; }
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

