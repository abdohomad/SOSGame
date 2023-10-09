function MakeMove(row, col) {
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
            player: 'S' // Assuming 'S' for the current player
        };

        xhr.onload = function () {
            if (xhr.status === 200) {
                // Successful response, update the cell with the player's symbol
                cell.innerText = data.player;

                // Implement logic to check for SOS formation and determine the winner
                // You'll need to handle this on the server-side
                // Here, you can check the response from the server for the game status

                // Example:
                if (xhr.responseText === 'GameWon') {
                    // Display a message to indicate the game has been won
                    alert('Game Over: Player ' + data.player + ' wins!');
                } else {
                    // Continue the game
                }
            } else {
                // Handle errors here
                alert('Error: Unable to make a move.');
            }
        };

        xhr.send(JSON.stringify(data));
    }
}
