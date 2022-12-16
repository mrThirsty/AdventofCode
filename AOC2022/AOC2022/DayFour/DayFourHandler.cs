using System.Reflection.Metadata;
using AOC.Library.Interfaces;
using AOC.Library.Tools;
using AOC2022.Model;

namespace AOC2022.DayFour;

public class DayFourHandler : IPuzzle
{
    public string Label { get; }
    public int OrderIndex { get; }
    public async Task ExecutePuzzle(CancellationToken ct)
    {
        ConsoleLogger.LogMessage("");
        ConsoleLogger.LogMessage("Lets resolve the puzzles for Day 4.");
        
        var data = File.ReadLines("DayFour/input.txt");

        List<CleaningPair> pairs = new();
        int index = 1;
        int partOne = 0, partTwo = 0;
        
        foreach (string line in data)
        {
            string[] assignments = line.Split(',');
            string[] elfOneSections = assignments[0].Split('-');
            string[] elfTwoSections = assignments[1].Split('-');
            CleaningElf elfOne = new(Convert.ToInt32(elfOneSections[0]), Convert.ToInt32(elfOneSections[1]));
            CleaningElf elfTwo = new(Convert.ToInt32(elfTwoSections[0]), Convert.ToInt32(elfTwoSections[1]));
            
            pairs.Add(new(index, elfOne, elfTwo));
            index++;
        }

        foreach (CleaningPair pair in pairs)
        {
            if ((pair.ElfOne.StartSection <= pair.ElfTwo.StartSection &&
                 pair.ElfOne.EndSection >= pair.ElfTwo.EndSection) ||
                (pair.ElfTwo.StartSection <= pair.ElfOne.StartSection &&
                 pair.ElfTwo.EndSection >= pair.ElfOne.EndSection))
            {
                partOne++;
            }
        }

        foreach (CleaningPair pair in pairs)
        {
            // if ((pair.ElfOne.StartSection <= pair.ElfTwo.StartSection &&
            //      pair.ElfOne.EndSection >= pair.ElfTwo.EndSection) ||
            //     (pair.ElfTwo.StartSection <= pair.ElfOne.StartSection &&
            //      pair.ElfTwo.EndSection >= pair.ElfOne.EndSection))
            // {
            //     partTwo++;
            // }

            if (InBetween(pair.ElfOne.StartSection, pair.ElfOne.EndSection, pair.ElfTwo.StartSection) ||
                InBetween(pair.ElfOne.StartSection, pair.ElfOne.EndSection, pair.ElfTwo.EndSection) ||
                InBetween(pair.ElfTwo.StartSection, pair.ElfTwo.EndSection, pair.ElfOne.StartSection) ||
                InBetween(pair.ElfTwo.StartSection, pair.ElfTwo.EndSection, pair.ElfOne.EndSection))
            {
                partTwo++;
            }
        }
        
        
        ConsoleLogger.LogPuzzleResult("Part 1 answers");
        ConsoleLogger.LogPuzzleResult($"how many assignment pairs does one range fully contain the other: {partOne}");
        ConsoleLogger.LogPuzzleResult("");
        ConsoleLogger.LogPuzzleResult("Part 2 Answers");
        ConsoleLogger.LogPuzzleResult($"how many assignment pairs do the ranges overlap: {partTwo}");
        ConsoleLogger.LogPuzzleResult("");
    }

    private bool InBetween(int start, int end, int point)
    {
        if (start <= point && end >= point) return true;

        return false;
    }
}