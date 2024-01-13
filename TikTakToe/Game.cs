using TikTakToe.Models;

namespace TikTakToe
{
    public class Game
    {
        private Field _field;
        private IApiClient _apiClient;
        private IPrintGame _printGame;
        private Marks _currentPlayer;

        public Game(IPrintGame printGame, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _printGame = printGame;
            _currentPlayer = Marks.X; // Start with player X
        }

        public async void Run()
        {
            string userName = _printGame.GetUserName();
            while (true)
            {
                var allFields = await _apiClient.GetAllItemsAsync();

                _printGame.ShowAllFields(allFields);
                var fieldId = _printGame.GetUserFieldChoice(allFields);

                var currentField = allFields.Where(f => f.Id == fieldId).First();


            }
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
    }
}
