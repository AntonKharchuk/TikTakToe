
using TikTakToe.Models;

namespace TikTakToe
{
    public interface IPrintGame
    {
        void ShowField(Field field);
        int[] GetUserMarkPlace();
        void ShowPlayerTurn(Marks mark);
        void ShowGameResult(Field field);   
        void ShowWaitingForPlayer();
        void ShowWaitingForOponentMove();
        string GetUserName();
        void ShowAllFieldsWithSelection(IList<Field> fields, int selectedId);
        ConsoleKey GetUserKey();
    }
}
