namespace TikTakToe
{
    public class Game
    {
        private Field _field;
        private IPrintGame _printGame;
        private Marks _currentPlayer;

        public Game(IPrintGame printGame)
        {
            _field = new Field();
            _printGame = printGame;
            _currentPlayer = Marks.X; // Start with player X
        }

        public void Run()
        {
            bool gameWon = false;
            bool gameTied = false;

            do
            {
                _printGame.ShowField(_field);
                _printGame.ShowPlayerTurn(_currentPlayer);

                int[] userMove = _printGame.GetUserMarkPlace();
                int row = userMove[0];
                int col = userMove[1];

                if (_field.SetMark(row, col, _currentPlayer))
                {
                    gameWon = CheckForWin();
                    gameTied = CheckForTie();

                    // Switch to the next player
                    _currentPlayer = (_currentPlayer == Marks.X) ? Marks.O : Marks.X;
                }
                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }

            } while (!gameWon && !gameTied);

            _printGame.ShowField(_field);

            if (gameWon)
            {
                var winPlayer = (_currentPlayer == Marks.X) ? Marks.O : Marks.X;
                _printGame.ShowWinner(winPlayer);
            }
            else
            {
                Console.WriteLine("The game is a tie!");
            }
        }

        private bool CheckForWin()
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (_field.Area[row, 0] == _currentPlayer &&
                    _field.Area[row, 1] == _currentPlayer &&
                    _field.Area[row, 2] == _currentPlayer)
                {
                    return true; // Winning row
                }
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (_field.Area[0, col] == _currentPlayer &&
                    _field.Area[1, col] == _currentPlayer &&
                    _field.Area[2, col] == _currentPlayer)
                {
                    return true; // Winning column
                }
            }

            // Check diagonals
            if (_field.Area[0, 0] == _currentPlayer &&
                _field.Area[1, 1] == _currentPlayer &&
                _field.Area[2, 2] == _currentPlayer)
            {
                return true; // Winning diagonal (top-left to bottom-right)
            }

            if (_field.Area[0, 2] == _currentPlayer &&
                _field.Area[1, 1] == _currentPlayer &&
                _field.Area[2, 0] == _currentPlayer)
            {
                return true; // Winning diagonal (top-right to bottom-left)
            }

            return false;
        }


        private bool CheckForTie()
        {
            // Implement your tie-checking logic here
            // Return true if the game is a tie, otherwise return false
            // The game is a tie if all positions on the board are filled
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (_field.Area[row, col] == Marks.None)
                    {
                        return false; // At least one empty spot is found, game is not tied
                    }
                }
            }
            return true; // All spots are filled, game is tied
        }
    }
}
