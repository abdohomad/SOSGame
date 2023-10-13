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
            else
            {
                // It's a draw
                return false;
            }
        }

    }

}
