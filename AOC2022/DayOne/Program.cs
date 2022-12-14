// See https://aka.ms/new-console-template for more information
// Puzzle one: Find the elf carrying the most calories

using DayOne;

CancellationToken token = new();

var data = File.ReadLines("input.txt");

List<Elf> elvesFood = new();

int index = 0;
int calories = 0;

foreach (string line in data)
{
    Console.WriteLine(line);

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

Console.WriteLine();
Console.WriteLine($"Elf with the most Calories is: {sortedElves.First().Index}");
Console.WriteLine($"They have: {sortedElves.First().Calories}");
Console.WriteLine($"The top three elves carry: {sortedElves.Take(3).Sum(e => e.Calories)}");
Console.WriteLine("Press any key to exit");
Console.ReadKey();