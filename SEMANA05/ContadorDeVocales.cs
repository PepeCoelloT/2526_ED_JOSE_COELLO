using System;

class ContadorDeVocales
{
    static void Main()
    {
        int a = 0, e = 0, i = 0, o = 0, u = 0;

        Console.Write("Ingrese una palabra: ");
        string palabra = Console.ReadLine().ToLower();

        foreach (char letra in palabra)
        {
            switch (letra)
            {
                case 'a': a++; break;
                case 'e': e++; break;
                case 'i': i++; break;
                case 'o': o++; break;
                case 'u': u++; break;
            }
        }

        Console.WriteLine("\nCantidad de vocales:");
        Console.WriteLine("A: " + a);
        Console.WriteLine("E: " + e);
        Console.WriteLine("I: " + i);
        Console.WriteLine("O: " + o);
        Console.WriteLine("U: " + u);
    }
}
