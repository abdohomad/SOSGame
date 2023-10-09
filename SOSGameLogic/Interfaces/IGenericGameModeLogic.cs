using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSGameLogic.Interfaces
{
    public interface IGenericGameModeLogic
    {
        void CheckForSOS(int row, int col);
        bool HasWinner();
        int GetCurrentPlayerScore();
    }
}
