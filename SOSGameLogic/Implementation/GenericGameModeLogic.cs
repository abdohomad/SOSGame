using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace SOSGameLogic.Implementation
{
    public abstract class GenericGameModeLogic : IGenericGameModeLogic
    {
        int count = 0;
        // Calculate the score based on SOS patterns
        public int CheckForSOS(char[,] board, List<Tuple<int, int>> player1Moves, List<Tuple<int, int>> player2Moves, int row, int col, char currentPlayerSymbol)
        {
            int score = 0;


            // Handle SOS for player 1
            if (CheckHorizontal(board, player1Moves, row, col, currentPlayerSymbol) ||
                  CheckDiagonal(board, player1Moves, row, col, currentPlayerSymbol) ||
                  CheckVertical(board, player1Moves, row, col, currentPlayerSymbol))
            {
                score += 3;
            }

            // Handle SOS for player 2
            if (CheckHorizontal(board, player2Moves, row, col, currentPlayerSymbol) ||
                  CheckDiagonal(board, player2Moves, row, col, currentPlayerSymbol) ||
                  CheckVertical(board, player2Moves, row, col, currentPlayerSymbol))
            {
                score += 3;
            }

            return score;
        }

        // Check for SOS pattern horizontally
        private bool CheckHorizontal(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol)
        {
            
            int boardWidth = board.GetLength(1); // Width of the board

            // Check to the left
            for (int i = col - 1; i >= 0; i--)
            {
                if (i >= 0 && (board[row, i] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(row, i))))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            // Check to the right
            for (int i = col + 1; i < boardWidth; i++)
            {
                if (i < boardWidth && (board[row, i] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(row, i))))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count >= 2; // At least 3 symbols in a row
        }

        // Check for SOS pattern diagonally
        private bool CheckDiagonal(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol)
        {
            
            int boardWidth = board.GetLength(1); // Width of the board
            int boardHeight = board.GetLength(0); // Height of the board

            // Check diagonal from top-left to bottom-right
            int i, j;
            for (i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, j)))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            for (i = row + 1, j = col + 1; i < boardHeight && j < boardWidth; i++, j++)
            {
                if (board[i, j] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, j)))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            if (count >= 2) return true;

            // Check diagonal from top-right to bottom-left
            for (i = row - 1, j = col + 1; i >= 0 && j < boardWidth; i--, j++)
            {
                if (board[i, j] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, j)))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            for (i = row + 1, j = col - 1; i < boardHeight && j >= 0; i++, j--)
            {
                if (board[i, j] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, j)))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count >= 2; // At least 3 symbols in a row
        }

        // Check for SOS pattern vertically
        private bool CheckVertical(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol)
        {
            
            int boardHeight = board.GetLength(0); // Height of the board

            // Check upward
            for (int i = row - 1; i >= 0; i--)
            {
                if (board[i, col] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, col)))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            // Check downward
            for (int i = row + 1; i < boardHeight; i++)
            {
                if (board[i, col] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, col)))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count >= 2; // At least 3 symbols in a row
        }

        // Determine the winner based on game-specific logic
        public abstract bool DetermineWinner(IPlayer player1, IPlayer player2);
    }
}
