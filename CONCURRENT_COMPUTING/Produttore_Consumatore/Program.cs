using System;
using System.Threading;

namespace Produttore_Consumatore
{
    class Program
    {
        // Oggetto lock per proteggere l'accesso alla sezione critica
        static readonly object _lock = new();

        // Dimensione del buffer condiviso
        static int BufferSize = 10;

        // Semaforo che conta le posizioni vuote (inizialmente tutte vuote)
        static SemaphoreSlim emptyPosCount = new(BufferSize, BufferSize);

        // Semaforo che conta le posizioni piene (inizialmente nessuna)
        static SemaphoreSlim fillPosCount = new(0, BufferSize);

        // Indice della prima cella vuota (comportamento LIFO)
        static int firstEmptyPos = 0;

        // Buffer condiviso
        static int[] buffer = new int[BufferSize];

        static void Main(string[] args)
        {
            // Creazione thread produttore e consumatore
            Thread producer = new(ProducerWork) { Name = "Producer" };
            Thread consumer = new(ConsumerWork) { Name = "Consumer" };

            // Avvio dei thread
            producer.Start();
            consumer.Start();

            // Attendo la fine dei thread (in questo esempio non finiranno mai)
            producer.Join();
            consumer.Join();

            Console.WriteLine("Fine");
        }

        private static void ConsumerWork()
        {
            while (true) // ciclo infinito
            {
                fillPosCount.Wait(); // aspetto che ci sia almeno una cella piena

                lock (_lock) // sezione critica: accesso al buffer
                {
                    buffer[--firstEmptyPos] = 0; // leggo (simulazione) e svuoto la cella LIFO
                    Console.WriteLine("Consumato prodotto alla posizione {0} da thread id = {1}, thread name = {2}",
                        firstEmptyPos,
                        Environment.CurrentManagedThreadId,
                        Thread.CurrentThread.Name);

                    PrintArray(buffer); // stampa buffer dopo il consumo
                }

                emptyPosCount.Release(); // segnalo che una cella vuota è disponibile

                Thread.Sleep(2500); // pausa per simulare lavoro lento del consumatore
            }
        }

        private static void ProducerWork()
        {
            while (true) // ciclo infinito
            {
                emptyPosCount.Wait(); // aspetto che ci sia almeno una cella vuota

                lock (_lock) // sezione critica: accesso al buffer
                {
                    buffer[firstEmptyPos] = 1; // inserisco un elemento nel buffer
                    Console.WriteLine("Aggiunto prodotto alla posizione {0} da thread id = {1}, thread name = {2}",
                        firstEmptyPos,
                        Environment.CurrentManagedThreadId,
                        Thread.CurrentThread.Name);

                    PrintArray(buffer); // stampa buffer dopo il consumo

                    firstEmptyPos++; // incremento l'indice (LIFO)
                }

                fillPosCount.Release(); // segnalo che una cella piena esiste

                Thread.Sleep(1000); // pausa per simulare lavoro più veloce del produttore
            }
        }

        static void PrintArray(int[] array)
        {
            // Stampa tutto il buffer
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}
