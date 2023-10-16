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

        public override IPlayer GetWinner(IPlayer _player1, IPlayer _player2)
        {
            IPlayer winnerPlayer = null;
          
           if(_player1.GetScore() >= 3)
            {
                winnerPlayer = _player1;
            }
           else if (_player2.GetScore() >= 3)
            { winnerPlayer = _player2; }
            else
            {
                return null;
            }
           return winnerPlayer;
        }



    }
}
