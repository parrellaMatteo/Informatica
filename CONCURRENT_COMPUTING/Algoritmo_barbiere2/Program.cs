using System;
using System.Threading;

namespace BarberShopV2
{
    class Program
    {
        //per la gestione della sezione critica si utilizza un Monitor (lock)
        const int numberOfSeats = 10;
        static int freeSeats = numberOfSeats;
        private static readonly Lock _lock = new();
        static readonly SemaphoreSlim barberReady = new(0, 1);
        static readonly SemaphoreSlim clientReady = new(0, numberOfSeats);

        static void Main(string[] args)
        {
            //Attivazione del thread del barbiere
            //il barbiere dorme sulla sua sedia da lavoro
            //quando arriva un cliente, il barbiere si sveglia e gli taglia i capelli
            //quando ha finito di tagliare i capelli, se c'è un altro cliente seduto
            //su una delle sedie del negozio, lo serve immediatamente, altrimenti ritorna a dormine
            //il barbiere si sveglia appena arriva un nuovo cliente
            Thread barber = new Thread(Barber);
            barber.Start();

            //Attivazione dei thread dei clienti
            //un cliente arriva in un momento qualsiasi, se trova un posto libero nella
            //sala d'attesa si siede e aspetta fino a che non viene servito; se arriva e trova
            //tutte le sedie occupate, se ne va via
            int numberOfClients = 30;
            for (int i = 0; i < numberOfClients; i++)
            {
                new Thread(Client).Start();
                //i clienti arrivano con una certa frequenza al negozio
                Thread.Sleep(500);
            }
            barber.Join();
        }

        private static void Client()
        {
            //il cliente deve verificare se c'è posto
            //per farlo deve controllare il numero di posti liberi e quindi deve accedere alla
            bool clienteSiSiede = false;
            //sezione critica
            //le operazioni in sezione critica devono durare il minimo possibile
            lock (_lock)
            {
                if (freeSeats > 0)
                {
                    //il cliente si siede ed occupa un posto
                    freeSeats--;
                    Console.WriteLine($"Il cliente con Thread Id = {Environment.CurrentManagedThreadId} trova posto e attende di essere servito;" +
                        $" posti disponibili = {freeSeats}");
                    clienteSiSiede = true;
                }
            }
            if (clienteSiSiede)
            {
                clientReady.Release();//aumenta il numero di clienti disponibili
                barberReady.Wait();//attende che il barbiere sia disponibile
                                   //il cliente viene servito
                Console.WriteLine($"Sono il cliente con ThreadId = {Environment.CurrentManagedThreadId} " +
                    $". Il Barbiere ha finito di tagliarmi i capelli.");
            }
            else
            {
                //il cliente se ne va
                Console.WriteLine($"Il cliente con ThreadId = {Environment.CurrentManagedThreadId} non ha trovato posto e se ne va");
            }
        }




        private static void Barber()
        {
            while (true)
            {
                clientReady.Wait();//attendo che ci sia un cliente
                //il cliente si accomoda sulla sedia del barbiere e libera un posto
                lock (_lock)
                {
                    freeSeats++;//il barbiere libera un posto
                    Console.WriteLine("Il barbiere libera un posto");
                }//il barbiere deve accedere alla sezione critica
                Console.WriteLine($"Il Barbiere sta tagliando i capelli");
                Thread.Sleep(2000);//il barbiere sta tagliando in capelli; questo è il tempo impiegato per tagliare i capelli
                barberReady.Release(); //il barbiere è nuovamente disponibile
            }
        }
    }
}