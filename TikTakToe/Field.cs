
namespace TikTakToe
{
    public enum Marks
    {
        None = 0,
        X = 1,
        O = 2,
    }
    public class Field
    {
        public Marks[,] Area{ get; private set; }

        public Field()
        {
            Area = new Marks[3,3];
        }

        public bool SetMark(int row, int coll, Marks mark)
        {
            if (ValidateMark(row, coll))
            {
                Area[row, coll] = mark;
                return true;
            }
            return false;   
        }

        private bool ValidateMark(int row, int coll)
        {
            return row >= 0 && coll >= 0 && row <= 2 && coll <= 2 && Area[row, coll] is Marks.None;
        }
    }
}
