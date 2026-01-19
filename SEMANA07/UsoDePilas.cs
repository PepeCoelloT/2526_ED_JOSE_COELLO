using System;
using System.Collections.Generic;

class VerificarParentesisBalanceados
{
    static void Main()
    {
        Console.Write("Ingrese la expresión matemática: ");
        string expresion = Console.ReadLine();

        if (EstaBalanceada(expresion))
            Console.WriteLine("Fórmula balanceada.");
        else
            Console.WriteLine("Fórmula NO balanceada.");

        Console.ReadKey();
    }

    // Función que verifica si la expresión está balanceada
    static bool EstaBalanceada(string expresion)
    {
        Stack<char> pila = new Stack<char>();

        foreach (char caracter in expresion)
        {
            // Si es un símbolo de apertura, se apila
            if (caracter == '(' || caracter == '{' || caracter == '[')
            {
                pila.Push(caracter);
            }
            // Si es un símbolo de cierre
            else if (caracter == ')' || caracter == '}' || caracter == ']')
            {
                if (pila.Count == 0)
                    return false;

                char ultimo = pila.Pop();

                // Verificar correspondencia
                if (!EsPar(ultimo, caracter))
                    return false;
            }
        }

        // Si la pila queda vacía, está balanceada
        return pila.Count == 0;
    }

    // Verifica si los símbolos coinciden
    static bool EsPar(char apertura, char cierre)
    {
        return (apertura == '(' && cierre == ')') ||
               (apertura == '{' && cierre == '}') ||
               (apertura == '[' && cierre == ']');
    }
}