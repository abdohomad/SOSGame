using SOSGameLogic.Implementation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SOSGameGU.GameManagers
{
    public class GameSetupManager
    {
        public GameSetupManager()
        {
        }

        public void StartGame(Grid GameBoardGrid, MainWindow mainWindow, int boardSize)
        {
            double gridWidth = GameBoardGrid.ActualWidth;
            double gridHeight = GameBoardGrid.ActualHeight;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Button cellButton = new Button
                    {
                        Width = gridWidth / boardSize,
                        Height = gridHeight / boardSize,
                        Content = new TextBlock
                        {
                            TextAlignment = TextAlignment.Center, // Center-align the text
                            VerticalAlignment = VerticalAlignment.Center, // Vertically center-align the text
                            HorizontalAlignment = HorizontalAlignment.Center,
                        },
                        Tag = new Tuple<int, int>(row, col)
                    };

                    cellButton.Click += mainWindow.Cell_Click;
                    Grid.SetRow(cellButton, row);
                    Grid.SetColumn(cellButton, col);
                    GameBoardGrid.Children.Add(cellButton);
                }
            }

            mainWindow.game = new Game(boardSize, mainWindow.player1, mainWindow.player2, mainWindow._modeLogic);
        }
    }
}
