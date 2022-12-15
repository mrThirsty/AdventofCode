using AOC2022.Library.Interfaces;
using AOC2022.Enums;
using AOC2022.Library.Tools;
using AOC2022.Model;

namespace AOC2022.DayTwo;

public class DayTwoHandler : IPuzzle
{
    public string Label { get; }
    public int OrderIndex { get; }

    public async Task ExecutePuzzle(CancellationToken ct)
    {
        ConsoleLogger.LogMessage("");
        ConsoleLogger.LogMessage("Lets resolve the puzzles for Day 2.");

        var rounds = File.ReadLines("DayTwo/input.txt");
        List<Round> roundDataPt1 = new();
        List<Round> roundDataPt2 = new();
        int index = 1;

        foreach (string line in rounds)
        {
            ConsoleLogger.LogMessage($"Round {index}:{line}");
            Shape opponent = GetShape(line[0]);
            Shape me = GetShape(line[2]);

            RoundResult result = GetResult(opponent, me);
            int points = ResultPoints(result);
            points += ShapePoints(me);

            roundDataPt1.Add(new(index, opponent, me, result, points));

            result = GetNeededResult(line[2]);

            if (result == RoundResult.Draw) me = opponent;
            else
            {
                //Rock beats scissors, scissors beats paper, paper beats rock
                if (opponent == Shape.Rock)
                {
                    if (result == RoundResult.Win)
                    {
                        me = Shape.Paper;
                    }
                    else if (result == RoundResult.Loss)
                    {
                        me = Shape.Scissors;
                    }
                }
                else if (opponent == Shape.Paper)
                {
                    if (result == RoundResult.Win)
                    {
                        me = Shape.Scissors;
                    }
                    else if (result == RoundResult.Loss)
                    {
                        me = Shape.Rock;
                    }
                }
                else if (opponent == Shape.Scissors)
                {
                    if (result == RoundResult.Win)
                    {
                        me = Shape.Rock;
                    }
                    else if (result == RoundResult.Loss)
                    {
                        me = Shape.Paper;
                    }
                }
            }

            points = ResultPoints(result);
            points += ShapePoints(me);

            roundDataPt2.Add(new(index, opponent, me, result, points));

            index++;
        }

        int scorePt1 = roundDataPt1.Sum(r => r.Score);
        int scorePt2 = roundDataPt2.Sum(r => r.Score);

        ConsoleLogger.LogPuzzleResult("Part 1 answers");
        ConsoleLogger.LogPuzzleResult($"Your score would be: {scorePt1}");
        ConsoleLogger.LogPuzzleResult("");
        ConsoleLogger.LogPuzzleResult("Part 2 Answers");
        ConsoleLogger.LogPuzzleResult($"Your score would be: {scorePt2}");
        ConsoleLogger.LogPuzzleResult("");
    }

    private int ResultPoints(RoundResult result)
    {
        switch (result)
        {
            case RoundResult.Draw:
                return 3;
            case RoundResult.Win:
                return 6;
        }

        return 0;
    }

    private int ShapePoints(Shape shapeUsed)
    {
        switch (shapeUsed)
        {
            case Shape.Rock:
                return 1;
            case Shape.Paper:
                return 2;
            case Shape.Scissors:
                return 3;
        }

        return 0;
    }

    private Shape GetShape(char move)
    {
        Shape shape = Shape.None;

        switch (move)
        {
            case 'A':
            case 'X':
                shape = Shape.Rock;
                break;
            case 'B':
            case 'Y':
                shape = Shape.Paper;
                break;
            case 'C':
            case 'Z':
                shape = Shape.Scissors;
                break;
        }

        return shape;
    }

    private RoundResult GetResult(Shape opponent, Shape me)
    {
        //Rock beats scissors, scissors beats paper, paper beats rock

        if (opponent == me)
            return RoundResult.Draw;

        if ((me == Shape.Rock && opponent == Shape.Scissors) ||
            (me == Shape.Scissors && opponent == Shape.Paper) ||
            (me == Shape.Paper && opponent == Shape.Rock))
            return RoundResult.Win;

        return RoundResult.Loss;
    }

    private RoundResult GetNeededResult(char neededResult)
    {
        // x = lose, y = draw, z = win

        switch (neededResult)
        {
            case 'Y':
                return RoundResult.Draw;
            case 'Z':
                return RoundResult.Win;
        }

        return RoundResult.Loss;
    }
}