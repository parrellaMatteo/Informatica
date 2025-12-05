using Microsoft.VisualBasic;

internal class Program
{
    //Dati condivisi
    const int NumberOfChildren = 50;
    const int NumberOfCarouselSeats = 5;
    static readonly Random gen = new();
    static readonly Lock _genLock  = new();
    //lock per accedere al generatore
    //semaforo inizializzato con 
    //valore iniziale delle variabili di conteggio
    //valore massimo che tale variabile puo assumere
    static readonly SemaphoreSlim postiLiberi = new(NumberOfCarouselSeats,NumberOfCarouselSeats);

    private static void Main(string[] args)
    {
        for (int i = 0;i < NumberOfChildren; i++)
        {
            new Thread(CarouselRide).Start(i);
            Thread.Sleep(100);
        }
    }

    private static void CarouselRide(object? obj)
    {
        //as fa il cast se il cast è possbile
        //as se il cast non è possibile restituisce null
        int? parametro = obj as int?;
        int indice = -1;
        if(parametro != null)
        {
            indice = (int)parametro;
        }
        System.Console.WriteLine($"la persona {indice} si mette in coda ");
        postiLiberi.Wait();
        //il bambino è salito sulla giostra 
        System.Console.WriteLine($"la persona {indice} sale sulla giostra  e i posti liberi sono {postiLiberi.CurrentCount}");
        //funzione random non è trade safe
        int elapsedMilliseconds = 10;
        lock (_genLock)
        {
            elapsedMilliseconds = gen.Next(1000,3001);
        }
        Thread.Sleep(elapsedMilliseconds);
        System.Console.WriteLine($"la persona {indice} esce dalla giostra ");
        postiLiberi.Release();
    }
}