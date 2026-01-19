using System;
using System.Collections.Generic;

class TorresDeHanoiConPilas
{
    static Stack<int> origen = new Stack<int>();
    static Stack<int> auxiliar = new Stack<int>();
    static Stack<int> destino = new Stack<int>();

    static void Main()
    {
        Console.Write("Ingrese el número de discos: ");
        int n = int.Parse(Console.ReadLine());

        // Inicializar la torre origen
        for (int i = n; i >= 1; i--)
        {
            origen.Push(i);
        }

        Console.WriteLine("\nMovimientos:");
        ResolverHanoi(n, origen, destino, auxiliar, "Origen", "Destino", "Auxiliar");

        Console.ReadKey();
    }

    // Algoritmo recursivo de Hanoi usando pilas
    static void ResolverHanoi(int n, Stack<int> origen, Stack<int> destino, Stack<int> auxiliar,
                              string nombreOrigen, string nombreDestino, string nombreAuxiliar)
    {
        if (n == 1)
        {
            int disco = origen.Pop();
            destino.Push(disco);
            Console.WriteLine($"Mover disco {disco} de {nombreOrigen} a {nombreDestino}");
        }
        else
        {
            ResolverHanoi(n - 1, origen, auxiliar, destino, nombreOrigen, nombreAuxiliar, nombreDestino);

            int disco = origen.Pop();
            destino.Push(disco);
            Console.WriteLine($"Mover disco {disco} de {nombreOrigen} a {nombreDestino}");

            ResolverHanoi(n - 1, auxiliar, destino, origen, nombreAuxiliar, nombreDestino, nombreOrigen);
        }
    }
}
