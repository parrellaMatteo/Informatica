using System;
using System.Threading;

namespace GiostraThreadConCodaFinita
{
    internal class Program
    {
        const int NumberOfChildren = 50;
        const int NumberOfCarouselSeats = 5;

        // Semaforo per i posti sulla giostra
        static readonly SemaphoreSlim postiLiberi = new(NumberOfCarouselSeats, NumberOfCarouselSeats);

        static readonly Random gen = new();
        static readonly Lock _lock = new();

        // Gestione della coda di attesa
        const int NumeroMassimoBambiniInAttesa = 10;
        static int numeroBambiniInAttesa = 0;
        // Semaforo usato come Mutex per proteggere la variabile numeroBambiniInAttesa
        static readonly SemaphoreSlim attesa = new(1, 1);

        static void Main(string[] args)
        {
            for (int i = 0; i < NumberOfChildren; i++)
            {
                new Thread(CarouselRide).Start(i);
                Thread.Sleep(100);
            }
        }

        private static void CarouselRide(object? obj)
        {
            if (obj == null) return;
            int index = (int)obj;
            bool siFerma = false;

            // Entriamo in sezione critica per verificare se c'è posto in coda
            attesa.Wait();
            if (numeroBambiniInAttesa < NumeroMassimoBambiniInAttesa)
            {
                numeroBambiniInAttesa++;
                siFerma = true;
                Console.WriteLine($"Bambino {index}: Mi metto in coda. (In attesa: {numeroBambiniInAttesa})");
            }
            attesa.Release(); // Rilasciamo subito il lock sulla coda

            if (siFerma)
            {
                Console.WriteLine($"Sono il bambino {index}-mo, con Thread Id = {Environment.CurrentManagedThreadId} e attendo di salire sulla giostra");

                // Attendiamo che si liberi un posto sulla giostra
                postiLiberi.Wait();

                // Una volta saliti, dobbiamo decrementare chi era in attesa
                attesa.Wait();
                numeroBambiniInAttesa--;
                Console.WriteLine($"Bambino {index}: Salito! (In attesa rimasti: {numeroBambiniInAttesa})");
                attesa.Release();

                Console.WriteLine($"Sono il bambino {index}-mo, con Thread Id = {Environment.CurrentManagedThreadId} e sto facendo il giro sulla giostra");

                int rideMilliSeconds;
                lock (_lock)
                {
                    rideMilliSeconds = gen.Next(1000, 3001);
                }
                Thread.Sleep(rideMilliSeconds);

                Console.WriteLine($"Sono il bambino {index}-mo, con Thread Id = {Environment.CurrentManagedThreadId} e sto liberando la giostra");
                postiLiberi.Release();
            }
            else
            {
                Console.WriteLine($"Sono il bambino {index}-mo, con Thread Id = {Environment.CurrentManagedThreadId} ho trovato troppa fila, me ne vado");
            }
        }
    }
}