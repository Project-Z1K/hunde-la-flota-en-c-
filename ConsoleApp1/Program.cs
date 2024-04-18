using System;
using System.Diagnostics;

class Battleship
{
    const int BOARD_SIZE = 10;
    static char[,] board = new char[BOARD_SIZE, BOARD_SIZE];
    static int shipsRemaining = 20;
    static int shotsRemaining = 50;

    static void InitializeBoard()
    {
        for (int i = 0; i < BOARD_SIZE; ++i)
        {
            for (int j = 0; j < BOARD_SIZE; ++j)
            {
                board[i, j] = '-';
            }
        }
    }

    static void PrintBoard()
    {
        Console.Write("  ");
        for (int i = 0; i < BOARD_SIZE; ++i)
        {
            Console.Write($"{i} ");
        }
        Console.WriteLine();
        for (int i = 0; i < BOARD_SIZE; ++i)
        {
            Console.Write($"{i} ");
            for (int j = 0; j < BOARD_SIZE; ++j)
            {
                if (board[i, j] == 'X')
                {
                    Console.Write("X ");
                }
                else if (board[i, j] == 'H')
                {
                    Console.Write("H ");
                }
                else if (board[i, j] == 'O')
                {
                    Console.Write("O ");
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

    static void PlaceRandomShips()
    {
        Random rnd = new Random();
        int ships = 0;
        while (ships < shipsRemaining)
        {
            int row = rnd.Next(0, BOARD_SIZE);
            int column = rnd.Next(0, BOARD_SIZE);
            if (board[row, column] == '-')
            {
                board[row, column] = 'X';
                ships++;
            }
        }
    }

    static void PlayBattleship()
    {
        InitializeBoard();
        PlaceRandomShips();

        while (shotsRemaining > 0)
        {
            Console.WriteLine($"Shots remaining: {shotsRemaining}");
            PrintBoard();
            Console.Write("Enter the row to shoot: ");
            int row = int.Parse(Console.ReadLine());
            Console.Write("Enter the column to shoot: ");
            int column = int.Parse(Console.ReadLine());

            if (row < 0 || row >= BOARD_SIZE || column < 0 || column >= BOARD_SIZE)
            {
                Console.WriteLine("Shot out of range. Try again.");
                continue;
            }

            if (board[row, column] == 'X')
            {
                board[row, column] = 'H';
                Console.WriteLine("You've sunk a ship!");
                shipsRemaining--;
            }
            else if (board[row, column] == '-')
            {
                board[row, column] = 'O';
                Console.WriteLine("Miss... Try another shot!");
            }
            else if (board[row, column] == 'H' || board[row, column] == 'O')
            {
                Console.WriteLine("You've already shot at this position. Try again.");
                continue;
            }

            shotsRemaining--;

            if (shipsRemaining == 0)
            {
                PrintBoard();
                Console.WriteLine("Congratulations! You've sunk all the ships!");
                break;
            }
        }

        if (shotsRemaining == 0 && shipsRemaining > 0)
        {
            Console.WriteLine("Out of shots! You've lost!");
            Console.WriteLine($"Ships remaining: {shipsRemaining}");
        }
    }

    static void ShowCredits()
    {
        Console.WriteLine("***** CREDITS *****");
        Console.WriteLine("Developed by:");
        Console.WriteLine("Andreu1k");
        Console.WriteLine("andreu1k on YouTube");
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
            Console.WriteLine("*** BATTLESHIP ***");
            Console.WriteLine("1. Play");
            Console.WriteLine("2. Difficulty");
            Console.WriteLine("3. Credits");
            Console.WriteLine("4. Open YouTube Channel");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    shipsRemaining = 20;
                    shotsRemaining = 50;
                    PlayBattleship();
                    break;
                case 2:
                    Console.WriteLine("Difficulty levels:");
                    Console.WriteLine("1. Easy");
                    Console.WriteLine("2. Medium");
                    Console.WriteLine("3. Hard");
                    Console.Write("Choose a difficulty level: ");
                    int level = int.Parse(Console.ReadLine());
                    switch (level)
                    {
                        case 1:
                            shipsRemaining = 15;
                            shotsRemaining = 40;
                            break;
                        case 2:
                            shipsRemaining = 20;
                            shotsRemaining = 50;
                            break;
                        case 3:
                            shipsRemaining = 25;
                            shotsRemaining = 60;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Selecting medium level by default.");
                            break;
                    }
                    break;
                case 3:
                    ShowCredits();
                    break;
                case 4:
                    AbrirCanalYT();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
