using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PracticaColaAsientos
{
    // Representa a una persona que llega a la fila
    public class Asistente
    {
        public int Id { get; }
        public string Nombre { get; }
        public DateTime HoraLlegada { get; }

        public Asistente(int id, string nombre)
        {
            Id = id;
            Nombre = (nombre ?? "").Trim();
            HoraLlegada = DateTime.Now;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Nombre: {Nombre} | Llegada: {HoraLlegada:HH:mm:ss}";
        }
    }

    // Maneja la cola y la asignación de asientos
    public class Atraccion
    {
        private readonly Queue<Asistente> _colaEspera = new Queue<Asistente>();
        private readonly List<Asistente> _asignados = new List<Asistente>();

        public int Capacidad { get; }

        public Atraccion(int capacidad)
        {
            if (capacidad <= 0) throw new ArgumentException("La capacidad debe ser mayor que cero.");
            Capacidad = capacidad;
        }

        // Totales
        public int TotalEnCola => _colaEspera.Count;
        public int TotalAsignados => _asignados.Count;

        // Para coherencia con el enunciado: no se "venden/registran" más de 30 cupos en total.
        public int TotalRegistrados => _asignados.Count + _colaEspera.Count;
        public bool CupoLleno => TotalRegistrados >= Capacidad;

        // Solo indica si ya se asignaron todos los asientos
        public bool AsientosCompletos => _asignados.Count >= Capacidad;

        // Registrar persona en la cola (FIFO) - No permite superar 30 cupos totales
        public bool Encolar(Asistente asistente)
        {
            if (asistente == null) throw new ArgumentNullException(nameof(asistente));
            if (CupoLleno) return false;
            _colaEspera.Enqueue(asistente);
            return true;
        }

        // Reportería: ver toda la cola
        public List<Asistente> VerCola()
        {
            return _colaEspera.ToList();
        }

        // Reportería: consultar si alguien está en cola por ID
        public Asistente BuscarEnColaPorId(int id)
        {
            return _colaEspera.FirstOrDefault(a => a.Id == id);
        }

        // Asignar asientos hasta completar la capacidad (o hasta que no haya personas)
        public void AsignarAsientos()
        {
            while (!AsientosCompletos && _colaEspera.Count > 0)
            {
                _asignados.Add(_colaEspera.Dequeue());
            }
        }

        // Reportería: ver orden final de abordaje/asignación
        public List<Asistente> VerAsignados()
        {
            return _asignados.ToList();
        }

        public void Reiniciar()
        {
            _colaEspera.Clear();
            _asignados.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Atraccion atraccion = new Atraccion(30);
            int contadorId = 1;

            Stopwatch sw = new Stopwatch();

            while (true)
            {
                Console.WriteLine("\n=== SISTEMA DE ASIGNACIÓN DE 30 ASIENTOS (COLA FIFO) ===");
                Console.WriteLine("1) Registrar persona en la fila");
                Console.WriteLine("2) Ver cola (reportería)");
                Console.WriteLine("3) Consultar persona en cola por ID (reportería)");
                Console.WriteLine("4) Asignar asientos (procesar fila)");
                Console.WriteLine("5) Ver asignados / orden de abordaje (reportería)");
                Console.WriteLine("6) Ver estado del sistema");
                Console.WriteLine("7) Reiniciar sistema");
                Console.WriteLine("0) Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = (Console.ReadLine() ?? "").Trim();

                if (opcion == "0") break;

                switch (opcion)
                {
                    case "1":
                        if (atraccion.CupoLleno)
                        {
                            Console.WriteLine(" Cupos agotados: ya se registraron 30 personas. No se puede registrar más.");
                            break;
                        }

                        Console.Write("Ingrese nombre del asistente: ");
                        string nombre = Console.ReadLine() ?? "";

                        if (string.IsNullOrWhiteSpace(nombre))
                        {
                            Console.WriteLine(" Nombre inválido.");
                            break;
                        }

                        sw.Restart();
                        bool ok = atraccion.Encolar(new Asistente(contadorId, nombre));
                        sw.Stop();

                        if (ok)
                        {
                            Console.WriteLine($" Registrado con ID {contadorId}. Tiempo: {sw.ElapsedTicks} ticks ({sw.ElapsedMilliseconds} ms).");
                            Console.WriteLine($"Cupos registrados: {atraccion.TotalRegistrados}/30");
                            contadorId++;
                        }
                        else
                        {
                            Console.WriteLine(" No se pudo registrar: cupos agotados.");
                        }
                        break;

                    case "2":
                        var cola = atraccion.VerCola();
                        Console.WriteLine("\n--- COLA ACTUAL ---");
                        if (cola.Count == 0) Console.WriteLine("No hay personas en la fila.");
                        else cola.ForEach(a => Console.WriteLine(a));
                        break;

                    case "3":
                        Console.Write("Ingrese ID a buscar: ");
                        if (!int.TryParse(Console.ReadLine(), out int idBuscar))
                        {
                            Console.WriteLine(" ID inválido.");
                            break;
                        }

                        sw.Restart();
                        var encontrado = atraccion.BuscarEnColaPorId(idBuscar);
                        sw.Stop();

                        if (encontrado == null)
                            Console.WriteLine("No se encontró ese ID en la cola.");
                        else
                            Console.WriteLine($" Encontrado: {encontrado}");

                        Console.WriteLine($"Tiempo: {sw.ElapsedTicks} ticks ({sw.ElapsedMilliseconds} ms).");
                        break;

                    case "4":
                        sw.Restart();
                        atraccion.AsignarAsientos();
                        sw.Stop();

                        Console.WriteLine($" Procesamiento realizado. Asignados: {atraccion.TotalAsignados}/30");
                        Console.WriteLine($"Tiempo: {sw.ElapsedTicks} ticks ({sw.ElapsedMilliseconds} ms).");
                        break;

                    case "5":
                        var asignados = atraccion.VerAsignados();
                        Console.WriteLine("\n--- ORDEN FINAL DE ABORDAJE (ASIGNADOS) ---");
                        if (asignados.Count == 0)
                        {
                            Console.WriteLine("Aún no hay asignados. Use la opción 4 para asignar asientos.");
                        }
                        else
                        {
                            int asiento = 1;
                            foreach (var a in asignados)
                            {
                                Console.WriteLine($"Asiento {asiento:00}: {a}");
                                asiento++;
                            }
                        }
                        break;

                    case "6":
                        Console.WriteLine("\n--- ESTADO DEL SISTEMA ---");
                        Console.WriteLine($"Capacidad total: {atraccion.Capacidad}");
                        Console.WriteLine($"Registrados (cola + asignados): {atraccion.TotalRegistrados}");
                        Console.WriteLine($"En cola: {atraccion.TotalEnCola}");
                        Console.WriteLine($"Asignados: {atraccion.TotalAsignados}");
                        Console.WriteLine($"Cupos agotados: {(atraccion.CupoLleno ? "SI" : "NO")}");
                        Console.WriteLine($"Asientos ya asignados completos: {(atraccion.AsientosCompletos ? "SI" : "NO")}");
                        break;

                    case "7":
                        atraccion.Reiniciar();
                        contadorId = 1;
                        Console.WriteLine(" Sistema reiniciado.");
                        break;

                    default:
                        Console.WriteLine(" Opción no válida.");
                        break;
                }
            }

            Console.WriteLine("Programa finalizado.");
        }
    }
}
