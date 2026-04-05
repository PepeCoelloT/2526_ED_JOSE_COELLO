using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        SistemaVuelos sistema = new SistemaVuelos();
        sistema.Menu();
    }
}

class SistemaVuelos
{
    private Dictionary<string, List<Vuelo>> vuelos;

    public SistemaVuelos()
    {
        // Base de datos ficticia simulada dentro del programa
        vuelos = new Dictionary<string, List<Vuelo>>()
        {
            { "QUITO", new List<Vuelo> {
                new Vuelo("GUAYAQUIL", 80),
                new Vuelo("CUENCA", 90),
                new Vuelo("MANTA", 70)
            }},
            { "GUAYAQUIL", new List<Vuelo> {
                new Vuelo("QUITO", 80),
                new Vuelo("CUENCA", 50),
                new Vuelo("LOJA", 120)
            }},
            { "CUENCA", new List<Vuelo> {
                new Vuelo("QUITO", 90),
                new Vuelo("GUAYAQUIL", 50),
                new Vuelo("LOJA", 60),
                new Vuelo("MANTA", 100)
            }},
            { "MANTA", new List<Vuelo> {
                new Vuelo("QUITO", 70),
                new Vuelo("CUENCA", 100),
                new Vuelo("LOJA", 140)
            }},
            { "LOJA", new List<Vuelo> {
                new Vuelo("GUAYAQUIL", 120),
                new Vuelo("CUENCA", 60),
                new Vuelo("MANTA", 140)
            }}
        };
    }

    public void Menu()
    {
        int opcion;
        do
        {
            Console.WriteLine("\n===== SISTEMA DE VUELOS BARATOS =====");
            Console.WriteLine("1. Mostrar ciudades");
            Console.WriteLine("2. Mostrar vuelos disponibles");
            Console.WriteLine("3. Buscar vuelo más barato");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Entrada inválida.");
                opcion = 0;
            }

            switch (opcion)
            {
                case 1:
                    MostrarCiudades();
                    break;
                case 2:
                    MostrarVuelos();
                    break;
                case 3:
                    BuscarVuelo();
                    break;
                case 4:
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

        } while (opcion != 4);
    }

    private void MostrarCiudades()
    {
        Console.WriteLine("\nCIUDADES DISPONIBLES:");
        foreach (var ciudad in vuelos.Keys)
        {
            Console.WriteLine("- " + ciudad);
        }
    }

    private void MostrarVuelos()
    {
        Console.WriteLine("\nBASE DE DATOS DE VUELOS:");
        foreach (var origen in vuelos.Keys)
        {
            Console.WriteLine($"\nDesde {origen}:");
            foreach (var vuelo in vuelos[origen])
            {
                Console.WriteLine($"  -> {vuelo.Destino}: ${vuelo.Costo}");
            }
        }
    }

    private void BuscarVuelo()
    {
        Console.Write("\nIngrese la ciudad de origen: ");
        string origen = Console.ReadLine().Trim().ToUpper();

        Console.Write("Ingrese la ciudad de destino: ");
        string destino = Console.ReadLine().Trim().ToUpper();

        if (!vuelos.ContainsKey(origen))
        {
            Console.WriteLine("La ciudad de origen no existe.");
            return;
        }

        if (!vuelos.ContainsKey(destino))
        {
            Console.WriteLine("La ciudad de destino no existe.");
            return;
        }

        Stopwatch cronometro = new Stopwatch();
        cronometro.Start();

        var resultado = EncontrarRutaMasBarata(origen, destino);

        cronometro.Stop();

        if (resultado == null)
        {
            Console.WriteLine("No se encontró una ruta disponible.");
        }
        else
        {
            Console.WriteLine("\nRESULTADO DE LA CONSULTA:");
            Console.WriteLine("Ruta más barata: " + string.Join(" -> ", resultado.Ruta));
            Console.WriteLine("Costo total: $" + resultado.CostoTotal);
        }

        Console.WriteLine($"Tiempo de ejecución: {cronometro.Elapsed.TotalMilliseconds} ms");
    }

    private ResultadoRuta EncontrarRutaMasBarata(string origen, string destino)
    {
        Dictionary<string, int> distancias = new Dictionary<string, int>();
        Dictionary<string, string> anteriores = new Dictionary<string, string>();
        List<string> noVisitados = new List<string>();

        foreach (var ciudad in vuelos.Keys)
        {
            distancias[ciudad] = int.MaxValue;
            anteriores[ciudad] = null;
            noVisitados.Add(ciudad);
        }

        distancias[origen] = 0;

        while (noVisitados.Count > 0)
        {
            string ciudadActual = noVisitados
                .OrderBy(c => distancias[c])
                .First();

            noVisitados.Remove(ciudadActual);

            if (distancias[ciudadActual] == int.MaxValue)
                break;

            if (ciudadActual == destino)
                break;

            foreach (var vecino in vuelos[ciudadActual])
            {
                int nuevaDistancia = distancias[ciudadActual] + vecino.Costo;

                if (nuevaDistancia < distancias[vecino.Destino])
                {
                    distancias[vecino.Destino] = nuevaDistancia;
                    anteriores[vecino.Destino] = ciudadActual;
                }
            }
        }

        if (distancias[destino] == int.MaxValue)
            return null;

        List<string> ruta = new List<string>();
        string ciudadRuta = destino;

        while (ciudadRuta != null)
        {
            ruta.Insert(0, ciudadRuta);
            ciudadRuta = anteriores[ciudadRuta];
        }

        return new ResultadoRuta
        {
            Ruta = ruta,
            CostoTotal = distancias[destino]
        };
    }
}

class Vuelo
{
    public string Destino { get; set; }
    public int Costo { get; set; }

    public Vuelo(string destino, int costo)
    {
        Destino = destino;
        Costo = costo;
    }
}

class ResultadoRuta
{
    public List<string> Ruta { get; set; }
    public int CostoTotal { get; set; }
}
