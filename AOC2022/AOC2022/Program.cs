﻿using AOC2022.DayOne;
using AOC2022.DayTwo;
using AOC2022.Library.Interfaces;
using AOC2022.Library.Tools;

try
{
    CancellationToken token = new();
    var tokenSource =CancellationTokenSource.CreateLinkedTokenSource(token);
    
    bool run = true;

    while (run)
    {
        string choice = Menu();

        switch (choice.ToLower()) 
        {
            case "a":
                IPuzzle dayOnePuzzle = new DayOneHandler();
                await dayOnePuzzle.ExecutePuzzle(token);
                break;
            case "b":
                IPuzzle dayTwoPuzzle = new DayTwoHandler();
                await dayTwoPuzzle.ExecutePuzzle(token);
                break;
            case "c":
                break;
            case "0":
                run = false;
                tokenSource.Cancel();
                break;
        }
    }
}
catch (Exception ex)
{
    ConsoleLogger.LogError("Something went wrong. exiting...");
}
finally
{
}

string Menu()
{
    ConsoleLogger.LogMessage("");
    ConsoleLogger.LogMessage("");
    ConsoleLogger.LogMessage("Please select the puzzle you want to run:");
    ConsoleLogger.LogMessage("a. Day 1");
    ConsoleLogger.LogMessage("b. Day 2");
    ConsoleLogger.LogMessage("c. Day 3");
    ConsoleLogger.LogMessage("d. Day 4");
    ConsoleLogger.LogMessage("e. Day 5");
    ConsoleLogger.LogMessage("f. Day 6");
    ConsoleLogger.LogMessage("g. Day 7");
    ConsoleLogger.LogMessage("h. Day 8");
    ConsoleLogger.LogMessage("i. Day 9");
    ConsoleLogger.LogMessage("j. Day 10");
    ConsoleLogger.LogMessage("k. Day 11");
    ConsoleLogger.LogMessage("l. Day 12");
    ConsoleLogger.LogMessage("m. Day 13");
    ConsoleLogger.LogMessage("n. Day 14");
    ConsoleLogger.LogMessage("o. Day 15");
    ConsoleLogger.LogMessage("p. Day 16");
    ConsoleLogger.LogMessage("q. Day 17");
    ConsoleLogger.LogMessage("r. Day 18");
    ConsoleLogger.LogMessage("s. Day 19");
    ConsoleLogger.LogMessage("t. Day 20");
    ConsoleLogger.LogMessage("u. Day 21");
    ConsoleLogger.LogMessage("v. Day 22");
    ConsoleLogger.LogMessage("x. Day 23");
    ConsoleLogger.LogMessage("y. Day 24");
    ConsoleLogger.LogMessage("z. Day 25");
    ConsoleLogger.LogMessage("0: Exit");
    ConsoleLogger.AppendMessage("What do you what to do? ");
    var choice = Console.ReadKey();

    return choice.KeyChar.ToString();
}