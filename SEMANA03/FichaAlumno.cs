using System;
 
class FichaAlumno
{
    public string Codigo;
    public string NombreCompleto;
    public string DireccionDomicilio;
    public string[] Contactos;
 
    public FichaAlumno()
    {
        Contactos = new string[3];
    }
 
    public void CargarDatosPersonales()
    {
        Console.Write("Código del estudiante: ");
        Codigo = Console.ReadLine();
 
        Console.Write("Nombre completo: ");
        NombreCompleto = Console.ReadLine();
 
        Console.Write("Dirección: ");
        DireccionDomicilio = Console.ReadLine();
    }
 
    public void CargarTelefonos()
    {
        for (int i = 0; i < Contactos.Length; i++)
        {
            Console.Write("Número telefónico " + (i + 1) + ": ");
            Contactos[i] = Console.ReadLine();
        }
    }
 
    public void MostrarFicha()
    {
        Console.WriteLine("\nFICHA DEL ESTUDIANTE");
        Console.WriteLine("Código: " + Codigo);
        Console.WriteLine("Nombre: " + NombreCompleto);
        Console.WriteLine("Dirección: " + DireccionDomicilio);
        Console.WriteLine("Teléfonos registrados:");
 
        int indice = 1;
        foreach (string numero in Contactos)
        {
            Console.WriteLine("  Teléfono " + indice + ": " + numero);
            indice++;
        }
    }
}
 
class Program
{
    static void Main()
    {
        FichaAlumno alumno = new FichaAlumno();
 
        Console.WriteLine("SISTEMA DE REGISTRO ACADÉMICO\n");
 
        alumno.CargarDatosPersonales();
        alumno.CargarTelefonos();
        alumno.MostrarFicha();
 
        Console.WriteLine("\nFin del programa. Presione ENTER para salir.");
        Console.ReadLine();
    }
}