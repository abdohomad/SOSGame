// sosGame.js
function MakeMove(row, col, currentPlayer) {
    var cellId = 'cell_' + row + '_' + col;
    var cell = document.getElementById(cellId);

    // Check if the cell is empty before making a move
    if (cell.innerText === '') {
        // Send an AJAX request to update the server-side game state
        var xhr = new XMLHttpRequest();
        xhr.open('POST', 'SOSGame.aspx/MakeMove', true);
        xhr.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');

        var data = {
            row: row,
            col: col,
            player: currentPlayer // Get the current player from the server
        };

        xhr.onload = function () {
            if (xhr.status === 200) {
                // Successful response, update the cell with the player's symbol
                cell.innerText = data.player;

                // Implement logic to check for SOS and determine the winner
                // You'll need to handle this on the server-side
            } else {
                // Handle errors here
            }
        };

        xhr.send(JSON.stringify(data));
    }
}
