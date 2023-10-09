<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SOSGame.aspx.cs" Inherits="SOSGameUI.SOSGame" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SOS Game</title>
    <script src="MakeMove.js"></script>
    <style>
        /* Add your CSS styles here for the game board and UI elements */
        .board-cell {
            width: 40px;
            height: 40px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #000;
            font-size: 24px;
            cursor: pointer;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>SOS Game</h1>
            <div>
                <label for="ddlBoardSize">Select Board Size:</label>
                <asp:DropDownList ID="ddlBoardSize" runat="server">
                    <asp:ListItem Text="5x5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6x6" Value="6"></asp:ListItem>
             
                </asp:DropDownList>
            </div>
            <div>
                <label>Select Game Mode:</label>
                <asp:RadioButtonList ID="rblGameMode" runat="server">
                     <asp:ListItem Text="Simple game" Value="Simple game"></asp:ListItem>
                     <asp:ListItem Text="General game" Value="General game"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <asp:Button ID="btnNewGame" runat="server" Text="New Game" OnClick="btnNewGame_Click" />
            <br />
            <div>
                <label id="lblStatus" runat="server"></label>
            </div>
            <asp:PlaceHolder ID="gameBoardContainer" runat="server"></asp:PlaceHolder>
            <table id="tblBoard" runat="server">

            </table>
        </div>
        
<%--  --%>
    </form>
</body>
</html>

