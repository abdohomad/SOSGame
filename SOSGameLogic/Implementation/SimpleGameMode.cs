using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SOSGameLogic.Implementation
{
    public class SimpleGameMode : GenericGameModeLogic
    {

        public SimpleGameMode() { }
       
        public override bool DetermineWinner(IPlayer _player1, IPlayer _player2)
        {
            int score = 3;
            if (_player1.GetScore() >= score || _player2.GetScore() >= score)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
