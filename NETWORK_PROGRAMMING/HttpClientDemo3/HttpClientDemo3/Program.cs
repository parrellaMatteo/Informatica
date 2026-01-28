using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using TaskParallelismControl;
//namespace della libreria creata
namespace HttpClientDemo3;

class Program
{

    /// <summary>
    /// Scarica un file dalla rete e restituisce la lunghezza in byte
    /// </summary>
    /// <param name="url"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    static async Task<int> ProcessURLAsync(string url, HttpClient client)
    {
        var sw = new Stopwatch();
        sw.Start();
        var byteArray = await client.GetByteArrayAsync(url);
        sw.Stop();
        DisplayResults(url, "https://docs.microsoft.com/en-us/", byteArray, sw.ElapsedMilliseconds);
        return byteArray.Length;
    }
    /// <summary>
    /// Stampa una parte dell'url, la dimensione in byte di una pagina e il tempo impiegato per il download
    /// </summary>
    /// <param name="url"></param>
    /// <param name="urlHeadingStrip"></param>
    /// <param name="content"></param>
    /// <param name="elapsedMillis"></param>
    static void DisplayResults(string url, string urlHeadingStrip, byte[] content, long elapsedMillis)
    {
        // Display the length of each website.
        var bytes = content.Length;
        // Strip off the "urlHeadingStrip" part from url
        var displayURL = url.Replace(urlHeadingStrip, "");
        Console.WriteLine($"{Environment.NewLine}{displayURL,-80} bytes: {bytes,-10} ms: {elapsedMillis,-10}");
    }
    /// <summary>
    /// Restituisce una lista di url
    /// </summary>
    /// <returns></returns>
    static List<string> SetUpURLList()
    {
        List<string> urls =
        [
        "https://docs.microsoft.com/en-us/welcome-to-docs",
        "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/",
        "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/",
        "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/linq-to-objects",
        "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/linq-and-strings",
        "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/linq-to-xml-overview",
        "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/async-return-types",
        "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/linq-to-xml-vs-dom",
        "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection"
        ];
        return urls;
    }
    /// <summary>
    /// Effettua il setup di una lista di url e per ognuno di essi avvia un download asincrono su un task separato
    /// </summary>
    /// <returns></returns>
    static async Task SumPageSizesAsync()
    {
        // Make a list of web addresses.
        List<string> urlList = SetUpURLList();
        HttpClient client = new ();
        //misuriamo il tempo complessivo per scaricare tutte le pagine
        var swGlobal = new Stopwatch();
        swGlobal.Start();
        //processiamo in parallelo una lista di URL
        IEnumerable<Task<int>> downloadTasks = urlList.Select(u => ProcessURLAsync(u, client));
        //attendiamo il completamento di tutti i download

        await Task.WhenAll(downloadTasks);
        //sommiamo i risultati
        int total = downloadTasks.Sum(t => t.Result);
        Console.WriteLine($"{Environment.NewLine}Total bytes returned: {total}{Environment.NewLine}");
        swGlobal.Stop();
        long elapsedTotalMs = swGlobal.ElapsedMilliseconds;
        Console.WriteLine($"Tempo complessivo di scaricamento = {elapsedTotalMs}");
        //ora facciamo la stessa cosa ma limitando il grado di parallelismo
        Console.WriteLine($"{Environment.NewLine}Esecuzione con grado di parallelismo limitato:");
        swGlobal.Restart();

        //ConcurrentBag<int> bag = [];
        //await urlList.ExecuteInParallel(async u => { bag.Add(await ProcessURLAsync(u, client)); }, 10);
        //var theTotal = bag.ToArray().Sum();
        //await urlList.ExecuteInParallel(async u => { await Task.Delay(10); }, 10);
        //definiamo il grado di parallelismo
        const int numberOfParallelThreads = 5;
        //processiamo tutti gli oggetti della collection con il grado di parallelismo massimo predefinito
        ConcurrentBag<int> concurrentBagOfResults = await urlList.ExecuteInParallel(u => ProcessURLAsync(u, client), numberOfParallelThreads);
        //sommiamo tutti i valori restituiti dai thread
        var theTotal = concurrentBagOfResults.ToArray().Sum();
        Console.WriteLine($"Somma = {theTotal}");
        swGlobal.Stop();
        elapsedTotalMs = swGlobal.ElapsedMilliseconds;
        // Display the total count for all of the web addresses.
        Console.WriteLine($"{Environment.NewLine}Total bytes returned: {theTotal}{Environment.NewLine}");
        Console.WriteLine($"Tempo complessivo di scaricamento = {elapsedTotalMs}");
    }
    static async Task Main(string[] args)
    {
        //imposto la dimensione della console - vale solo per Windows
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WindowWidth = 120;
        }
        await SumPageSizesAsync();
    }
}


// metti la reference al secondo progetto, fai clic sinistro su questo progetto > add project reference e selezioni il secondo progetto