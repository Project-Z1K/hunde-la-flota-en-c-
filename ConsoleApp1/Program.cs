using System;
using System.Diagnostics;

class Battleship
{
    const int BOARD_SIZE = 10;
    const int MAX_SHIPS = 20;
    const int MAX_SHOTS = 50;

    static char[,] board = new char[BOARD_SIZE, BOARD_SIZE];
    static int shipsRemaining = MAX_SHIPS;
    static int shotsRemaining = MAX_SHOTS;
    static ConsoleKeyInfo keyForRow = new ConsoleKeyInfo('W', ConsoleKey.W, false, false, false);
    static ConsoleKeyInfo keyForColumn = new ConsoleKeyInfo('A', ConsoleKey.A, false, false, false);
    static ConsoleKeyInfo keyForShoot = new ConsoleKeyInfo('S', ConsoleKey.S, false, false, false);

    static Random rnd = new Random();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("*** BATTLESHIP ***");
            Console.WriteLine("1. Play");
            Console.WriteLine("2. Difficulty");
            Console.WriteLine("3. Credits");
            Console.WriteLine("4. Open YouTube Channel");
            Console.WriteLine("5. Customize Keys");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid option. Please enter a number.");
                continue;
            }

            switch (option)
            {
                case 1:
                    shipsRemaining = MAX_SHIPS;
                    shotsRemaining = MAX_SHOTS;
                    PlayBattleship();
                    break;
                case 2:
                    SetDifficulty();
                    break;
                case 3:
                    ShowCredits();
                    break;
                case 4:
                    OpenYouTubeChannel();
                    break;
                case 5:
                    CustomizeKeys();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void PlayBattleship()
    {
        InitializeBoard();
        PlaceRandomShips();

        while (shotsRemaining > 0 && shipsRemaining > 0)
        {
            Console.WriteLine($"Shots remaining: {shotsRemaining}");
            PrintBoard();
            Console.Write("Enter the row to shoot: ");
            int row = int.Parse(Console.ReadLine());
            Console.Write("Enter the column to shoot: ");
            int column = int.Parse(Console.ReadLine());

            if (!IsValidShot(row, column))
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
            else
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

    static void SetDifficulty()
    {
        Console.WriteLine("Difficulty levels:");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. Hard");
        Console.Write("Choose a difficulty level: ");
        int level;
        if (!int.TryParse(Console.ReadLine(), out level) || level < 1 || level > 3)
        {
            Console.WriteLine("Invalid option. Selecting medium level by default.");
            level = 2;
        }

        switch (level)
        {
            case 1:
                shipsRemaining = 15;
                shotsRemaining = 40;
                break;
            case 2:
                shipsRemaining = MAX_SHIPS;
                shotsRemaining = MAX_SHOTS;
                break;
            case 3:
                shipsRemaining = 25;
                shotsRemaining = 60;
                break;
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

    static void OpenYouTubeChannel()
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

    static void CustomizeKeys()
    {
        Console.WriteLine("Customize Keys:");
        Console.WriteLine("Enter the key for row selection:");
        keyForRow = Console.ReadKey();
        Console.WriteLine("\nEnter the key for column selection:");
        keyForColumn = Console.ReadKey();
        Console.WriteLine("\nEnter the key for shooting:");
        keyForShoot = Console.ReadKey();
        Console.WriteLine("\nKeys customized successfully!");
    }

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

    static bool IsValidShot(int row, int column)
    {
        return row >= 0 && row < BOARD_SIZE && column >= 0 && column < BOARD_SIZE;
    }
}
