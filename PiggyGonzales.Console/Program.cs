// See https://aka.ms/new-console-template for more information
using PiggyGonzales.Console.Application;

Console.WriteLine("Hello, World!");

GameFactory newGame = new GameFactory(5, 10);

newGame.Play();
