using SOSGameLogic.Interfaces;
using SOSGameLogic.Implementation;

using System;
using System.Web;

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SOSGameUI
{
    public partial class SOSGame : System.Web.UI.Page
    {
        private IGame game;
        

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                // Initialize the UI with default values
                ddlBoardSize.SelectedValue = "5"; // Set default board size to 5x5
                rblGameMode.SelectedValue = "SinglePlayer"; // Set default game mode to Single Player

                //CreateGameBoard();
                UpdateUI();
            }
        }
        private void CreateGameBoard()
        {
            int boardSize = int.Parse(ddlBoardSize.SelectedValue);

            HtmlTable tblBoard = new HtmlTable();
            tblBoard.Attributes["class"] = "board-table";

            // Generate the game board dynamically
            for (int row = 0; row < boardSize; row++)
            {
                HtmlTableRow tableRowElement = new HtmlTableRow();
                for (int col = 0; col < boardSize; col++)
                {
                    HtmlTableCell cell = new HtmlTableCell
                    {
                        ID = $"cell_{row}_{col}"
                    };
                    cell.Attributes["class"] = "board-cell"; // Add CSS class for styling
                    cell.InnerText = ""; // Initially empty
                    cell.Attributes["onclick"] = $"MakeMove({row}, {col})"; // Attach click event
                    tableRowElement.Cells.Add(cell);
                }
                tblBoard.Rows.Add(tableRowElement);
            }
            gameBoardContainer.Controls.Add(tblBoard);
        }


        private void UpdateUI()
        {
            // Update the UI based on the game state
            // Display the current player and game status
            lblStatus.InnerText = $"Current Player: {GetCurrentPlayer()}";
        }

        private char GetCurrentPlayer()
        {
            // Implement logic to get the current player from your game instance
            if (game != null)
            {
                return game.GetCurrentPlayer();
            }
            return 'S'; // Default player 'S' for demonstration
        }

        protected void btnNewGame_Click(object sender, EventArgs e)
        {
            int boardSize = int.Parse(ddlBoardSize.SelectedValue);
           game = new Game(boardSize); // Create a new game instance

            CreateGameBoard();
            UpdateUI();
        }

        // Implement the MakeMove JavaScript function for handling cell clicks on the game board
        // You'll need to use JavaScript to allow players to place 'S' or 'O' symbols
        protected void Page_PreRender(object sender, EventArgs e)
        {
            int row = 0; // Replace with the actual row number
            int col = 0;
            char currentPlayer = GetCurrentPlayer(); // Implement this function to get the current player
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MakeMoveFunctionCall", $"MakeMove({row}, {col}, '{currentPlayer}');", true);
        }

       

    }

}