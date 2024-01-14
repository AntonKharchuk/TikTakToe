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
                    fieldId = await SlectField();
                    canEnterGame = await WaitForPlayers(fieldId);
                } while (!canEnterGame);

                await RunGame(fieldId);
            }
        }
        private async Task<int> SlectField()
        {
            int selectedIndex = 0;

            IList<Field> fields = new List<Field>();
            await UpdateView();
            while (true)
            {
                var key = _printGame.GetUserKey();

                switch (key)
                {
                    case ConsoleKey.W:
                        if (CanMove(selectedIndex - 1))
                        {
                            selectedIndex--;
                            await UpdateView();
                        }
                        break;
                    case ConsoleKey.S:
                        if (CanMove(selectedIndex + 1))
                        {
                            selectedIndex++;
                            await UpdateView();
                        }
                        break;
                    case ConsoleKey.C:
                        await _apiClient.CreateItemAsync();
                        await UpdateView();
                        break;
                    case ConsoleKey.Enter:
                        return fields[selectedIndex].Id;
                }
            }
            async Task UpdateView()
            {
                fields = await _apiClient.GetAllItemsAsync();
                fields = fields.Where(f => {
                    if (f.Players is null)
                        return true;
                    var players = f.Players.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < players.Length; i++)
                    {
                        if (players[i] == _userName)
                            return true;
                    }
                    return players.Length < 2;
                }).ToList();
                _printGame.ShowAllFieldsWithSelection(fields, selectedIndex);
            }
            bool CanMove(int index)
            {
                return index >= 0 && index < fields.Count;
            }
        }
        private async Task RunGame(int fieldId)
        {
            var field = await _apiClient.GetItemByIdAsync(fieldId);

            while (field.StatusId == 1|| field.StatusId == 5)
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
            else
            {
                var players = field.Players.Split(',', StringSplitOptions.RemoveEmptyEntries);

                if (players.Length == 2)
                {
                    if (players[0] == _userName)
                    {
                        _currentPlayer = Marks.X;
                        return true;
                    }
                    else if (players[1] == _userName)
                    {
                        _currentPlayer = Marks.O;
                        return true;
                    }
                    return false;
                }

                if (players.Length == 1)
                {
                    if (players[0] == _userName)
                    {
                        _currentPlayer = Marks.X;
                    }
                    else
                    {
                        _currentPlayer = Marks.O;
                        await _apiClient.UpdatePlayersAsync($"{field.Players},{_userName}", field.Id);
                        return true;
                    }
                }
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
                    await _apiClient.UpdatePlayersAsync("", field.Id);
                    return false;
                }
            }
        }
    }
}
