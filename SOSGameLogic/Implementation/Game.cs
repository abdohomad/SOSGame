using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace SOSGameLogic.Implementation
{
    public class Game : IGame
    {
        private readonly IBoard board; // Represents the game board
        private IPlayer currentPlayer; // Represents the current player
        private readonly IPlayer _player1; // Player 1
        private readonly IPlayer _player2; // Player 2
        private readonly List<Tuple<int, int>> _player1Moves;
        private readonly List<Tuple<int, int>> _player2Moves;
        private IGenericGameModeLogic _modeLogic;
        public Game(int size, IPlayer player1, IPlayer player2, IGenericGameModeLogic modeLogic)
        {
            board = new Board(size); // Initialize the game board with the specified size
            _player1 = player1; // Initialize Player 1
            _player2 = player2; // Initialize Player 2
            currentPlayer = player1; // Set the current player to Player 1 at the start of the game
            _player1Moves = new List<Tuple<int, int>>();
            _player2Moves = new List<Tuple<int, int>>();
            _modeLogic = modeLogic;
        }

     
        // Returns the symbol of the current player ('S' or 'O')
        public char GetCurrentPlayer()
        {
            char currentPlayersSymbol = currentPlayer.GetPlayerSymbol();
            Console.WriteLine($"Current player is {currentPlayersSymbol}");
            return currentPlayersSymbol;
        }

        // Checks if a cell on the game board is occupied by a symbol
        public bool IsCellOccupied(int row, int col)
        {
            return board.GetSymbolAt(row, col) != ' ';
        }

        // Checks if the game is over
        public bool IsGameOver(){  

             if (_modeLogic is SimpleGameMode)
            {
                // Game over when one player scores 3
                return _modeLogic.DetermineWinner(_player1, _player2);
            }
            else if (_modeLogic is GeneralGameMode)
            {
                // Game over when the board is full.
                if (board.IsBoardFull())
                {
                    return _modeLogic.DetermineWinner(_player1, _player2);
                }
            }
          
            return false;
        }

            // Allows a player to make a move by placing their symbol on the board
        public void MakeMove(int row, int col)
        { 
                if (!IsGameOver())
                {
                    if (board.IsValidMove(row, col))
                    {
                        char currentPlayerSymbol = currentPlayer.GetPlayerSymbol();
                        board.PlaceSymbol(row, col, currentPlayerSymbol);
                        if (currentPlayer == _player1)
                        {
                             _player1Moves.Add(new Tuple<int, int>(row, col));
                        }
                        else if (currentPlayer == _player2)
                        {
                            _player2Moves.Add(new Tuple<int, int>(row, col));
                        }
                        int score =  _modeLogic.CheckForSOS(board.GetBoard(), _player1Moves, _player2Moves, row, col, currentPlayerSymbol);
                        currentPlayer.IncreaseScore(score);
                        SwitchPlayer();
                    }

                }

        }

        // Switches the current player between Player 1 and Player 2
        public void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == _player1) ? _player2 : _player1;
        }

        

    }
}
