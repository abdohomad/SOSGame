using SOSGameLogic.Implementation;
using System;
using System.Windows.Controls;
using System.Windows;

namespace SOSGameGU.GameManagers
{
    public class ActiveGameManager
    {
        private DrawSOSLineManager drawSOSLineManager;

        public ActiveGameManager(DrawSOSLineManager drawSOSLineManager)
        {
            this.drawSOSLineManager = drawSOSLineManager;
        }

        public void Cell_Click(object sender, Grid GameBoardGrid, MainWindow mainWindow, int boardSize, Canvas GameCanvas)
        {
            drawSOSLineManager = new DrawSOSLineManager();
            Button cellButton = (Button)sender;
            Tuple<int, int> cellPosition = (Tuple<int, int>)cellButton.Tag;
            int row = cellPosition.Item1;
            int col = cellPosition.Item2;

            if (!mainWindow.game.IsGameOver())
            {
                if (!mainWindow.player1SymbolSelected || !mainWindow.player2SymbolSelected)
                {
                    MessageBox.Show("Both players must choose valid player symbols");
                    return;
                }
                else if (mainWindow.game.IsCellOccupied(row, col))
                {
                    MessageBox.Show("This cell is already occupied.\n" +
                        "Please choose an empty cell.");
                    return;
                }
                else
                {
                    char currentPlayerSymbol = mainWindow.game.GetCurrentPlayerSymbol();
                    mainWindow.game.MakeMove(row, col);

                    cellButton.Content = currentPlayerSymbol.ToString();
                    cellButton.HorizontalAlignment = HorizontalAlignment.Center;
                    cellButton.VerticalAlignment = VerticalAlignment.Center;
                    cellButton.FontSize = 15;
                    drawSOSLineManager.DrawLinesOnCanvas(mainWindow,GameBoardGrid, GameCanvas);
                    mainWindow.txtPlayer1Score.Text = mainWindow.player1.GetScore().ToString();
                    mainWindow.txtPlayer2Score.Text = mainWindow.player2.GetScore().ToString();
                    if (mainWindow.game.GetCurrentPlayer() == mainWindow.player1)
                    {
                        mainWindow.currentPlayerTurnName = mainWindow.player1Name;
                    }
                    else if (mainWindow.game.GetCurrentPlayer() == mainWindow.player2)
                    {
                        mainWindow.currentPlayerTurnName = mainWindow.player2Name;
                    }

                    mainWindow.txtCurrentPlayerTurn.Text = "Current Turn: " + mainWindow.currentPlayerTurnName;
                }
            }
            CheckGameState(mainWindow);

        }

        private void CheckGameState(MainWindow mainWindow)
        {

            if (mainWindow.game.IsGameOver())
            {
                if (mainWindow._modeLogic is SimpleGameMode)
                {
                    if (mainWindow.player1.GetScore() >= 3)
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player1Name} wins!\n" +
                            $"with score{mainWindow.player1.GetScore()}");
                    }
                    else if (mainWindow.player2.GetScore() >= 3)
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player2Name} wins!\n" +
                            $"with score{mainWindow.player2.GetScore()}");
                    }
                    else
                    {
                        MessageBox.Show("Game Over! It's a Draw!");
                    }
                }
                else if (mainWindow._modeLogic is GeneralGameMode)
                {
                    // Add logic to determine the winner or draw in the GeneralGameMode
                    if (mainWindow.player1.GetScore() > mainWindow.player2.GetScore())
                    {
                        MessageBox.Show($"Game Over!{mainWindow.player1Name}wins!\n" +
                            $"with score{mainWindow.player1.GetScore()}");
                    }
                    else if (mainWindow.player2.GetScore() > mainWindow.player1.GetScore())
                    {
                        MessageBox.Show($"Game Over!{mainWindow.player2Name} wins!\n" +
                            $"with score{mainWindow.player2.GetScore()}");
                    }
                    else
                    {
                        MessageBox.Show("Game Over! It's a Draw!");
                    }
                }
            }
        }
        
    }
}
