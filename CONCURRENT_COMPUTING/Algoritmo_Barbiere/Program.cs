
namespace Algoritmo_Barbiere
{
    internal class Program
    {
        //per la gestione della sezione critica si utilizza un semaforo
        const int numberOfSeats = 10;
        static int freeSeats = numberOfSeats;
        //seaat acces è un semaforo che funziona da mutex
        static SemaphoreSlim seatAccess = new SemaphoreSlim(1, 1);
        static SemaphoreSlim barberReady = new SemaphoreSlim(0, 1);
        static SemaphoreSlim clientReady = new SemaphoreSlim(0, numberOfSeats);
        //IMPORTANTE : wait : decrementa il contatore del semaforo 
        //IMPORTANTE : release : incrementa il contatore del semaforoS
        static void Main(string[] args)
        {
            //gestiamo la logica della simulazione
            //facciamo partire 1 thread per il barbiere
            Thread barber = new Thread(BarberWork);
            barber.Start();
            const int numberOfClients = 30;
            // Thread[] clienti = new Thread[numberOfClients];
            for (int i = 0;i < numberOfClients; i++)
            {
                // clienti[i] = new (ClientWork);
                // clienti[i].Start();
                //creo thread anonimi 
                new Thread(ClientWork).Start();
                //inseriamo un ritardo tra l'avvento di un cliente e il successivo
                Thread.Sleep(500);
            }
            barber.Join();
        }

        private static void ClientWork(object? obj)
        {
            //Il cliente quando arriva verifica se cè posto
            seatAccess.Wait();//entro in sezione critica
            if(freeSeats>0)
            {
                freeSeats--;
                System.Console.WriteLine("Ho trovato posto e mi siedo");
                //attendo che il barbiere mi faccia sedere sulla sedia di taglio
                seatAccess.Release();//esco dalla sezione critica 
                clientReady.Release();//seganlo al barbiere che sono pronto a tagliarmi i capelli
                barberReady.Wait();//attendo che il barbiere mi tagli i capelli
                
            }
            //se cè posto si siede e attende la disponibilità del barbiere
            //Se non cè posto se ne va e lascia il locale
        }

        private static void BarberWork(object? obj)
        {
            while (true)
            {
                //attendo che ci sia un cliente
                clientReady.Wait();
                //barbiere fa accomodare il cliente e libera un posto di attesa
                seatAccess.Wait();//entro in sezione critica 
                freeSeats++;
                System.Console.WriteLine("il barbiere ha liberaeto un posto");
                seatAccess.Release();//esco dalla sezione critica
                //il barbiere taglia i capelli
                System.Console.WriteLine("Il barbiere sta tagliando i capelli");
                Thread.Sleep(2000);
                barberReady.Release();//il barbiere è di nuovo disponibile
            }
        }
    }
}
