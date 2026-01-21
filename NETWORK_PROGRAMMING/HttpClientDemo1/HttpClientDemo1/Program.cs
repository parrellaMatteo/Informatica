namespace HttpClientDemo1
{
    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static HttpClient? client;
        static async Task Main()
        {
            client = new HttpClient();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/overview");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                await File.WriteAllTextAsync(Path.Combine(desktopPath, nameof(HttpClientDemo1) + ".html"), responseBody);
                // Above three lines can be replaced with new helper method below
                //string responseBody = await client.GetStringAsync(url);
                //Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"{Environment.NewLine}Exception Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}