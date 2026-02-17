using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Universo
        HashSet<string> personas = new HashSet<string>();
        for (int i = 1; i <= 500; i++)
        {
            personas.Add("Persona " + i);
        }

        // Pfizer (75)
        HashSet<string> pfizer = new HashSet<string>();
        for (int i = 1; i <= 75; i++)
        {
            pfizer.Add("Persona " + i);
        }

        // AstraZeneca (75)
        HashSet<string> astraZeneca = new HashSet<string>();
        for (int i = 50; i < 125; i++)
        {
            astraZeneca.Add("Persona " + i);
        }

        // Operaciones
        HashSet<string> union = new HashSet<string>(pfizer);
        union.UnionWith(astraZeneca);

        HashSet<string> noVacunados = new HashSet<string>(personas);
        noVacunados.ExceptWith(union);

        HashSet<string> ambasDosis = new HashSet<string>(pfizer);
        ambasDosis.IntersectWith(astraZeneca);

        HashSet<string> soloPfizer = new HashSet<string>(pfizer);
        soloPfizer.ExceptWith(astraZeneca);

        HashSet<string> soloAstraZeneca = new HashSet<string>(astraZeneca);
        soloAstraZeneca.ExceptWith(pfizer);

        // Menú
        int opcion;

        do
        {
            Console.WriteLine("\n===== SISTEMA DE VACUNACIÓN =====");
            Console.WriteLine("1. Ver ciudadanos no vacunados");
            Console.WriteLine("2. Ver ciudadanos con ambas dosis");
            Console.WriteLine("3. Ver ciudadanos solo Pfizer");
            Console.WriteLine("4. Ver ciudadanos solo AstraZeneca");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    MostrarLista(noVacunados, "No vacunados");
                    break;
                case 2:
                    MostrarLista(ambasDosis, "Ambas dosis");
                    break;
                case 3:
                    MostrarLista(soloPfizer, "Solo Pfizer");
                    break;
                case 4:
                    MostrarLista(soloAstraZeneca, "Solo AstraZeneca");
                    break;
                case 5:
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 5);
    }

    static void MostrarLista(HashSet<string> conjunto, string titulo)
    {
        Console.WriteLine($"\n--- {titulo} ({conjunto.Count}) ---");
        foreach (var persona in conjunto)
        {
            Console.WriteLine(persona);
        }
    }
}
