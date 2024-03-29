﻿
using TikTakToe.Models;

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
                    Console.Write($" {GetMarkSymbolWithBorder(field.Positions[row, col])} ");
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
                Console.CursorTop = 1 + row * 2;
                Console.CursorLeft = 2 + col * 6;

                var key = GetUserKey();

                switch (key)
                {

                    case ConsoleKey.W:
                        if (CanMove(row - 1, col))
                        {
                            row--;
                        }

                        break;
                    case ConsoleKey.A:
                        if (CanMove(row, col - 1))
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
                        if (CanMove(row, col + 1))
                        {
                            col++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return new int[] { row, col };
                }
            }

        }

        private bool CanMove(int row, int coll)
        {
            return row >= 0 && coll >= 0 && row <= 2 && coll <= 2;
        }

        public ConsoleKey GetUserKey()
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

        public string GetUserName()
        {
            string userName = string.Empty;
            bool userNameIsSet = false;
            while (!userNameIsSet)
            {
                Console.Write("Type User Name: ");
                userName = Console.ReadLine();
                userName = userName.Trim();
                if (!string.IsNullOrEmpty(userName))
                {
                    userNameIsSet = true;
                    for (int i = 0; i < userName.Length; i++)
                    {
                        if (userName[i]==',')
                        {
                            userNameIsSet = false;
                            break;
                        }
                    }
                }
            }
            return userName;
        }

        public void ShowWaitingForPlayer()
        {
            Console.WriteLine("Waiting For Player...");
        }

        public void ShowGameResult(Field field)
        {
            Console.WriteLine(field.Status.Name);
            Console.ReadKey();
        }

        public void ShowWaitingForOponentMove()
        {
            Console.WriteLine("Waiting For Oponent");
        }

        public void ShowAllFieldsWithSelection(IList<Field> fields, int selectedIndex)
        {
            int tableBorderWidth = 65;
            Console.Clear();
            Console.WriteLine(new string('-', tableBorderWidth));
            Console.WriteLine("|\tRoom\t|\tStatus\t\t|\tPlayers\t\t|");

            for (int i = 0; i < fields.Count; i++)
            {
                if (i == selectedIndex)
                    ShowItemWithBorders(fields[i], '=');
                else
                    ShowItemWithBorders(fields[i], '-');
            }
            void ShowItemWithBorders(Field field, char border)
            {
                Console.WriteLine(new string(border, tableBorderWidth));
                Console.WriteLine($"|\t{field.Id}\t|\t{field.Status.Name}\t\t|\t{field.Players}");
                Console.WriteLine(new string(border, tableBorderWidth));
            }
        }
    }

}
