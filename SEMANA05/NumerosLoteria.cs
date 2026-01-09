using System;
using System.Collections.Generic;

class NumerosLoteria
{
    static void Main()
    {
        List<int> listaNumeros = new List<int>();
        int contador = 1;

        Console.WriteLine("Ingrese los números ganadores de la lotería:");

        while (contador <= 6)
        {
            Console.Write("Ingrese el número " + contador + ": ");
            int valor = Convert.ToInt32(Console.ReadLine());
            listaNumeros.Add(valor);
            contador++;
        }

        listaNumeros.Sort();

        Console.WriteLine("\nNúmeros ordenados de menor a mayor:");
        foreach (var numero in listaNumeros)
        {
            Console.Write(numero + " ");
        }
    }
}
