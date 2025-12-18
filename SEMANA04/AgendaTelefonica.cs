using System;

namespace AgendaTelefonica
{
    // Record struct (registro) -> distinto a un struct tradicional
    public readonly record struct Contacto(int Id, string Nombre, string Correo, string Direccion);

    public class Agenda
    {
        private readonly Contacto[] _contactos;     // Vector de contactos
        private readonly string[,] _telefonos;      // Matriz [contactoIndex, telefonoIndex] (hasta 3 teléfonos)
        private readonly bool[] _activo;            // Vector para "eliminación lógica"
        private int _count;
        private int _nextId;

        public Agenda(int capacidad)
        {
            if (capacidad < 5) capacidad = 5;

            _contactos = new Contacto[capacidad];
            _telefonos = new string[capacidad, 3];
            _activo = new bool[capacidad];

            _count = 0;
            _nextId = 1;

            Inicializar();
        }

        private void Inicializar()
        {
            for (int i = 0; i < _contactos.Length; i++)
            {
                _activo[i] = false;
                for (int t = 0; t < 3; t++)
                    _telefonos[i, t] = "";
            }
        }

        public bool AgregarContacto(string nombre, string correo, string direccion, string tel1, string tel2, string tel3, out string mensaje)
        {
            mensaje = "";

            int slot = BuscarSlotLibre();
            if (slot == -1)
            {
                mensaje = "No se puede agregar: agenda llena.";
                return false;
            }

            nombre = (nombre ?? "").Trim();
            correo = (correo ?? "").Trim();
            direccion = (direccion ?? "").Trim();

            if (nombre.Length < 2)
            {
                mensaje = "Nombre inválido (mínimo 2 caracteres).";
                return false;
            }

            // Validar teléfonos (permitimos vacíos salvo tel1)
            if (!TelefonoValido(tel1, permitirVacio: false) ||
                !TelefonoValido(tel2, permitirVacio: true) ||
                !TelefonoValido(tel3, permitirVacio: true))
            {
                mensaje = "Teléfonos inválidos (solo dígitos, longitud 7 a 10; tel1 es obligatorio).";
                return false;
            }

            // Evitar duplicado de teléfono principal
            if (ExisteTelefono(tel1))
            {
                mensaje = "Ya existe un contacto con ese teléfono principal.";
                return false;
            }

            int id = _nextId++;
            _contactos[slot] = new Contacto(id, nombre, correo, direccion);
            _telefonos[slot, 0] = tel1.Trim();
            _telefonos[slot, 1] = (tel2 ?? "").Trim();
            _telefonos[slot, 2] = (tel3 ?? "").Trim();
            _activo[slot] = true;

            _count++;
            mensaje = $"Contacto agregado con ID {id}.";
            return true;
        }

        private int BuscarSlotLibre()
        {
            for (int i = 0; i < _contactos.Length; i++)
                if (!_activo[i]) return i;
            return -1;
        }

        private bool TelefonoValido(string tel, bool permitirVacio)
        {
            tel = (tel ?? "").Trim();
            if (permitirVacio && tel == "") return true;

            if (tel.Length < 7 || tel.Length > 10) return false;
            for (int i = 0; i < tel.Length; i++)
                if (!char.IsDigit(tel[i])) return false;

            return true;
        }

        private bool ExisteTelefono(string tel)
        {
            tel = (tel ?? "").Trim();
            for (int i = 0; i < _contactos.Length; i++)
            {
                if (!_activo[i]) continue;
                if (_telefonos[i, 0] == tel) return true; // teléfono principal
            }
            return false;
        }

        public void Listar()
        {
            Console.WriteLine("\n--- LISTA DE CONTACTOS ---");
            if (_count == 0)
            {
                Console.WriteLine("Agenda vacía.");
                return;
            }

            for (int i = 0; i < _contactos.Length; i++)
            {
                if (!_activo[i]) continue;
                Console.WriteLine($"ID: {_contactos[i].Id} | Nombre: {_contactos[i].Nombre} | Tel: {_telefonos[i, 0]}");
            }
        }

        public void VerDetalle(int id)
        {
            int idx = BuscarIndicePorId(id);
            if (idx == -1)
            {
                Console.WriteLine("No se encontró el contacto.");
                return;
            }

            var c = _contactos[idx];
            Console.WriteLine("\n--- DETALLE DEL CONTACTO ---");
            Console.WriteLine($"ID: {c.Id}");
            Console.WriteLine($"Nombre: {c.Nombre}");
            Console.WriteLine($"Correo: {(string.IsNullOrWhiteSpace(c.Correo) ? "N/D" : c.Correo)}");
            Console.WriteLine($"Dirección: {(string.IsNullOrWhiteSpace(c.Direccion) ? "N/D" : c.Direccion)}");
            Console.WriteLine("Teléfonos:");
            for (int t = 0; t < 3; t++)
            {
                if (!string.IsNullOrWhiteSpace(_telefonos[idx, t]))
                    Console.WriteLine($"  - {_telefonos[idx, t]}");
            }
        }

