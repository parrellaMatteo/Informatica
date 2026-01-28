using System;
using _07_ExtensionMethodsDemo; // Se necessario, ma se la classe è nello stesso namespace puoi ometterlo

namespace _07_ExtensionMethodsDemo;
internal class Program
{
    private static void Main(string[] args)
    {
        string s = "The quick brown fox jumped over the lazy dog.";
        int i = s.WordCount();
        Console.WriteLine("Word count of s is {0}", i);
        int[] array = [1, 2, 3, 4, 5];
        int totale = array.Sum();
    }
}


public static class StringExtension
{
    /// <summary>
    /// Conta il numero di parole nalla stringa str
    /// </summary>
    /// <param name="str"></param> La stringa di cui si vuole conoscere il numero di parole
    /// <returns>Il numero di parole contenute nella stringa str</returns>
    public static int WordCount(this string str)
    {
        return str.Split([' ', '.', '?',';',',',':','!'], StringSplitOptions.RemoveEmptyEntries).Length;
    }
}