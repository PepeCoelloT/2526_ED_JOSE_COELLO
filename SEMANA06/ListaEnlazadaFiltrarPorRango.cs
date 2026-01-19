using System;
using System.Collections.Generic;

class ListaEnlazadaFiltrarPorRango
{
    static void Main(string[] args)
    {
        // Crear la lista enlazada
        LinkedList<int> lista = new LinkedList<int>();
        Random random = new Random();

        // Generar 50 números aleatorios entre 1 y 999
        for (int i = 0; i < 50; i++)
        {
            lista.AddLast(random.Next(1, 1000));
        }

        Console.WriteLine("Lista enlazada original:");
        MostrarLista(lista);

        // Leer rango desde teclado
        Console.Write("\nIngrese el valor mínimo del rango: ");
        int minimo = int.Parse(Console.ReadLine());

        Console.Write("Ingrese el valor máximo del rango: ");
        int maximo = int.Parse(Console.ReadLine());

        // Eliminar nodos fuera del rango
        LinkedListNode<int> nodoActual = lista.First;

        while (nodoActual != null)
        {
            LinkedListNode<int> siguiente = nodoActual.Next;

            if (nodoActual.Value < minimo || nodoActual.Value > maximo)
            {
                lista.Remove(nodoActual);
            }

            nodoActual = siguiente;
        }

        Console.WriteLine("\nLista enlazada después de eliminar valores fuera del rango:");
        MostrarLista(lista);

        Console.ReadKey();
    }

    // Método para mostrar la lista
    static void MostrarLista(LinkedList<int> lista)
    {
        foreach (int valor in lista)
        {
            Console.Write(valor + " ");
        }
        Console.WriteLine();
    }
}
