//https://docs.microsoft.com/it-it/dotnet/csharp/programming-guide/concepts/async/using-async-for-file-access

//https://docs.microsoft.com/it-it/dotnet/csharp/programming-guide/concepts/async/using-async-for-file-access

using System.Text;
namespace FileDownloadAsyncDemo
{
    class Program
    {
        /// <summary>
        /// Scrive del testo su un file usando la codifica specificata
        /// https://stackoverflow.com/questions/11774827/writing-to-a-file-asynchronously/
        /// https://stackoverflow.com/a/22617832
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        static async Task WriteTextAsync(string filePath, string text, Encoding encoding, int writeBufferSize = 4096)
        {
            using FileStream sourceStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None,
            bufferSize: writeBufferSize, useAsync: true);
            using StreamWriter sw = new(sourceStream, encoding);
            await sw.WriteAsync(text);
        }
        /// <summary>
        /// Legge un file di testo usando la codifica specificata
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <param name="readBufferSize"></param>
        /// <returns></returns>
        static async Task<string> ReadTextAsync2(string filePath, Encoding? encoding = null, int readBufferSize = 4096)
        {
            encoding ??= new UTF8Encoding(false);
            using FileStream sourceStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: readBufferSize, useAsync: true);
            StringBuilder sb = new();
            byte[] buffer = new byte[0x1000];
            int numRead;
            while ((numRead = await sourceStream.ReadAsync(buffer.AsMemory(0, buffer.Length))) != 0)
            {
                string text = encoding.GetString(buffer, 0, numRead);
                sb.Append(text);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Legge un file di testo usando la codifica specificata
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        static async Task<string> ReadTextAsync(string filePath, Encoding encoding, int readBufferSize = 4096)
        {
            using FileStream sourceStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: readBufferSize, useAsync: true);
            using StreamReader sr = new(sourceStream, encoding);
            //https://docs.microsoft.com/it-it/dotnet/standard/io/how-to-read-text-from-a-file
            return await sr.ReadToEndAsync();
        }
        /// <summary>
        /// Recupera il nome del file dall'url
        /// https://stackoverflow.com/a/40361205
        /// https://stackoverflow.com/questions/1105593/get-file-name-from-uri-string-in-c-sharp
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static string GetFileNameFromUrl(string url)
        {
            Uri SomeBaseUri = new("http://canbeanything");
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                uri = new Uri(SomeBaseUri, url);
            }
            //Path.GetFileName funziona se ha in input un URL assoluto
            return Path.GetFileName(uri.LocalPath);
        }
        static async Task Main(string[] args)
        {
            HttpClient client = new();
            try
            {
                //https://www.gutenberg.org/files/1012/1012-0.txt - La Divina Commedia in txt
                string fileName = GetFileNameFromUrl("https://www.gutenberg.org/files/1012/1012-0.txt");
                HttpResponseMessage response = await client.GetAsync("https://www.gutenberg.org/files/1012/1012-0.txt");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //salvataggio su file
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + Path.DirectorySeparatorChar + fileName;
                //uso diversi modi per salvare su file
                //metodo sincrono
                //File.WriteAllText(path, responseBody);
                //oppure, metodo asincrono
                //await File.WriteAllTextAsync(path, responseBody);
                //metodo sincrono con encoding specificato
                //File.WriteAllText(path, responseBody, new UTF8Encoding(false));
                //oppure, metodo asincrono con encoding specificato
                //await File.WriteAllTextAsync(path, responseBody, new UTF8Encoding(false));

                // https://stackoverflow.com/questions/2223882/whats-the-difference-between-utf-8-and-utf-8-without-bom
                //encoding senza BOM - https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding.utf8?view=netframework-4.8
                //BOM https://stackoverflow.com/a/2223926
                //BOM https://it.wikipedia.org/wiki/Byte_Order_Mark

                //Oppure uso le funzioni di lettura/scrittura asincrona definite sopra
                Console.WriteLine("Salvataggio su file in corso...");
                await WriteTextAsync(path, responseBody, new UTF8Encoding(false));
                Console.WriteLine("Lettura da file in corso... dei primi 10000 caratteri...tutto non entra nel buffer della console");
                string testoLettoDaFile = await ReadTextAsync(path, new UTF8Encoding(false));
                //oppure usando i metodi di File
                //string testoLettoDaFile2 = await File.ReadAllTextAsync( path, new UTF8Encoding(false));
                //oppure usando la seconda versione di ReadTextAsync
                //string testoLettoDaFile3 = await File.ReadAllTextAsync(path);
                Console.WriteLine(testoLettoDaFile[..10000]);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"{Environment.NewLine}Exception Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}