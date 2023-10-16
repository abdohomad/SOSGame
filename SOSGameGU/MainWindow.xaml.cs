using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SOSGameGU
{
    public partial class MainWindow : Window
    {
        public IGame game;              // Reference to the game logic
        public IPlayer player1;         // Player 1
        public IPlayer player2;         // Player 2
        string player1Name;             // Name of Player 1
        string player2Name;             // Name of Player 2
        string currentPlayerTurnName;   // Name of the current player
        public int boardSize;
        public bool player1SymbolSelected = false;
        public bool player2SymbolSelected = false;
        public IGenericGameModeLogic _modeLogic;
        public char playerSymbol = ' '; // Player symbol (S or O)
        public readonly IPlayer currentPlayer;
        
        
      

        public MainWindow()
        {
            InitializeComponent();

            GameBoardGrid.SizeChanged += (sender, e) =>
            {
                double newWidth = e.NewSize.Width;
                double newHeight = e.NewSize.Height;
                GameCanvas.Width = newWidth;
                GameCanvas.Height = newHeight;

                int numberOfColumns = GameBoardGrid.ColumnDefinitions.Count;
                int numberOfRows = GameBoardGrid.RowDefinitions.Count;
                double cellWidth = 300 / numberOfColumns;
                double cellHeight = 300 / numberOfRows;
            };

           
            

            player1 = new Player(playerSymbol); // Initialize Player 1
            player2 = new Player(playerSymbol); // Initialize Player 2

            _modeLogic = new SimpleGameMode();
            game = new Game(boardSize, player1, player1, _modeLogic); // Initialize the game
        }

        // Event handler for when a radio button for game mode is checked
        public void RadioButton_Checked_Game_Mode(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                // Check which radio button was checked and update the selected game mode
                if (radioButton.Name == "rbSimpleMode")
                {
                    _modeLogic = new SimpleGameMode();

                }
                else if (radioButton.Name == "rbGeneralMode")
                {
                    _modeLogic = new GeneralGameMode();
                }

            }
        }

        // Event handler for the "Start Game" button click
        public void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected board size from the ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)ddlBoardSize.SelectedItem;

            if (selectedItem != null)
            {
                // Parse the selected item's content to get the board size
                string[] parts = selectedItem.Content.ToString().Split('x');
                int rows = int.Parse(parts[0]);
                int cols = int.Parse(parts[1]);
                Console.WriteLine("Selected board size: " + rows + "x" + cols);

                // Clear any existing rows and columns from the grid
                GameBoardGrid.RowDefinitions.Clear();
                GameBoardGrid.ColumnDefinitions.Clear();
                Console.WriteLine("Cleared existing rows and columns from the grid.");

                //cellWidth = boardWidth / cols;
///cellHeight = boardHeight / rows;
                player1Name = lblPlayer1.Content.ToString();
                player2Name = lblPlayer2.Content.ToString();
                currentPlayerTurnName = player1Name;
                txtCurrentPlayerTurn.Text = "Current Turn: " + currentPlayerTurnName;
                Console.WriteLine("Initialized player names and current player's turn.");
                // Add new rows and columns based on the selected board size
                for (int i = 0; i < rows; i++)
                {
                    GameBoardGrid.RowDefinitions.Add(new RowDefinition());
                    Console.WriteLine("Added new rows and columns to the grid.");
                }

                for (int j = 0; j < cols; j++)
                {
                    GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                StartGame(rows);

            }
            else
            {
                MessageBox.Show("Please select a board size before starting the game.");
            }
        }

        // Event handler for player symbol radio buttons
        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            IPlayer currentPlayer = null;

            // Determine which player's radio button was checked
            switch (radioButton.Name)
            {
                case "rbPlayer1S":
                case "rbPlayer1O":
                    currentPlayer = player1;
                    break;
                case "rbPlayer2S":
                case "rbPlayer2O":
                    currentPlayer = player2;
                    break;
                default:
                    break;
            }

            if (currentPlayer != null)
            {
                if (radioButton.Name.EndsWith("S"))
                {
                    currentPlayer.SetPlayerSymbol('S');
                    if (currentPlayer == player1)
                    {
                        player1SymbolSelected = true;
                    }
                    else if (currentPlayer == player2)
                    {
                        player2SymbolSelected = true;
                    }
                }
                else if (radioButton.Name.EndsWith("O"))
                {
                    currentPlayer.SetPlayerSymbol('O');
                    if (currentPlayer == player1)
                    {
                        player1SymbolSelected = true;
                    }
                    else if (currentPlayer == player2)
                    {
                        player2SymbolSelected = true;
                    }
                }
            }




            // Enable or disable the "Start Game" button based on symbol selections
            btnStartGame.IsEnabled = player1SymbolSelected || player2SymbolSelected;

        }

        // Start the game with the specified board size
        public void StartGame(int boardSize)
        {
            GameBoardGrid.Children.Clear();
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

                    cellButton.Click += Cell_Click;
                    Grid.SetRow(cellButton, row);
                    Grid.SetColumn(cellButton, col);
                    GameBoardGrid.Children.Add(cellButton);
                }
            }


            game = new Game(boardSize, player1, player2, _modeLogic);

        }


        public void Cell_Click(object sender, RoutedEventArgs e)
        {
            Button cellButton = (Button)sender;
            // Extract the row and column information from the button's Tag property
            Tuple<int, int> cellPosition = (Tuple<int, int>)cellButton.Tag;
            int row = cellPosition.Item1;
            int col = cellPosition.Item2;
            


          
            if (!player1SymbolSelected || !player2SymbolSelected)
            {
                MessageBox.Show("Both players must choose valid player symbols ('S' or 'O') before making a move.");
                return;
            }
            else if (game.IsCellOccupied(row, col))
            {
                MessageBox.Show("This cell is already occupied. Please choose an empty cell.");
                return;
            }
            else if (game.IsGameOver())
            {
                MessageBox.Show("The game is already over. Please start a new game.");
                return;

            }

          
            char currentPlayerSymbol = game.GetCurrentPlayer();
            game.MakeMove(row, col);
            cellButton.Content = currentPlayerSymbol.ToString();
            DrawLinesOnCanvas();
            currentPlayerTurnName = (currentPlayerTurnName == player1Name) ? player2Name : player1Name;
            txtCurrentPlayerTurn.Text = "Current Turn: " + currentPlayerTurnName;


        }

        private char GetLineType(SOSLineType sosLineType)
        {
            
            switch (sosLineType)
            {
                
                
                case SOSLineType.HorizontalWithMiddle:
                    return 'H';
                case SOSLineType.VerticalWithMiddle:
                    return 'V';
                case SOSLineType.DiagonalTopLeftToBottomRightWithMiddle:
                    return 'D';
                default:
                    return ' ';
            }
        }

 

        private Line CreateSOSLine(SOSLine sosLine, char lineType)
        {
            Line line = new Line
            {
                StrokeThickness = 5
            };

            // Set the stroke color based on the current player
            if (sosLine.Player == player1)
            {
                line.Stroke = Brushes.Blue;
            }
            else if (sosLine.Player == player2)
            {
                line.Stroke = Brushes.Red;
            }
            else
            {

                // Default to another color (you can change this as needed)
                line.Stroke = Brushes.Black;
            }

            SetLineCoordinates(line, sosLine, lineType);
            return line;
        }




        private void SetLineCoordinates(Line line, SOSLine sosLine, char lineType)
        {

            // Calculate cellWidth and cellHeight based on the size of GameBoardGrid
            int numberOfColumns = GameBoardGrid.ColumnDefinitions.Count;
            int numberOfRows = GameBoardGrid.RowDefinitions.Count;
            double cellWidth = GameBoardGrid.ActualWidth / numberOfColumns;
            double cellHeight = GameBoardGrid.ActualHeight / numberOfRows;

            // Determine the start and end points for the line based on the cell positions
            double startX = CalculateXPosition(sosLine.StartCol, cellWidth);
            double startY = CalculateYPosition(sosLine.StartRow, cellHeight);
            double endX = CalculateXPosition(sosLine.EndCol, cellWidth);
            double endY = CalculateYPosition(sosLine.EndRow, cellHeight);


            if (lineType == 'H')
            {
                // Horizontal line
                startY += cellHeight / 2;
                //endY += cellHeight / 2;
                endX = CalculateXPosition(sosLine.EndCol + 1, cellWidth) - cellWidth / 2;
                endY = CalculateYPosition(sosLine.StartRow, cellHeight) + cellHeight / 2;
            }
            else if (lineType == 'V')
            {
                // Vertical line
                startX += cellWidth / 2;
                endX += cellWidth / 2;
                endY = CalculateYPosition(sosLine.EndRow + 1, cellHeight); // Extend to the bottom of the last cell
            }
            else if (lineType == 'D')
            {

                startX = CalculateXPosition(sosLine.StartCol, cellWidth);
                startY = CalculateYPosition(sosLine.StartRow, cellHeight);
                endX = CalculateXPosition(sosLine.EndCol, cellWidth) + cellWidth;
                endY = CalculateYPosition(sosLine.EndRow, cellHeight) + cellHeight;



            }

            // Set the line coordinates for horizontal and vertical lines
            if (lineType == 'H' || lineType == 'V')
            {
                line.X1 = startX;
                line.Y1 = startY;
                line.X2 = endX;
                line.Y2 = endY;
          
             
            }


        }



            private double CalculateXPosition(int col, double cellWidth)
        {
            
            return col * cellWidth; // Assuming cellWidth is the width of a game board cell
        }

        private double CalculateYPosition(int row, double cellHeight)
        {
            
            return row * cellHeight; 
        }


        private bool IsCellInSOSLine(SOSLine sosLine, int row, int col)
        {
            return row >= sosLine.StartRow && row <= sosLine.EndRow &&
                   col >= sosLine.StartCol && col <= sosLine.EndCol;
        }

        private void DrawLinesOnCanvas()
        {
            //GameCanvas.Children.Clear();
            GameCanvas.Children.Clear(); // Clear the canvas
            List<SOSLine> sosLines = game.GetDetectedSOSLines();

            foreach (SOSLine sosLine in sosLines)
            {
                char lineType = GetLineType(sosLine.Type);

                // Check if this SOS line intersects with any cell on the game board
                bool shouldDraw = false;
                for (int row = sosLine.StartRow; row <= sosLine.EndRow; row++)
                {
                    for (int col = sosLine.StartCol; col <= sosLine.EndCol; col++)
                    {
                        if (IsCellInSOSLine(sosLine, row, col))
                        {
                            shouldDraw = true;
                            break;
                        }
                    }
                    if (shouldDraw)
                    {
                        break;
                    }
                }

                if (shouldDraw)
                {
                    Line line = CreateSOSLine(sosLine, lineType);
                    SetLineCoordinates(line, sosLine, lineType);
                    GameCanvas.Children.Add(line);
                }
            }

        }

    }
}

