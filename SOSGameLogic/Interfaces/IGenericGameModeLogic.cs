using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSGameLogic.Interfaces
{
    public interface IGenericGameModeLogic
    {
        void CheckForSOS(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol, IPlayer currentPlayer);
        SOSLine DetectSOSLine(char[,] board, List<Tuple<int, int>> playerMoves, int row, int col, char currentPlayerSymbol, IPlayer currentPLayer);
        bool DetermineWinner(IPlayer _player1, IPlayer _player2);
        IPlayer GetWinner(IPlayer _player1, IPlayer _layer2);


    }
}