        public void BuscarPorNombre(string texto)
        {
            Console.WriteLine("\n--- BÚSQUEDA POR NOMBRE ---");
            texto = (texto ?? "").Trim().ToLowerInvariant();
            if (texto.Length == 0)
            {
                Console.WriteLine("Ingrese un texto válido.");
                return;
            }

            int encontrados = 0;
            for (int i = 0; i < _contactos.Length; i++)
            {
                if (!_activo[i]) continue;
                if (_contactos[i].Nombre.ToLowerInvariant().Contains(texto))
                {
                    Console.WriteLine($"ID: {_contactos[i].Id} | Nombre: {_contactos[i].Nombre} | Tel: {_telefonos[i, 0]}");
                    encontrados++;
                }
            }

            if (encontrados == 0) Console.WriteLine("No hubo coincidencias.");
        }

        public void BuscarPorTelefono(string tel)
        {
            Console.WriteLine("\n--- BÚSQUEDA POR TELÉFONO ---");
            tel = (tel ?? "").Trim();

            for (int i = 0; i < _contactos.Length; i++)
            {
                if (!_activo[i]) continue;

                for (int t = 0; t < 3; t++)
                {
                    if (_telefonos[i, t] == tel)
                    {
                        Console.WriteLine($"Encontrado: ID {_contactos[i].Id} | {_contactos[i].Nombre}");
                        return;
                    }
                }
            }

            Console.WriteLine("No se encontró el teléfono.");
        }

        public bool Eliminar(int id, out string mensaje)
        {
            mensaje = "";
            int idx = BuscarIndicePorId(id);
            if (idx == -1)
            {
                mensaje = "No se encontró el contacto.";
                return false;
            }

            _activo[idx] = false;
            for (int t = 0; t < 3; t++) _telefonos[idx, t] = "";
            _count--;

            mensaje = "Contacto eliminado correctamente.";
            return true;
        }

        private int BuscarIndicePorId(int id)
        {
            for (int i = 0; i < _contactos.Length; i++)
                if (_activo[i] && _contactos[i].Id == id) return i;
            return -1;
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("AGENDA TELEFÓNICA (Consola - C#)");
            int capacidad = LeerEntero("Capacidad de contactos (ej. 30): ", 5, 500);
            var agenda = new Agenda(capacidad);

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n--- MENÚ ---");
                Console.WriteLine("1) Agregar contacto");
                Console.WriteLine("2) Listar contactos");
                Console.WriteLine("3) Ver detalle por ID");
                Console.WriteLine("4) Buscar por nombre");
                Console.WriteLine("5) Buscar por teléfono");
                Console.WriteLine("6) Eliminar contacto");
                Console.WriteLine("0) Salir");

                int op = LeerEntero("Opción: ", 0, 6);

                switch (op)
                {
                    case 1:
                        AgregarUI(agenda);
                        break;
                    case 2:
                        agenda.Listar();
                        break;
                    case 3:
                        agenda.VerDetalle(LeerEntero("ID: ", 1, 99999999));
                        break;
                    case 4:
                        Console.Write("Texto (nombre): ");
                        agenda.BuscarPorNombre(Console.ReadLine());
                        break;
                    case 5:
                        Console.Write("Teléfono: ");
                        agenda.BuscarPorTelefono(Console.ReadLine());
                        break;
                    case 6:
                        {
                            int id = LeerEntero("ID a eliminar: ", 1, 99999999);
                            bool ok = agenda.Eliminar(id, out string msg);
                            Console.WriteLine(msg);
                        }
                        break;
                    case 0:
                        salir = true;
                        break;
                }
            }

            Console.WriteLine("\nFin.");
        }

        private static void AgregarUI(Agenda agenda)
        {
            Console.WriteLine("\n--- NUEVO CONTACTO ---");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Correo (opcional): ");
            string correo = Console.ReadLine();

            Console.Write("Dirección (opcional): ");
            string direccion = Console.ReadLine();

            Console.Write("Teléfono 1 (obligatorio): ");
            string tel1 = Console.ReadLine();

            Console.Write("Teléfono 2 (opcional): ");
            string tel2 = Console.ReadLine();

            Console.Write("Teléfono 3 (opcional): ");
            string tel3 = Console.ReadLine();

            bool ok = agenda.AgregarContacto(nombre, correo, direccion, tel1, tel2, tel3, out string msg);
            Console.WriteLine(msg);
        }

        private static int LeerEntero(string mensaje, int min, int max)
        {
            while (true)
            {
                Console.Write(mensaje);
                string s = (Console.ReadLine() ?? "").Trim();
                if (int.TryParse(s, out int v) && v >= min && v <= max) return v;
                Console.WriteLine($"Entrada inválida. Debe estar entre {min} y {max}.");
            }
        }
    }
}
