using SOSGameLogic.Interfaces;

namespace SOSGameLogic.Implementation
{
    public class Board : IBoard
    {
        public char[,] board; // Represents the game board as a 2D character array

        public Board(int size)
        {
            board = new char[size, size]; // Initialize the game board with the specified size
            InitializeBoard(); // Initialize the game board with empty spaces
        }

        // Initializes the game board with empty spaces
        private void InitializeBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = ' '; // Initialize all cells with empty spaces
                }
            }
        }

        // Returns the game board as a 2D character array
        public char[,] GetBoard()
        {
            return board;
        }

        // Checks if a move to the specified row and column is valid
        public bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < board.GetLength(0) &&
                   col >= 0 && col < board.GetLength(1) &&
                   board[row, col] == ' ';
        }

        // Places the specified symbol at the given row and column on the board
        public void PlaceSymbol(int row, int col, char symbol)
        {
            board[row, col] = symbol;
        }

        // Retrieves the symbol at the specified row and column on the board
        public char GetSymbolAt(int row, int col)
        {
            return board[row, col];
        }

        // Checks if the board is full (no empty spaces remain)
        public bool IsBoardFull()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false; // If an empty cell is found, the board is not full
                    }
                }
            }
            return true; // If no empty cells are found, the board is full
        }
    }
}
