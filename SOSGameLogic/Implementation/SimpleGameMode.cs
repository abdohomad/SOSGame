using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SOSGameLogic.Implementation
{
    public class SimpleGameMode : IGenericGameModeLogic
    {
        public  IBoard _board;
        private int _currantPlayerScore;


        public SimpleGameMode(int size)
        {
            _board = new Board(size);
            _currantPlayerScore = 0;
        }


        public int CheckForSOS(char[,] board, List<Tuple<int, int>> player1Moves, List<Tuple<int, int>> player2Moves, int row, int col, char currentPlayerSymbol)
        {
            int score = 0;

            if (CheckHorizontal(board, player1Moves, row, col, currentPlayerSymbol))
            {
                // Handle SOS for player 1
                score += 3;
            }

            if (CheckHorizontal(board, player2Moves, row, col, currentPlayerSymbol))
            {
                // Handle SOS for player 2
                score += 3;
            }

            // Continue checking for SOS based on other directions

            return score;
        }

       

        private bool CheckHorizontal(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol)
        {
            int count = 0;
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

        private bool CheckDiagonal(int row, int col, char currentPlayerSymbol)
        {
            throw new NotImplementedException();
        }

        private bool CheckVertical(int row, int col, char currentPlayerSymbol)
        {
            throw new NotImplementedException();
        }


      


        public bool HasWinner()
        {
            return true;
        }
    }
}
