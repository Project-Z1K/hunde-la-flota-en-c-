using System;
using System.Diagnostics;

class BatallaNaval
{
    const int TAMANIO_TABLERO = 10;
    static char[,] tablero = new char[TAMANIO_TABLERO, TAMANIO_TABLERO];
    static int barcosRestantes = 20;
    static int disparosRestantes = 50;

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
            Console.Write($"\x1b[33m{i}\x1b[0m ");
        }
        Console.WriteLine();
        for (int i = 0; i < TAMANIO_TABLERO; ++i)
        {
            Console.Write($"\x1b[33m{i}\x1b[0m ");
            for (int j = 0; j < TAMANIO_TABLERO; ++j)
            {
                if (tablero[i, j] == 'X')
                {
                    Console.Write("\x1b[36mX\x1b[0m ");
                }
                else if (tablero[i, j] == 'H')
                {
                    Console.Write("\x1b[31mH\x1b[0m ");
                }
                else if (tablero[i, j] == 'O')
                {
                    Console.Write("\x1b[34mO\x1b[0m ");
                }
                else
                {
                    Console.Write("- ");
                }
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
            else if (tablero[fila, columna] == 'H' || tablero[fila, columna] == 'O')
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

    static void MostrarCreditos()
    {
        Console.WriteLine("***** CREDITOS *****");
        Console.WriteLine("Desarrollado por:");
        Console.WriteLine("Andreu1k");
        Console.WriteLine("andreu1k on yt");
        Console.WriteLine("*********************");
    }

    static void AbrirCanalYT()
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd",
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process proc = Process.Start(psi);
        if (proc != null)
        {
            proc.StandardInput.WriteLine("start https://www.youtube.com/channel/UC1-tOvP6-SGgyxJwD9nZiPQ");
        }
    }

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("*** BATALLA NAVAL ***");
            Console.WriteLine("1. Jugar");
            Console.WriteLine("2. Dificultad");
            Console.WriteLine("3. Créditos");
            Console.WriteLine("4. Abrir Canal de YouTube");
            Console.WriteLine("5. Salir");
            Console.Write("Elige una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    barcosRestantes = 20;
                    disparosRestantes = 50;
                    JugarBatallaNaval();
                    break;
                case 2:
                    Console.WriteLine("Niveles de dificultad:");
                    Console.WriteLine("1. Fácil");
                    Console.WriteLine("2. Medio");
                    Console.WriteLine("3. Difícil");
                    Console.Write("Elige un nivel de dificultad: ");
                    int nivel = int.Parse(Console.ReadLine());
                    switch (nivel)
                    {
                        case 1:
                            barcosRestantes = 15;
                            disparosRestantes = 40;
                            break;
                        case 2:
                            barcosRestantes = 20;
                            disparosRestantes = 50;
                            break;
                        case 3:
                            barcosRestantes = 25;
                            disparosRestantes = 60;
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Seleccionando nivel medio por defecto.");
                            break;
                    }
                    break;
                case 3:
                    MostrarCreditos();
                    break;
                case 4:
                    AbrirCanalYT();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción inválida. Inténtalo de nuevo.");
                    break;
            }
        }
    }
}
