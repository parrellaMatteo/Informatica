using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
namespace AsyncBreakfast
{

    internal class Bacon
    {
    }
    internal class Egg
    {
    }
    internal class Coffee
    {
    }
    internal class Juice
    {
    }
    internal class Toast
    {
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Prepariamo la colazione - versione sincrona
            Console.WriteLine("Prepariamo la colazione - versione sincrona");
            ColazioneSincrona();

            //Console.WriteLine("\n\nPrepariamo la colazione - versione parallela");
            //ColazioneParallela();

            ////Prepariamo la colazione - versione asincrona
            //Console.WriteLine("\n\nPrepariamo la colazione - versione asincrona");
            //await ColazioneAsincrona();

            ////Prepariamo la colazione - versione asincrona ottimizzata
            //Console.WriteLine("\n\nPrepariamo la colazione - versione asincrona ottimizzata");
            //await ColazioneAsincronaOttimizzata();
        }

private static void ColazioneSincrona()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");
            List<Egg> eggs = FryEggs(2);
            Console.WriteLine("eggs are ready");
            List<Bacon> bacon = FryBacon(3);
            Console.WriteLine("bacon is ready");
            List<Toast> toast = ToastBread(2);
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("toast is ready");
            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
            sw.Stop();
            Console.WriteLine($"Il tempo per la colazione sincrona è {sw.ElapsedMilliseconds} ms");
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine($"Sto iniziando a preparare il caffè");
            Task.Delay(1000).Wait();
            return new Coffee();
        }

private static List<Egg> FryEggs(int v)
        {
            Console.WriteLine($"Sto iniziando a friggere {v} uova");

            List<Egg> uova = new List<Egg>();
            for (int i = 0; i < v; i++)
            {
                // https://stackoverflow.com/questions/20082221/when-to-use-task-delay-when-to-use-thread-sleep
  Task.Delay(200).Wait();
                uova.Add(new Egg());
            }
            return uova;
        }

private static List<Bacon> FryBacon(int v)
        {
            Console.WriteLine($"Sto iniziando a friggere {v} fette di pancetta");

            List<Bacon> fetteDiPancetta = new List<Bacon>();
            for (int i = 0; i < v; i++)
            {
                Task.Delay(200).Wait();
                fetteDiPancetta.Add(new Bacon());
            }
            return fetteDiPancetta;
        }

private static List<Toast> ToastBread(int v)
        {
            Console.WriteLine($"Sto iniziando a tostare {v} fette di pane");
            List<Toast> toasts = new List<Toast>();
            for (int i = 0; i < v; i++)
            {
                Console.WriteLine($"\tTosto la {i+1}-ma fetta");
                Task.Delay(200).Wait();
                toasts.Add(new Toast());
            }
            return toasts;
        }
private static void ApplyButter(List<Toast> toast)
        {
            Console.WriteLine("Sto iniziando a spalmare il burro ");
            for (int i = 0; i < toast.Count; i++)
            {
                Task.Delay(300).Wait();
                Console.WriteLine($"\tSto spalmando il burro sulla {i + 1}-ma fetta ");
            }

        }

private static void ApplyJam(List<Toast> toast)
        {
            Console.WriteLine("Sto iniziando a spalmare la marlellata ");
            for (int i = 0; i < toast.Count; i++)
            {
                Task.Delay(500).Wait();
                Console.WriteLine($"\tSto spalmando la marmellata sulla {i+1}-ma fetta");
            }

        }

private static Juice PourOJ()
        {
            Console.WriteLine("Sto iniziando a spremere le arance");
            Task.Delay(1000).Wait();
            return new Juice();
        }

    }
}