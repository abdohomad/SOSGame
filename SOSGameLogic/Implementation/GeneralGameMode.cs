using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSGameLogic.Implementation
{
    public class GeneralGameMode : IGenericGameModeLogic
    {
        public GeneralGameMode() {
        }
        public int CheckForSOS(char[,] board, List<Tuple<int, int>> player1Moves, List<Tuple<int, int>> player2Moves, int row, int col, char currentPlayerSymbol)
        {
            throw new NotImplementedException();
        }

        public int GetCurrentPlayerScore()
        {
            throw new NotImplementedException();
        }

        public bool HasWinner()
        {
            throw new NotImplementedException();
        }

       
    }
}
