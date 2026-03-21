using System;

// 👇 Clase Nodo
class Nodo
{
    public int Valor;
    public Nodo Izquierdo;
    public Nodo Derecho;

    public Nodo(int valor)
    {
        Valor = valor;
        Izquierdo = null;
        Derecho = null;
    }
}

// 👇 Clase Árbol BST
class ArbolBST
{
    public Nodo Raiz;

    public Nodo Insertar(Nodo raiz, int valor)
    {
        if (raiz == null)
            return new Nodo(valor);

        if (valor < raiz.Valor)
            raiz.Izquierdo = Insertar(raiz.Izquierdo, valor);
        else if (valor > raiz.Valor)
            raiz.Derecho = Insertar(raiz.Derecho, valor);

        return raiz;
    }

    public bool Buscar(Nodo raiz, int valor)
    {
        if (raiz == null) return false;

        if (valor == raiz.Valor) return true;
        else if (valor < raiz.Valor)
            return Buscar(raiz.Izquierdo, valor);
        else
            return Buscar(raiz.Derecho, valor);
    }

    public Nodo Minimo(Nodo raiz)
    {
        while (raiz.Izquierdo != null)
            raiz = raiz.Izquierdo;
        return raiz;
    }

    public Nodo Eliminar(Nodo raiz, int valor)
    {
        if (raiz == null) return raiz;

        if (valor < raiz.Valor)
            raiz.Izquierdo = Eliminar(raiz.Izquierdo, valor);
        else if (valor > raiz.Valor)
            raiz.Derecho = Eliminar(raiz.Derecho, valor);
        else
        {
            if (raiz.Izquierdo == null && raiz.Derecho == null)
                return null;

            if (raiz.Izquierdo == null)
                return raiz.Derecho;
            else if (raiz.Derecho == null)
                return raiz.Izquierdo;

            Nodo temp = Minimo(raiz.Derecho);
            raiz.Valor = temp.Valor;
            raiz.Derecho = Eliminar(raiz.Derecho, temp.Valor);
        }

        return raiz;
    }

    public void Inorden(Nodo raiz)
    {
        if (raiz != null)
        {
            Inorden(raiz.Izquierdo);
            Console.Write(raiz.Valor + " ");
            Inorden(raiz.Derecho);
        }
    }

    public void Preorden(Nodo raiz)
    {
        if (raiz != null)
        {
            Console.Write(raiz.Valor + " ");
            Preorden(raiz.Izquierdo);
            Preorden(raiz.Derecho);
        }
    }

    public void Postorden(Nodo raiz)
    {
        if (raiz != null)
        {
            Postorden(raiz.Izquierdo);
            Postorden(raiz.Derecho);
            Console.Write(raiz.Valor + " ");
        }
    }

    public int Altura(Nodo raiz)
    {
        if (raiz == null) return 0;

        int izq = Altura(raiz.Izquierdo);
        int der = Altura(raiz.Derecho);

        return Math.Max(izq, der) + 1;
    }

    public int Maximo(Nodo raiz)
    {
        while (raiz.Derecho != null)
            raiz = raiz.Derecho;
        return raiz.Valor;
    }

    public int MinimoValor(Nodo raiz)
    {
        while (raiz.Izquierdo != null)
            raiz = raiz.Izquierdo;
        return raiz.Valor;
    }

    public void Limpiar()
    {
        Raiz = null;
    }
}

// 👇 Programa principal (MENÚ)
class Program
{
    static void Main(string[] args)
    {
        ArbolBST arbol = new ArbolBST();
        int opcion, valor;

        do
        {
            Console.WriteLine("\n--- MENU BST ---");
            Console.WriteLine("1. Insertar");
            Console.WriteLine("2. Buscar");
            Console.WriteLine("3. Eliminar");
            Console.WriteLine("4. Inorden");
            Console.WriteLine("5. Preorden");
            Console.WriteLine("6. Postorden");
            Console.WriteLine("7. Minimo");
            Console.WriteLine("8. Maximo");
            Console.WriteLine("9. Altura");
            Console.WriteLine("10. Limpiar");
            Console.WriteLine("0. Salir");
            Console.Write("Opcion: ");

            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("Valor: ");
                    valor = int.Parse(Console.ReadLine());
                    arbol.Raiz = arbol.Insertar(arbol.Raiz, valor);
                    break;

                case 2:
                    Console.Write("Buscar: ");
                    valor = int.Parse(Console.ReadLine());
                    Console.WriteLine(arbol.Buscar(arbol.Raiz, valor) ? "Encontrado" : "No encontrado");
                    break;

                case 3:
                    Console.Write("Eliminar: ");
                    valor = int.Parse(Console.ReadLine());
                    arbol.Raiz = arbol.Eliminar(arbol.Raiz, valor);
                    break;

                case 4:
                    arbol.Inorden(arbol.Raiz);
                    Console.WriteLine();
                    break;

                case 5:
                    arbol.Preorden(arbol.Raiz);
                    Console.WriteLine();
                    break;

                case 6:
                    arbol.Postorden(arbol.Raiz);
                    Console.WriteLine();
                    break;

                case 7:
                    Console.WriteLine("Minimo: " + arbol.MinimoValor(arbol.Raiz));
                    break;

                case 8:
                    Console.WriteLine("Maximo: " + arbol.Maximo(arbol.Raiz));
                    break;

                case 9:
                    Console.WriteLine("Altura: " + arbol.Altura(arbol.Raiz));
                    break;

                case 10:
                    arbol.Limpiar();
                    Console.WriteLine("Arbol limpio");
                    break;

            }

        } while (opcion != 0);
    }
}