using System;
using System.Collections.Generic;

class ContarElementosDeUnaLista
{
    static void Main(string[] args)
    {
        List<int> lista = new List<int>();
        int cantidad;

        Console.Write("Ingrese la cantidad de elementos que tendrá la lista: ");
        cantidad = int.Parse(Console.ReadLine());

        // Ingreso de elementos a la lista
        for (int i = 0; i < cantidad; i++)
        {
            Console.Write($"Ingrese el elemento {i + 1}: ");
            int valor = int.Parse(Console.ReadLine());
            lista.Add(valor);
        }

        // Llamada a la función
        int totalElementos = ContarElementos(lista);

        Console.WriteLine("\nLa cantidad de elementos en la lista es: " + totalElementos);
        Console.ReadKey();
    }

    // Función que recorre la lista y cuenta sus elementos
    static int ContarElementos(List<int> lista)
    {
        int contador = 0;

        foreach (int elemento in lista)
        {
            contador++;
        }

        return contador;
    }
}
