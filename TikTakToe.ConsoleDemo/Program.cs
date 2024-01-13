// See https://aka.ms/new-console-template for more information
using TikTakToe;

var consoleGamePrinter = new ConsoleGamePrinterWithArrowSelection();

string baseAddress = "https://localhost:7282/";

var apiClient = new ApiClient(baseAddress);

var game = new Game(consoleGamePrinter, apiClient);

await game.Run();
