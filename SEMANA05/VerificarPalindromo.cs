using System;

class VerificarPalindromo
{
    static void Main()
    {
        Console.Write("Ingrese una palabra: ");
        string palabra = Console.ReadLine().ToLower();

        string invertida = "";

        for (int i = palabra.Length - 1; i >= 0; i--)
        {
            invertida += palabra[i];
        }

        if (palabra == invertida)
            Console.WriteLine("La palabra es un palíndromo.");
        else
            Console.WriteLine("La palabra NO es un palíndromo.");
    }
}
