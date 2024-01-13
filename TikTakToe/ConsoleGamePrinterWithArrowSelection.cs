
namespace TikTakToe
{
    public class ConsoleGamePrinterWithArrowSelection : IPrintGame
    {
        public void ShowField(Field field)
        {
            Console.Clear();
            Console.WriteLine("Current Game Board:");
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write($" {GetMarkSymbolWithBorder(field.Area[row, col])} ");
                    if (col < 2)
                    {
                        Console.Write("|"); // Vertical separator between columns
                    }
                }
                Console.WriteLine();
                if (row < 2)
                {
                    Console.WriteLine("-----------------"); // Horizontal separator between rows
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
            var row = 0;
            var col = 0;

            while (true) 
            {
                Console.CursorTop = 1 + row*2; 
                Console.CursorLeft = 2 + col*6;

                var key = GetUserKey();

                switch (key) {
                    
                    case ConsoleKey.W:
                        if (CanMove(row - 1, col))
                        {
                            row--;
                        }
                        
                        break;
                    case ConsoleKey.A:
                        if (CanMove(row, col-1))
                        {
                            col--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (CanMove(row + 1, col))
                        {
                            row++;
                        }
                        break;
                    case ConsoleKey.D:
                        if (CanMove(row, col+1))
                        {
                            col++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return new int[] {row, col};    
                }
            }

        }

        private bool CanMove(int row, int coll)
        {
            return row >= 0 && coll >= 0 && row <= 2 && coll <= 2;
        }

        private  ConsoleKey GetUserKey()
        {
            var key = Console.ReadKey(true);
            return key.Key;
        }



        public void ShowPlayerTurn(Marks mark)
        {
            Console.WriteLine($"Player {GetMarkSymbol(mark)}'s turn.");
        }
        public void ShowWinner(Marks mark)
        {
            Console.WriteLine($"Player {GetMarkSymbol(mark)} wins!");
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
    }

}
