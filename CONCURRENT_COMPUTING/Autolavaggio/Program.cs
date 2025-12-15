
// Scrivere un programma multithreading console C# che simuli il funzionamento di un autolavaggio mediante Task.


// L’autolavaggio è costituito da un tunnel nel quale possono entrare le macchine una alla volta. Davanti al tunnel c’è un parcheggio nel quale possono entrare al massimo 20 macchine.
// Se una macchina arriva, c’è posto nel parcheggio ed è aperto, entra e aspetta di entrare nel tunnel
// Se una macchina arriva e l’autolavaggio è chiuso, oppure è aperto ma non c’è posto nel parcheggio, va via
// Le macchine arrivano con un intervallo di tempo variabile casualmente tra 0,1 e 0,3 secondi
// Quando una macchina entra nel tunnel, il programma stampa: “la macchina con indice i su Task id = TaskId entra nel tunnel di lavaggio, ho atteso x ms da quando sono entrata”
// Il tempo di permanenza nel tunnel è di 400 ms
// Quando esce, il programma stampa: “la macchina i-ma esce dal tunnel pulita e va via”
// Se l’autolavaggio viene chiuso ma ci sono ancora auto in attesa, il sistema lava tutte le auto nel parcheggio. Quando non ci sono più macchine in attesa e il parcheggio è chiuso, il sistema termina.

// Il Main program fa partire il Task che simula il tunnel di lavaggio e 50 Task che simulano le auto che arrivano, quindi va in sleep per qualche secondo, poi chiude l’ autolavaggio e aspetta che il sistema finisca.

class CarData
{
        public int IndiceAuto { get; set; }
        public long CurrentTimeStamp { get; set; }
}
internal class Program
{
    //sezione variabili condivise 
    const int NumberOfPlaces = 20;
    static int freePlaces = NumberOfPlaces;
    static bool carWashOpen ;
    
    const int NumberOfCars = 50;
    const int TimeTunnel = 400;
    const int MinCarArrivals = 100;
    const int MaxCarArrivals = 300;

    private static readonly Lock _freePlacesLock = new();
    private static readonly Lock _carWashOpenLock = new();


    static SemaphoreSlim TunnelReady = new(0,1);
    static SemaphoreSlim CarReady = new(0, NumberOfCars);


    private static void Main(string[] args)
    {
        lock (_carWashOpenLock)
        {
            carWashOpen = true;
            System.Console.WriteLine("HO APERTO L'AUTOLAVAGGIO DOPO CI VEDIAMO ALLO TSUNAMI CLUB");
        }
        Random gen = new();
        //il riferimenti al tunnel seve per poter aspettare la conclusione dei task
        Task Tunnel = Task.Factory.StartNew(tunnelAction);
        //creiamo i task che simulano le auto che arrivano
        for (int i = 0;i < NumberOfCars; i++)
        {
             Task.Factory.StartNew(CarAction,new CarData()
             {
                 CurrentTimeStamp = DateTime.Now.Ticks,
                 IndiceAuto = i,
             });
             int millis = gen.Next(MinCarArrivals,MaxCarArrivals+1);
             Task.Delay(millis).Wait();

        }
        Task.Delay(1000).Wait();
        //chiudo l'autolavaggio
        lock (_carWashOpenLock)
        {
            carWashOpen = false;
            System.Console.WriteLine("HO CHIUSO L'AUTOLAVAGGIO CIAONE E SALUTATI CARI CI VEDIAMO ALLO TSUNAMI CLUB");
        }
        Tunnel.Wait();
        System.Console.WriteLine("L'AUTOLAVAGGIO HA TERMINATO LA SUA ATTIVITA'");
    }

    private static void CarAction(Object? s)
    {
        throw new NotImplementedException();
    }

    private static void tunnelAction()
    {
        bool carWashStilOpen;
        lock (_carWashOpenLock)
        {
            carWashStilOpen=carWashOpen;
        }
        while (carWashStilOpen || CarReady.CurrentCount> 0)
        {
            CarReady.Wait();
            lock (_freePlacesLock)
            {
                freePlaces++;
                System.Console.WriteLine($"Una mcchina entra nel tunnel posti liberi {freePlaces}");
            }
            //il lavaggio dura 40 ms
            Task.Delay(TimeTunnel).Wait();
            TunnelReady.Release();
        }
    }
}