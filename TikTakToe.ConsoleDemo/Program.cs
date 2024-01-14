// See https://aka.ms/new-console-template for more information
using TikTakToe;

var consoleGamePrinter = new ConsoleGamePrinterWithArrowSelection();

string baseAddress = "https://188c-31-40-110-212.ngrok-free.app/";

var apiClient = new ApiClient(baseAddress);

var game = new Game(consoleGamePrinter, apiClient);

await game.Run();
