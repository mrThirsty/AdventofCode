using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using AOC.Library.Interfaces;
using AOC.Library.Tools;
using AOC2022.Model;
using System.Linq;
using System.Linq.Expressions;

namespace AOC2022.DaySix;

public class DaySixHandler : IPuzzle
{
    public string Label { get; }
    public int OrderIndex { get; }

    //private readonly string _boxLineMatch = @"((\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}){1}";
    private readonly string _boxLineMatch = @"((\[\D\]\s{1}|\s{4}){1}){1}";

    private readonly string _stackNumberMatch =
        @"(\s{1}\d{1}\s{3}\d{1}\s{3}\d\s{1}\d{1}\s{3}\d{1}\s{3}\d\s{1}\d{1}\s{3}\d{1}\s{3}\d){1}";

    private readonly string _commandMatch = @"(move\s{1}\d{1,}\s{1}from\s{1}\d\s{1}to\s{1}\d){1}";

    public async Task ExecutePuzzle(CancellationToken ct)
    {
        ConsoleLogger.LogMessage("");
        ConsoleLogger.LogMessage("Lets resolve the puzzles for Day 6.");

        var data = File.ReadLines("DaySix/input.txt");

        int index = 1;
        int partOne = 0;
        int partTwo = 0;

        foreach (string line in data)
        {
            string markers = "";
            string msg = "";

            for (int marker = 0; marker < line.Length; marker++)
            {
                markers += line[marker];
                msg += line[marker];

                if (markers.Length > 4)
                {
                    markers = markers.Remove(0, 1);
                }

                if (msg.Length > 14)
                {
                    msg = msg.Remove(0, 1);
                }

                if (markers.Length == 4)
                {
                    int mCount = markers.Distinct().Count();
                    
                    if (mCount == 4 && partOne == 0)
                    {
                        partOne = index;
                    }
                }

                if (msg.Length == 14)
                {
                    var dCount = msg.Distinct().Count();

                    if (dCount == 14 && partTwo == 0)
                    {
                        partTwo = index;
                    }
                }

                index++;
            }
        }

        ConsoleLogger.LogPuzzleResult($"Part 1 answer: {partOne}");
        ConsoleLogger.LogPuzzleResult("");
        ConsoleLogger.LogPuzzleResult($"Part 2 Answer: {partTwo}");
        ConsoleLogger.LogPuzzleResult("");
    }
}