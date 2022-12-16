using AOC.Library.Interfaces;
using AOC.Library.Tools;
using AOC2022.Model;

namespace AOC2022.DayOne;

public class DayOneHandler : IPuzzle
{
    public DayOneHandler()
    {
        
    }
    
    public string Label { get; }
    public int OrderIndex { get; }
    
    public async Task ExecutePuzzle(CancellationToken ct)
    {
        ConsoleLogger.LogMessage("");
        ConsoleLogger.LogMessage("Lets resolve the puzzles for Day 1.");
        
        var data = File.ReadLines("dayone/input.txt");

        List<Elf> elvesFood = new();

        int index = 0;
        int calories = 0;

        foreach (string line in data)
        {
            ConsoleLogger.LogMessage(line);

            if (string.IsNullOrWhiteSpace(line))
            {
                elvesFood.Add(new(index, calories));
        
                index++;
                calories = 0;
            }
            else
            {
                calories += Convert.ToInt32(line);
            }
        }

        var sortedElves = elvesFood.OrderByDescending(e => e.Calories);
        
        ConsoleLogger.LogPuzzleResult($"Elf with the most Calories is: {sortedElves.First().Index}");
        ConsoleLogger.LogPuzzleResult($"They have: {sortedElves.First().Calories}");
        ConsoleLogger.LogPuzzleResult($"The top three elves carry: {sortedElves.Take(3).Sum(e => e.Calories)}");
    }
}