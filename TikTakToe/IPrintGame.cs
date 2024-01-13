
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

    public class ConsoleGamePrinter : IPrintGame
    {
        public void ShowField(Field field)
        {
            Console.Clear();
            Console.WriteLine("Current Game Board:");
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write($" {GetMarkSymbolWithBorder(field.Positions[row, col])} ");
                    if (col < 2)
                    {
                        Console.Write("|"); // Vertical separator between columns
                    }
                }
                Console.WriteLine();
                if (row < 2)
                {
                    Console.WriteLine("------------------"); // Horizontal separator between rows
                }
            }
            Console.WriteLine();
        }

        private string GetMarkSymbolWithBorder(Marks mark)
        {
            string symbol = GetMarkSymbol(mark);
            return $" {symbol} ";
        }

        public int[] GetUserMarkPlace()
        {
            Console.WriteLine("Enter row (0-2) and column (0-2) for your mark (comma-separated): ");
            string[] input = Console.ReadLine()?.Split(',');
            if (input != null && input.Length == 2 &&
                int.TryParse(input[0], out int row) && int.TryParse(input[1], out int col))
            {
                return new int[] { row, col };
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
                return GetUserMarkPlace();
            }
        }

        public void ShowPlayerTurn(Marks mark)
        {
            Console.WriteLine($"Player {GetMarkSymbol(mark)}'s turn.");
        }

        

        private string GetMarkSymbol(Marks mark)
        {
            switch (mark)
            {
                case Marks.X:
                    return "X";
                case Marks.O:
                    return "O";
                default:
                    return " ";
            }
        }

        public void ShowWinner(Marks mark)
        {
            Console.WriteLine($"Player {GetMarkSymbol(mark)} wins!");
        }

        public void ShowAllFields(IList<Field> fields)
        {
            throw new NotImplementedException();
        }

        public string GetUserName()
        {
            throw new NotImplementedException();
        }

        public int GetUserFieldChoice(IList<Field> fields)
        {
            throw new NotImplementedException();
        }

        public void ShowWaitingForPlayer()
        {
            throw new NotImplementedException();
        }

        public void ShowGameResult(Field field)
        {
            throw new NotImplementedException();
        }

        public void ShowWaitingForOponentMove()
        {
            throw new NotImplementedException();
        }
    }

}
