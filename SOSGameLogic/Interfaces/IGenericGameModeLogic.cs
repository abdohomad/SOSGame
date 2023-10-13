using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSGameLogic.Interfaces
{
    public interface IGenericGameModeLogic
    {
        int CheckForSOS(char[,] board, List<Tuple<int, int>> player1Moves, List<Tuple<int, int>> player2Moves, int row, int col, char current);
        bool HasWinner();
    }
}
