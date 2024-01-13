
using System.Runtime.InteropServices;

namespace TikTakToe.Models
{
    public enum Marks
    {
        None = 0,
        X = 1,
        O = 2,
    }
    public class Field:BaseEntity
    {
        public Marks[,] Positions { get; set; }//0 none, 1 X, 2 O
        /*
        0,1,2
        3,4,5
        6,7,8
         */
        public string? Players { get; set; }

        public int StatusId { get; set; }
        public Status? Status { get; set; }

        public int TurnId { get; set; }
        public Turn? Turn { get; set; }

        public bool TrySetMark(int row, int col, Marks currentPlayer)
        {
            if( row >= 0 && col >= 0 && col <= 2 && row <= 2 && Positions[row,col] is Marks.None)
            {
                Positions[row,col] = currentPlayer;
                return true;
            }
            return false;
        }
    }
}
