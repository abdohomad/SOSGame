using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace SOSGameLogic.Implementation
{
    public abstract class GenericGameModeLogic : IGenericGameModeLogic
    {
         // Represents the current player

       

        // Calculate the score based on SOS patterns
        public void CheckForSOS(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol, IPlayer currentPlayer)
        {
        
           
            if (CheckHorizontal(board, playerMoves, row, col, currentPlayerSymbol) ||
                  CheckDiagonal(board, playerMoves, row, col, currentPlayerSymbol) ||
                  CheckVertical(board, playerMoves, row, col, currentPlayerSymbol))
            {
                currentPlayer.IncreaseScore(3);

            }
      
        }


        public SOSLine DetectSOSLine(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol, IPlayer currentPLayer)
        {
            // Check for SOS pattern horizontally, diagonally, and vertically
            if (CheckHorizontal(board, playerMoves, row, col, currentPlayerSymbol))
            {
                int startCol = col - 2;
                int middleCol = col - 1;
                int endCol = col;
                int middleRow = 0;
                int startRow = row;
                int endRow = row;

                // Create and return an SOSLine for a horizontal pattern with the middle cell
                return new SOSLine(startRow, startCol, endRow, endCol, middleRow, middleCol, SOSLineType.HorizontalWithMiddle, currentPLayer);
            }
            else if (CheckDiagonal(board, playerMoves, row, col, currentPlayerSymbol))
            {
                int startRow = row - 2;
                int startCol = col - 2;
                int middleRow = row - 1;
                int middleCol = col - 1;
                int endRow = row;
                int endCol = col;

                // Create and return an SOSLine for a diagonal pattern with the middle cell
                return new SOSLine(startRow, startCol, endRow, endCol, middleRow, middleCol, SOSLineType.DiagonalTopLeftToBottomRightWithMiddle, currentPLayer);
            }
            else if (CheckVertical(board, playerMoves, row, col, currentPlayerSymbol))
            {
                int startRow = row - 2;
                int middleRow = row - 1;
                int endRow = row;
                int startCol = col;
                int middleCol = col;
                int endCol = col;

                // Create and return an SOSLine for a vertical pattern with the middle cell
                return new SOSLine(startRow, startCol, endRow, endCol, middleRow, middleCol, SOSLineType.VerticalWithMiddle, currentPLayer);
            }

            return null; // No SOS line detected
        }



        private bool CheckHorizontal(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol)
        {
            int boardWidth = board.GetLength(1); // Width of the board

            int symbolsInARow = 0;

            // Check to the left
            for (int i = col - 1; i >= 0; i--)
            {
                if (board[row, i] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(row, i)))
                {
                    symbolsInARow++;
                }
                else
                {
                    break;
                }
            }

            // Check to the right
            for (int i = col + 1; i < boardWidth; i++)
            {
                if (board[row, i] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(row, i)))
                {
                    symbolsInARow++;
                }
                else
                {
                    break;
                }
            }

            return symbolsInARow >= 2 && (symbolsInARow % 3 == 2); // Check for "SOS" pattern
        }


        // Check for SOS pattern diagonally

        // Check for SOS pattern diagonally
        private bool CheckDiagonal(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol)
        {
            int boardWidth = board.GetLength(1); // Width of the board
            int boardHeight = board.GetLength(0); // Height of the board

            int symbolsInARow = 0;

            // Check diagonal from top-left to bottom-right
            int i, j;
            for (i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, j)))
                {
                    symbolsInARow++;
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
                    symbolsInARow++;
                }
                else
                {
                    break;
                }
            }

            if (symbolsInARow >= 2 && (symbolsInARow % 3 == 2))
            {
                return true;
            }

            // Check diagonal from top-right to bottom-left
            symbolsInARow = 0;
            for (i = row - 1, j = col + 1; i >= 0 && j < boardWidth; i--, j++)
            {
                if (board[i, j] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, j)))
                {
                    symbolsInARow++;
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
                    symbolsInARow++;
                }
                else
                {
                    break;
                }
            }

            return symbolsInARow >= 2 && (symbolsInARow % 3 == 2); // Check for "SOS" pattern
        }

        // Check for SOS pattern vertically
        private bool CheckVertical(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol)
        {
            int boardHeight = board.GetLength(0); // Height of the board

            int symbolsInARow = 0;

            // Check upward
            for (int i = row - 1; i >= 0; i--)
            {
                if (board[i, col] == currentPlayerSymbol || playerMoves.Contains(Tuple.Create(i, col)))
                {
                    symbolsInARow++;
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
                    symbolsInARow++;
                }
                else
                {
                    break;
                }
            }

            return symbolsInARow >= 2 && (symbolsInARow % 3 == 2); // Check for "SOS" pattern
        }



        // Determine the winner based on game-specific logic
        public abstract bool DetermineWinner(IPlayer player1, IPlayer player2);

        public abstract IPlayer GetWinner(IPlayer _player1, IPlayer _layer2);
        
            
        
    }
}
