
namespace TikTakToe.Models
{
    public class FieldDto:BaseEntity
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
    }
}
