
internal class Program
{
    static readonly SemaphoreSlim goA = new(1,1);//puo partire subito e puo stampare una volta
    static readonly SemaphoreSlim goB = new(0,2);//deve asppettare il segnale da A e puo stampare una volta
    static readonly SemaphoreSlim goC = new(0,1);


    private static void Main(string[] args)
    {
        Thread threadA = new(AWork);
        Thread threadB = new(BWork);
        Thread threadC = new(CWork);
        threadA.Start();
        threadB.Start();
        threadC.Start();
        threadA.Join();
        threadB.Join();
        threadC.Join();

    }

    private static void CWork(object? obj)
    {
       while (true)
        {
            goC.Wait();
            System.Console.WriteLine("C");
            goA.Release();
        }
    }

    private static void BWork(object? obj)
    {
       while (true)
        {
            goB.Wait();
            Console.WriteLine("B");
            if(goB.CurrentCount==0)
            {
                goC.Release()
            }
        }
    }

    private static void AWork(object? obj)
    {
       while (true)
        {
            goA.Wait();
            System.Console.WriteLine("A");
            goB.Release(2);
        }
    }
}