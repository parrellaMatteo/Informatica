using System;
using System.Threading;

namespace CONCURRENT_COMPUTING
{
    class Program
    {
        // Semaforo che useremo per limitare il numero di thread che possono entrare contemporaneamente
        private static SemaphoreSlim semaphore;

        // Variabile usata per "allungare" il tempo di attesa dei thread in modo diverso
        private static int padding;

        public static void Main()
        {
            // Creazione del semaforo:
            // - conteggio iniziale = 0 (nessun thread può entrare subito)
            // - conteggio massimo = 3 (al massimo 3 thread alla volta possono essere dentro)
            semaphore = new SemaphoreSlim(0, 3);

            Console.WriteLine("{0} Thread can enter the semaphore.",
                              semaphore.CurrentCount); // Stampa il conteggio iniziale del semaforo (0)

            // Creiamo un array di 5 thread
            Thread[] threads = new Thread[5];

            // Inizializziamo i 5 thread
            for (int i = 0; i <= 4; i++)
            {
                // Ogni thread eseguirà questo codice (lambda expression)
                threads[i] = new Thread(() =>
                {
                    Console.WriteLine("Thread {0} begins and waits for the semaphore.",
                                      Thread.CurrentThread.ManagedThreadId);

                    // Il thread prova ad entrare nel semaforo
                    // Se il conteggio è 0, il thread resta bloccato finché qualcuno non fa Release()
                    semaphore.Wait();

                    // Aggiungiamo 100 alla variabile padding in modo atomico (thread-safe)
                    // Questo serve solo a far "dormire" ogni thread un po' di più
                    Interlocked.Add(ref padding, 100);

                    Console.WriteLine("Thread {0} enters the semaphore.",
                                      Thread.CurrentThread.ManagedThreadId);

                    // Il thread "lavora" per un po':
                    // 1000 ms (1 secondo) + il valore di padding (100, 200, 300, ...)
                    Thread.Sleep(1000 + padding);

                    // Quando ha finito, il thread rilascia il semaforo
                    // semaphore.Release() restituisce il conteggio precedente del semaforo
                    Console.WriteLine("Thread {0} releases the semaphore; previous count: {1}.",
                                      Thread.CurrentThread.ManagedThreadId, semaphore.Release());
                });
            }

            // Avviamo tutti i 5 thread
            foreach (var thread in threads)
            {
                thread.Start();
            }

            // Aspettiamo un attimo (mezzo secondo) per dare tempo ai thread di bloccarsi su Wait()
            Thread.Sleep(500);

            Console.Write("Main thread calls Release(3) --> ");

            // Il thread principale rilascia 3 permessi sul semaforo
            // Questo sbloccherà 3 dei thread che erano in attesa
            semaphore.Release(3);

            Console.WriteLine("{0} Threads can enter the semaphore.",
                              semaphore.CurrentCount); // Dopo il Release(3), il conteggio torna subito a 0 perché i 3 thread li consumano immediatamente

            // Il main aspetta che tutti i thread finiscano (si usa Join per ogni thread)
            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Main thread exits.");
            Console.ReadLine(); // Mantiene la console aperta finché non premi Invio
        }

    }
}
