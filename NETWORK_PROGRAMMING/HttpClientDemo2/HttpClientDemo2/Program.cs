//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
using System.Diagnostics;
using System.Runtime.InteropServices;
 
namespace HttpClientDemo2;
 
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
        //setup del client con eventuale Proxy
        HttpClient client = new();
 
        //misuriamo il tempo complessivo per scaricare tutte le pagine
        var swGlobal = new Stopwatch();
        swGlobal.Start();
        //processiamo in parallelo una lista di URL
        // Materializziamo subito per evitare enumerazioni multiple accidentali.
 
 
        //ProcessURLAsync restituisce un Task<int> per ogni URL della lista
        var mieiTask = urlList.Select(u => ProcessURLAsync(u, client)).ToList();
        await Task.WhenAll(mieiTask);
        await Task.WhenAny(mieiTask);
        //devo attedere il completamento di tutti i task
        Task<int>[] downloadTasks = [.. urlList.Select(u => ProcessURLAsync(u, client))];
 
        //*****************************************************************************
        // //altro modo per processare in parallelo più attività è il seguente:
        // // ATTENZIONE: List<T> non è thread-safe. Usiamo un array indicizzato.
        // var completedDownloads = new Task<int>[urlList.Count];
        // Parallel.For(0, urlList.Count, index =>
        // {
        //     completedDownloads[index] = ProcessURLAsync(urlList[index], client);
        // });
        // // You can do other work here before awaiting.
        // int[] lengthsParallel = await Task.WhenAll(completedDownloads);
        // int totalParallel = lengthsParallel.Sum();
        // Console.WriteLine($"{Environment.NewLine}Total bytes returned (Parallel.For): {totalParallel}{Environment.NewLine}");
        // //altro modo per processare in parallelo più attività è il seguente:
        // // ATTENZIONE: List<T>.Add non è thread-safe. Usiamo l'overload con indice + array.
        // var completedDownloads2 = new Task<int>[urlList.Count];
        // Parallel.ForEach(urlList, (url, _, index) =>
        // {
        //     completedDownloads2[(int)index] = ProcessURLAsync(url, client);
        // });
        // // You can do other work here before awaiting.
        // int[] lengthsParallelForEach = await Task.WhenAll(completedDownloads2);
        // int totalParallelForEach = lengthsParallelForEach.Sum();
        // Console.WriteLine($"{Environment.NewLine}Total bytes returned (Parallel.ForEach): {totalParallelForEach}{Environment.NewLine}");
        // Await the completion of all the running tasks.
        //*****************************************************************************
 
        int[] lengths = await Task.WhenAll(downloadTasks);
        //// The previous line is equivalent to the following two statements.
        //Task<int[]> whenAllTask = Task.WhenAll(downloadTasks);
        //int[] lengths = await whenAllTask;
        swGlobal.Stop();
        long elapsedTotalMs = swGlobal.ElapsedMilliseconds;
        int total = lengths.Sum();
        // Display the total count for all of the web addresses.
        Console.WriteLine($"{Environment.NewLine}Total bytes returned: {total}{Environment.NewLine}");
        Console.WriteLine($"Tempo complessivo di scaricamento = {elapsedTotalMs} ms");
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