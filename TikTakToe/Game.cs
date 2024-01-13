using TikTakToe.Models;

namespace TikTakToe
{
    public class Game
    {
        private Field _field;
        private IApiClient _apiClient;
        private IPrintGame _printGame;
        private Marks _currentPlayer;
        private string _userName;

        public Game(IPrintGame printGame, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _printGame = printGame;
        }
        public async Task Run()
        {
            _userName = _printGame.GetUserName();
           
            while (true)
            {
                var canEnterGame = false;
                var fieldId = 0;
                do
                {
                    var allFields = await _apiClient.GetAllItemsAsync();
                    _printGame.ShowAllFields(allFields);
                    fieldId = _printGame.GetUserFieldChoice(allFields);
                    canEnterGame =  await WaitForPlayers(fieldId);
                } while (!canEnterGame);

                await RunGame(fieldId);
            }
        }
        private async Task RunGame(int fieldId)
        {
            var field = await _apiClient.GetItemByIdAsync(fieldId);

            while (field.StatusId == 1)
            {
                _printGame.ShowField(field);
                var currentTurn = (Marks)field.TurnId;
                _printGame.ShowPlayerTurn(currentTurn);

                if (currentTurn == _currentPlayer)
                {
                    int row = -1;
                    int col = -1;
                    do
                    {
                        int[] userMove = _printGame.GetUserMarkPlace();
                        row = userMove[0];
                        col = userMove[1];
                    }
                    while (!field.TrySetMark(row, col, _currentPlayer));
                    await _apiClient.UpdateItemAsync(field);
                }
                else
                {
                    _printGame.ShowWaitingForOponentMove();
                    await Task.Delay(3000);
                }
                field = await _apiClient.GetItemByIdAsync(fieldId);
            }
            _printGame.ShowField(field);
            _printGame.ShowGameResult(field);
        }
        private async Task<bool> WaitForPlayers(int fieldId)
        {
            var field = await _apiClient.GetItemByIdAsync(fieldId);
            if (field.Players == null)
            {
                _currentPlayer = Marks.X;
                await _apiClient.UpdatePlayersAsync(_userName, field.Id);
            }
            else if (field.Players.Split(',', StringSplitOptions.RemoveEmptyEntries).Length == 1)
            {
                _currentPlayer = Marks.O;
                await _apiClient.UpdatePlayersAsync($"{field.Players},{_userName}", field.Id);
                return true;
            }
            else if (field.Players.Split(',', StringSplitOptions.RemoveEmptyEntries).Length == 2)
            {
                return false;
            }
            int delayCount = 0;
            while (true)
            {
                field = await _apiClient.GetItemByIdAsync(fieldId);
                if (field.Players.Split(',', StringSplitOptions.RemoveEmptyEntries).Length == 2)
                {
                    return true;
                }
                else
                {
                    _printGame.ShowField(field);
                    _printGame.ShowWaitingForPlayer();
                    await Task.Delay(3000);
                    delayCount++;
                }
                if (delayCount==10)
                {
                    await _apiClient.UpdatePlayersAsync(null, field.Id);
                    return false;
                }
            }
        }
    }
}
