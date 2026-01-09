using System;
using System.Collections.Generic;

class ListaDeAsignaturasSalud
{
    static void Main()
    {
        List<string> asignaturas = new List<string>();

        asignaturas.Add("Histologia");
        asignaturas.Add("Anatomia");
        asignaturas.Add("Embriologia");
        asignaturas.Add("Parasitologia");

        Console.WriteLine("Asignaturas del curso:");
        foreach (string materia in asignaturas)
        {
            Console.WriteLine(materia);
        }
    }
}
