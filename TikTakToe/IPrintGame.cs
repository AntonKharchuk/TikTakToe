
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
        void ShowAllFields(IList<Field> fields);
        string GetUserName();
        int GetUserFieldChoice(IList<Field> fields);
    }
}
