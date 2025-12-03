namespace ProduttoreConsumatoreFIFO
{
    internal class Program
    {

        //FIRST IN FIRST OUT


        //Area dati statica condivisa
        static int BufferSize = 10; //Dimensione buffer condiviso
        static int[] buffer = new int[BufferSize]; //Buffer condiviso

        //semafori
        static SemaphoreSlim emptyPosCount = new(BufferSize, BufferSize); //semaforo che conta le posizioni vuote (inizialmente tutte vuote)
        static SemaphoreSlim fillPosCount = new(0, BufferSize); //semaforo che conta le posizioni piene (inizialmente nessuna)

        //lock per sezione critica
        static readonly object _lock = new();  // _ => variabili locali private

        //prima posizione libera
        static int writePos = 0;
        static int readPos = 0;

        static void Main(string[] args)
        {
            //creo il thread produttore e quello consumatore
            Thread produttore = new(ProducerWork) { Name = "Produttore" };
            Thread consumatore = new(ConsumerWork) { Name = "Consumatore" };

            //faccio partire i due thread
            produttore.Start();
            consumatore.Start();

            //attendo che i due thread finiscano
            produttore.Join();
            consumatore.Join();

            Console.WriteLine("Fine del programma zio pera");

        }

        private static void ConsumerWork(object? obj)
        {
            while (true)
            {
                //per consumare ci deve essere qualcosa da consumare. il semaforo mi fa consumare? c'è qualcosa in fillposcount()?
                fillPosCount.Wait();

                //entro in sezione critica
                lock (_lock)
                {
                    //lettura nel buffer condiviso
                    buffer[readPos] = 0; //pre incremento
                    Console.WriteLine("Consumato prodotto alla posizione {0} da thread id = {1}, thread name = {2}",
                        readPos,
                        Environment.CurrentManagedThreadId,
                        Thread.CurrentThread.Name);
                    //modulo % per dire che appena arriva a buffer size viene messo a zero per ricominciare da capo a leggere
                    readPos = (readPos+1)%BufferSize;
                    PrintArray(buffer);
                }

                //finita la sezione critica.
                //il consumatore ha consumato e segnala all'altro thread che è stato svuotato un elemento
                emptyPosCount.Release();

                //rallento
                Thread.Sleep(2500);
            }
        }

        private static void ProducerWork(object? obj)
        {
            while (true)
            {
                //per produrre ci deve essere spazio. il semaforo mi fa passare? c'è spazio in emptyposcount()?
                emptyPosCount.Wait();

                //entro in sezione critica
                lock (_lock)
                {
                    //scrittura nel buffer condiviso
                    buffer[writePos] = 1; //post incremento
                    Console.WriteLine("Aggiunto prodotto alla posizione {0} da thread id = {1}, thread name = {2}",
                        writePos,
                        Environment.CurrentManagedThreadId,
                        Thread.CurrentThread.Name);

                    //modulo % per dire che appena arriva a buffer size viene messo a zero per ricominciare da capo a scrivere
                    writePos = (writePos+1)%BufferSize;
                    PrintArray(buffer);
                }

                //finita la sezione critica.
                //il produttore ha prodotto e segnala all'altro thread che è stato prodotto un elemento
                fillPosCount.Release();

                //rallento
                Thread.Sleep(1000);
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
