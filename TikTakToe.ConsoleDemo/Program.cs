// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TikTakToe;
using TikTakToe.ConsoleDemo;

//var consoleGamePrinter = new ConsoleGamePrinterWithArrowSelection();

//string baseAddress = "https://188c-31-40-110-212.ngrok-free.app/";

//var apiClient = new ApiClient(baseAddress);

//var game = new Game(consoleGamePrinter, apiClient);

//await game.Run();



IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

// Set up Dependency Injection
var serviceProvider = new ServiceCollection()
    .ConfigureServices(configuration)
    .BuildServiceProvider();

// Run the application
var game = serviceProvider.GetRequiredService<Game>();
game.Run().GetAwaiter().GetResult();