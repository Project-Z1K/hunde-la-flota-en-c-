using System;

class BatallaNaval
{
    const int TAMANIO_TABLERO = 8;
    static char[,] tablero = new char[TAMANIO_TABLERO, TAMANIO_TABLERO];
    static int barcosRestantes = 10;
    static int disparosRestantes = 30;

    static void InicializarTablero()
    {
        for (int i = 0; i < TAMANIO_TABLERO; ++i)
        {
            for (int j = 0; j < TAMANIO_TABLERO; ++j)
            {
                tablero[i, j] = '-';
            }
        }
    }

    static void ImprimirTablero()
    {
        Console.Write("  ");
        for (int i = 0; i < TAMANIO_TABLERO; ++i)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        for (int i = 0; i < TAMANIO_TABLERO; ++i)
        {
            Console.Write(i + " ");
            for (int j = 0; j < TAMANIO_TABLERO; ++j)
            {
                Console.Write(tablero[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static void ColocarBarcosAleatorios()
    {
        Random rnd = new Random();
        int barcos = 0;
        while (barcos < barcosRestantes)
        {
            int fila = rnd.Next(0, TAMANIO_TABLERO);
            int columna = rnd.Next(0, TAMANIO_TABLERO);
            if (tablero[fila, columna] == '-')
            {
                tablero[fila, columna] = 'X';
                barcos++;
            }
        }
    }

    static void JugarBatallaNaval()
    {
        InicializarTablero();
        ColocarBarcosAleatorios();

        while (disparosRestantes > 0)
        {
            Console.WriteLine($"Disparos restantes: {disparosRestantes}");
            ImprimirTablero();
            Console.Write("Ingresa la fila para disparar: ");
            int fila = int.Parse(Console.ReadLine());
            Console.Write("Ingresa la columna para disparar: ");
            int columna = int.Parse(Console.ReadLine());

            if (fila < 0 || fila >= TAMANIO_TABLERO || columna < 0 || columna >= TAMANIO_TABLERO)
            {
                Console.WriteLine("Disparo fuera de rango. Inténtalo de nuevo.");
                continue;
            }

            if (tablero[fila, columna] == 'X')
            {
                tablero[fila, columna] = 'H';
                Console.WriteLine("¡Has hundido un barco!");
                barcosRestantes--;
            }
            else if (tablero[fila, columna] == '-')
            {
                tablero[fila, columna] = 'O';
                Console.WriteLine("Agua... ¡Intenta con otro disparo!");
            }
            else
            {
                Console.WriteLine("Ya has disparado en esta posición. Inténtalo de nuevo.");
                continue;
            }

            disparosRestantes--;

            if (barcosRestantes == 0)
            {
                ImprimirTablero();
                Console.WriteLine("¡Felicidades! ¡Has hundido todos los barcos!");
                break;
            }
        }

        if (disparosRestantes == 0 && barcosRestantes > 0)
        {
            Console.WriteLine("¡Te has quedado sin disparos! ¡Has perdido!");
            Console.WriteLine($"Barcos restantes: {barcosRestantes}");
        }
    }

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("*** BATALLA NAVAL ***");
            Console.WriteLine("1. Jugar");
            Console.WriteLine("2. Salir");
            Console.Write("Elige una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    barcosRestantes = 10;
                    disparosRestantes = 30;
                    JugarBatallaNaval();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción inválida. Inténtalo de nuevo.");
                    break;
            }
        }
    }
}
