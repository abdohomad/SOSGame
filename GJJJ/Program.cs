using System;

namespace SOSGame
{
    // Enum to represent the players
    public enum Player
    {
        Blue,
        Red
    }

    // Class to represent the SOS game board
    public class SOSBoard
    {
        private readonly char[,] board;
        public int Size { get; }

        public SOSBoard(int size)
        {
            Size = size;
            board = new char[size, size];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("\n   " + string.Join("   ", new string[] { "1", "2", "3" }));
            for (int i = 0; i < Size; i++)
            {
                Console.Write(i + 1 + "  ");
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(board[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }

        public void PlaceSOS(Player player, int row, int col)
        {
            char symbol = player == Player.Blue ? 'S' : 'O';
            board[row, col] = symbol;
        }
    }

    // Class to manage the game
    public class SOSGame
    {
        private SOSBoard board;
        private Player currentPlayer;

        public SOSGame(int boardSize)
        {
            board = new SOSBoard(boardSize);
            currentPlayer = Player.Blue;
        }

        public void Play()
        {
            bool gameOver = false;
            while (!gameOver)
            {
                board.PrintBoard();
                Console.WriteLine($"Current turn: {currentPlayer.ToString().ToLower()}");

                Console.Write("Enter row (1-3): ");
                int row = int.Parse(Console.ReadLine()) - 1;

                Console.Write("Enter column (1-3): ");
                int col = int.Parse(Console.ReadLine()) - 1;

                if (IsValidMove(row, col))
                {
                    board.PlaceSOS(currentPlayer, row, col);
                    currentPlayer = currentPlayer == Player.Blue ? Player.Red : Player.Blue;
                }
                else
                {
                    Console.WriteLine("Invalid move. Try again.");
                }

                // Implement SOS checking and winner determination here.
            }
        }

        private bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < board.Size && col >= 0 && col < board.Size && board[row, col] == ' ';
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SOS - Simple Game\n");

            int boardSize = 3; // Change the board size as needed
            SOSGame game = new SOSGame(boardSize);
            game.Play();
        }
    }
}
