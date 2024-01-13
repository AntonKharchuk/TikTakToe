
using System.Runtime.InteropServices;

namespace TikTakToe.Models
{
    public class Field:BaseEntity
    {
        public string? Positions { get; set; }//0 none, 1 X, 2 O
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

        public bool CanSetMark(int position)
        {
            var strPositions = Positions.Split(',');
            return position >= 0 && position <= 9 && int.Parse(strPositions[position]) is (int) Marks.None;
        }
    }
}
