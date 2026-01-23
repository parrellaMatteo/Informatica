namespace HttpResponseAnalysis
{
    class Program
    {

        static async Task Main()
        {
            // HttpClient is intended to be instantiated once per application, rather than per-use.
            HttpClient client = new();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //invio richiesta Get in modalità Async e ottengo la risposta
                HttpResponseMessage response = await client.GetAsync("https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/overview");
                //stampo lo status code
                Console.WriteLine($"status code = {response.StatusCode}");
                //stampo gli headers http della risposta
                Console.WriteLine($"{Environment.NewLine}stampa gli headers http della risposta{Environment.NewLine}");
                foreach (var header in response.Headers)
                {
                    Console.Write($"{header.Key} : ");
                    foreach (var val in header.Value)
                    {
                        Console.Write($"{val} ");
                    }
                    Console.WriteLine();
                }
                //stampo gli headers http della risposta usando il response.Headers.toString()
                //Console.WriteLine($"{Environment.NewLine}stampa di ToString() di response.Headers{Environment.NewLine}");
                //Console.WriteLine(response.Headers.ToString());
                //stampo gli headers del content
                Console.WriteLine($"{Environment.NewLine}stampa degli headers del content della risposta{Environment.NewLine}");
                foreach (var header in response.Content.Headers)
                {
                    Console.Write($"{header.Key} : ");
                    foreach (var val in header.Value)
                    {
                        Console.Write($"{val} ");
                    }
                    Console.WriteLine();
                }
                //ottenere il charset
                Console.Write($"{Environment.NewLine}Stampa del charset: ");
                Console.WriteLine(response.Content.Headers.ContentType?.CharSet);
                Console.Write($"{Environment.NewLine}Stampa del media type: ");
                Console.WriteLine(response.Content.Headers.ContentType?.MediaType);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // il contenuto di una pagina web può essere ottenuto anche con la seguente istruzione
                // string responseBody = await client.GetStringAsync(uri);
                // Console.WriteLine($"{Environment.NewLine}stampa del contenuto del body del messaggio http{Environment.NewLine}");
                // Console.WriteLine(responseBody);
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                await File.WriteAllTextAsync(Path.Combine(desktopPath, nameof(HttpResponseAnalysis) + ".html"), responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"{Environment.NewLine}Exception Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}