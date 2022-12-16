using System.Reflection.Metadata;
using AOC.Library.Interfaces;
using AOC.Library.Tools;
using AOC2022.Model;

namespace AOC2022.DayThree;

public class DayThreeHandler : IPuzzle
{
    public string Label { get; }
    public int OrderIndex { get; }
    public async Task ExecutePuzzle(CancellationToken ct)
    {
        ConsoleLogger.LogMessage("");
        ConsoleLogger.LogMessage("Lets resolve the puzzles for Day 3.");
        
        var data = File.ReadLines("dayThree/input.txt");
        List<Rucksack> rucksacks = new();
        int index = 0;
        
        foreach (string pack in data)
        {
            Dictionary<string, int> priorities = new();
            int splitItem = pack.Length / 2;

            string compartmentOne = pack.Substring(0, splitItem);
            string compartmentTwo = pack.Substring(splitItem);

            foreach (char item in compartmentOne)
            {
                if (compartmentTwo.Contains(item) && !priorities.ContainsKey(item.ToString()))
                {
                    priorities[item.ToString()] = 0;
                }
            }

            foreach (string key in priorities.Keys)
            {
                priorities[key] = GetPriority(char.Parse(key));
            }
            
            rucksacks.Add(new(index, pack, compartmentOne, compartmentTwo, priorities));

            index++;
        }

        int sumPriorities = 0;
        
        foreach (var pack in rucksacks)
        {
            foreach (var key in pack.CommonPriorities.Keys)
            {
                sumPriorities += pack.CommonPriorities[key];
            }
        }

        var sortedPacks = rucksacks.OrderBy(p => p.Index);

        int summedBadges = 0;
        
        for(int group = 0; group < sortedPacks.Count(); group += 3)
        {
            Rucksack first = sortedPacks.ElementAt(group);
            Rucksack second = sortedPacks.ElementAt(group + 1);
            Rucksack third = sortedPacks.ElementAt(group + 2);

            foreach (char item in first.AllItems)
            {
                if (second.AllItems.Contains(item) && third.AllItems.Contains(item))
                {
                    summedBadges += GetPriority(item);
                    break;
                }
            }
        }
        
        ConsoleLogger.LogPuzzleResult("Part 1 answers");
        ConsoleLogger.LogPuzzleResult($"The priority sum would be: {sumPriorities}");
        ConsoleLogger.LogPuzzleResult("");
        ConsoleLogger.LogPuzzleResult("Part 2 Answers");
        ConsoleLogger.LogPuzzleResult($"badge priorty sum is : {summedBadges}");
        ConsoleLogger.LogPuzzleResult("");
    }

    private int GetPriority(char item)
    {
        int aTest = (int)'a' % 32;
        int bTest = (int)'b' % 32;
        int eTest = (int)'e' % 32;
        
        bool upperCase = char.IsUpper(item);

        return upperCase ? ((int)item % 32) + 26 : ((int)item % 32);
    }
}