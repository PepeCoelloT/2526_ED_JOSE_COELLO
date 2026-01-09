using System;
using System.Collections.Generic;

class ProductoEscalarVectores
{
    static void Main()
    {
        // Definición de los vectores usando listas
        List<int> vector1 = new List<int> { 1, 2, 3 };
        List<int> vector2 = new List<int> { -1, 0, 2 };

        int productoEscalar = 0;

        // Cálculo del producto escalar
        for (int i = 0; i < vector1.Count; i++)
        {
            productoEscalar += vector1[i] * vector2[i];
        }

        // Mostrar resultado
        Console.WriteLine("Vector 1: (1, 2, 3)");
        Console.WriteLine("Vector 2: (-1, 0, 2)");
        Console.WriteLine("Producto escalar: " + productoEscalar);
    }
}
