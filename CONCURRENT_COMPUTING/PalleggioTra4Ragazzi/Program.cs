using System;
using System.Threading;
using System.Threading.Tasks;

namespace VolleyWarmUp
{
    class GiocatoreData
    {
        public string? MyName { get; set; }
        public string? ToName { get; set; }
        public SemaphoreSlim? CanIPlay { get; set; }
        public SemaphoreSlim? PassToNext { get; set; }
    }

    internal class Program
    {
        // Semafori per coordinare i turni di gioco
        static SemaphoreSlim giocaGiovanni = new(1, 1); // Giovanni inizia il gioco
        static SemaphoreSlim giocaMattia = new(0, 1);
        static SemaphoreSlim giocaAlessandro = new(0, 1);
        static SemaphoreSlim giocaRoberto = new(0, 1);

        // Flag per terminare il gioco
        static bool fischioAllenatore = false;
        private static readonly Lock _lockFischioAllenatore = new();

        static void Main(string[] args)
        {
            // Il main fa partire l'attività di palleggio per ogni giocatore
            Task giovanni = Task.Factory.StartNew(WarmUp,
                new GiocatoreData() {
                    MyName = "Giovanni",
                    ToName = "Mattia",
                    CanIPlay = giocaGiovanni,
                    PassToNext = giocaMattia
                });

            Task mattia = Task.Factory.StartNew(WarmUp,
                new GiocatoreData() {
                    MyName = "Mattia",
                    ToName = "Alessandro",
                    CanIPlay = giocaMattia,
                    PassToNext = giocaAlessandro
                });

            Task alessandro = Task.Factory.StartNew(WarmUp,
                new GiocatoreData() {
                    MyName = "Alessandro",
                    ToName = "Roberto",
                    CanIPlay = giocaAlessandro,
                    PassToNext = giocaRoberto
                });

            Task roberto = Task.Factory.StartNew(WarmUp,
                new GiocatoreData() {
                    MyName = "Roberto",
                    ToName = "Giovanni",
                    CanIPlay = giocaRoberto,
                    PassToNext = giocaGiovanni
                });

            // Attendo 10 secondi per il riscaldamento
            Task.Delay(10000).Wait();
            Console.WriteLine("Riscaldamento terminato, fischio di fine gioco");

            // Segnalo la fine del gioco
            lock (_lockFischioAllenatore)
            {
                fischioAllenatore = true;
            }

            // Attendo che tutti i task terminino
            Task.WaitAll(giovanni, mattia, alessandro, roberto);
        }

        private static void WarmUp(object? obj)
        {
            if (obj is GiocatoreData data)
            {
                Random gen = new();
                bool haFischiato;

                // Verifico se l'allenatore ha già fischiato
                lock (_lockFischioAllenatore)
                {
                    haFischiato = fischioAllenatore;
                }

                while (!haFischiato)
                {
                    // Attendo il mio turno
                    data?.CanIPlay?.Wait();

                    // Simulo il tempo di palleggio
                    Task.Delay(gen.Next(100, 301)).Wait();
                    Console.WriteLine($"Sono {data?.MyName} e passo la palla a {data?.ToName}");

                    // Segnalo al prossimo giocatore che può giocare
                    data?.PassToNext?.Release();

                    // Verifico se l'allenatore ha fischiato
                    lock(_lockFischioAllenatore)
                    {
                        haFischiato = fischioAllenatore;
                    }
                }
            }
        }
    }
}