// See https://aka.ms/new-console-template for more information
using TikTakToe;

Console.WriteLine("Hello, World!");

var consoleGamePrinter = new ConsoleGamePrinterWithArrowSelection();

var game = new Game(consoleGamePrinter);

game.Run();
