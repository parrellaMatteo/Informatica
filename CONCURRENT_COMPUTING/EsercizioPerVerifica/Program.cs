
internal class Program
{
    static Lock _randomLock = new Lock();
    static Lock _lockAccensione = new Lock();
    static Random r = new();
    static bool Accensione=true;
    static readonly SemaphoreSlim impasta = new(1, 1);
    static readonly SemaphoreSlim forma = new(0, 1);
    static readonly SemaphoreSlim cuoci = new(0, 1);


    public static void Main(string[] args)
    {
        Task tImpasta = Task.Factory.StartNew(Impasta);
        Task tForma = Task.Factory.StartNew(Forma);
        Task tCuoci = Task.Factory.StartNew(Cuoci);

        Thread.Sleep(20000); 

        lock (_lockAccensione)
        {
            Accensione = false;
        }
        impasta.Release(); forma.Release(); cuoci.Release();

        Task.WaitAll(tImpasta, tForma, tCuoci);
        Console.WriteLine("Impianto fermato correttamente.");
    }

    private static int GetRandom(int min, int max)
    {
        lock (_randomLock) { return r.Next(min, max); }
    }

    private static bool IsAcceso()
    {
        lock (_lockAccensione)
        {
            return Accensione;
        }
    }

    private static void Impasta()
    {
        while (IsAcceso())
        {
            impasta.Wait();
            if (IsAcceso())
            {
                Console.WriteLine("Inizio di un nuovo impasto...");
                Thread.Sleep(GetRandom(2000, 3000));
                forma.Release();
            }
        }
        Console.WriteLine("Impasta terminato.");
    }

    private static void Forma()
    {
        while (IsAcceso())
        {
            forma.Wait();
            if (IsAcceso())
            {
                Console.WriteLine("Inizio attività di formatura dei Pangoccioli");
                Thread.Sleep(GetRandom(1500, 2000));
                cuoci.Release();
            }
        }
        Console.WriteLine("Forma terminata.");
    }

    private static void Cuoci()
    {
        while (IsAcceso())
        {
            cuoci.Wait();
            if (IsAcceso())
            {
                Console.WriteLine("Inizio fase di cottura");
                Thread.Sleep(2000);
                Console.WriteLine("Pangoccioli sfornati!");
                impasta.Release();
            }
        }
        Console.WriteLine("Cuoci terminato.");
    }
}