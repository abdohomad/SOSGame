using SOSGameLogic.Interfaces;

namespace SOSGameLogic.Implementation
{
    public class Player : IPlayer
    {
        private char playerSymbol; // Stores the player's symbol ('S' or 'O')
        private int score; // Stores the player's score

        public Player(char symbol)
        {
            playerSymbol = symbol; // Initialize the player's symbol
            score = 0; // Initialize the player's score to zero
        }

        // Returns the player's symbol ('S' or 'O')
        public char GetPlayerSymbol()
        {
            return playerSymbol;
        }

        // Increases the player's score by the given points
        public void IncreaseScore(int points)
        {
            score += points;
        }

        // Returns the player's current score
        public int GetScore()
        {
            return score;
        }

        // Sets the player's symbol to the specified symbol
        public void SetPlayerSymbol(char symbol)
        {
            playerSymbol = symbol;
        }
    }
}
