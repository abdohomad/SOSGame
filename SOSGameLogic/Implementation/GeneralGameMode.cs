using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace SOSGameLogic.Implementation
{
    public class GeneralGameMode : GenericGameModeLogic
    {
       
        public GeneralGameMode() { }

        public override bool DetermineWinner(IPlayer player1, IPlayer player2)
        {

            int player1Score = player1.GetScore();
            int player2Score = player2.GetScore();


            if (player1Score > player2Score)
            {
                // Player 1 wins
                return true;
            }
            else if (player2Score > player1Score)
            {
                // Player 2 wins
                return true;
            }
             
            return false;
        }
        public override IPlayer GetWinner(IPlayer _player1, IPlayer _player2)
        {
            IPlayer Winner = null;

            if (_player1.GetScore() > _player2.GetScore())
            {
                // Player 1 wins
                Winner = _player1;
            }
            else if (_player2.GetScore() > _player1.GetScore())
            {
                // Player 2 wins
                Winner = _player2;
            }
           return Winner;
        }
    }

}
