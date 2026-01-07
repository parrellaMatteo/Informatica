using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessoProduttivo
{
    internal class Program
    {
        const int CapacitaNastro = 10;  // Capacità massima del nastro trasportatore
        const int NumeroPezzi = 25;     // Numero totale di pezzi da produrre
        const int TempoPressa = 500;    // Tempo pressa in millisecondi
        const int TempoVerniciatrice = 700; // Tempo verniciatrice in millisecondi

        // Semafori per gestire il nastro trasportatore
        static readonly SemaphoreSlim postiLiberi = new(CapacitaNastro, CapacitaNastro); // Posti disponibili
        static readonly SemaphoreSlim pezziPronti = new(0, CapacitaNastro); // Pezzi sul nastro

        // Coda FIFO per il nastro trasportatore
        static readonly Queue<int> nastroTrasportatore = new();
        static readonly object lockNastro = new(); // Lock per proteggere l'accesso alla coda

        static void Main(string[] args)
        {
            Console.WriteLine("=== AVVIO PROCESSO PRODUTTIVO ===\n");

            // Creo i task per pressa e verniciatrice
            Task pressa = Task.Factory.StartNew(Pressa);
            Task verniciatrice = Task.Factory.StartNew(Verniciatrice);

            // Attendo il completamento di entrambi i task
            Task.WaitAll(pressa, verniciatrice);

            Console.WriteLine("\n=== PROCESSO PRODUTTIVO COMPLETATO ===");
            Console.WriteLine($"Totale pezzi prodotti e verniciati: {NumeroPezzi}");
        }

        /// <summary>
        /// Task che simula la pressa (produttore)
        /// </summary>
        static void Pressa()
        {
            for (int i = 1; i <= NumeroPezzi; i++)
            {
                // Attendo che ci sia spazio disponibile sul nastro
                postiLiberi.Wait();

                // Simulo il tempo di pressatura
                Task.Delay(TempoPressa).Wait();

                // Metto il pezzo sul nastro trasportatore (sezione critica)
                lock (lockNastro)
                {
                    nastroTrasportatore.Enqueue(i);
                    Console.WriteLine($"Pressa: prodotto il pezzo {i}-mo (sul nastro: {nastroTrasportatore.Count})");
                }

                // Segnalo che c'è un nuovo pezzo pronto sul nastro
                pezziPronti.Release();
            }

            Console.WriteLine("\n[Pressa] Produzione completata");
        }

        /// <summary>
        /// Task che simula la verniciatrice (consumatore)
        /// </summary>
        static void Verniciatrice()
        {
            for (int i = 1; i <= NumeroPezzi; i++)
            {
                // Attendo che ci sia un pezzo disponibile sul nastro
                pezziPronti.Wait();

                // Prelevo il pezzo dal nastro trasportatore (sezione critica)
                int numeroPezzo;
                lock (lockNastro)
                {
                    numeroPezzo = nastroTrasportatore.Dequeue();
                }

                // Simulo il tempo di verniciatura
                Task.Delay(TempoVerniciatrice).Wait();

                Console.WriteLine($"Verniciatrice: verniciato il pezzo {numeroPezzo}-mo");

                // Segnalo che si è liberato un posto sul nastro
                postiLiberi.Release();
            }

            Console.WriteLine("\n[Verniciatrice] Verniciatura completata");
        }
    }
}