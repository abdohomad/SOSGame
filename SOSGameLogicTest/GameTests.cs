using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;

namespace SOSGameLogicTest
{
    public class GameTests
    {
        [Fact]
        public void GetCurrentPlayer_ReturnsInitialPlayerSymbol()
        {
            // Arrange
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            IGame game = new Game(3, player1, player2);

            // Act
            char currentPlayerSymbol = game.GetCurrentPlayer();

            // Assert
            Assert.Equal('S', currentPlayerSymbol); // Assuming player1 starts
        }

        [Theory]
        [InlineData(0, 0, true)] // Valid move to an empty cell
        [InlineData(1, 1, false)] // Invalid move to a non-empty cell
        [InlineData(4, 4, false)] // Invalid move outside of the board
        public void IsCellOccupied_ReturnsExpectedResult(int row, int col, bool expectedResult)
        {
            // Arrange
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            IGame game = new Game(4, player1, player2);

            // Act
            bool isOccupied = game.IsCellOccupied(row, col);

            // Assert
            Assert.Equal(expectedResult, isOccupied);
        }

        [Fact]
        public void IsGameOver_EmptyBoard_ReturnsFalse()
        {
            // Arrange
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            IGame game = new Game(3, player1, player2);

            // Act
            bool isGameOver = game.IsGameOver();

            // Assert
            Assert.False(isGameOver);
        }

        [Fact]
        public void MakeMove_PlacesSymbolOnBoard()
        {
            // Arrange
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            IGame game = new Game(3, player1, player2);

            // Act
            game.MakeMove(0, 0);
            char symbol = game.GetCurrentPlayer(); // Assuming player1 starts

            // Assert
            Assert.Equal(symbol, game.GetCurrentPlayer());
            Assert.True(game.IsCellOccupied(0, 0));
        }

        [Fact]
        public void SwitchPlayer_SwitchesPlayerCorrectly()
        {
            // Arrange
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            IGame game = new Game(3, player1, player2);

            // Act
            game.SwitchPlayer();

            // Assert
            Assert.Equal('O', game.GetCurrentPlayer());
        }

   
    }
}
