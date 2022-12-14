// See https://aka.ms/new-console-template for more information

using DayTwo;

var rounds = File.ReadLines("input.txt");
List<Round> roundDataPt1 = new();
List<Round> roundDataPt2 = new();
int index = 1;

foreach (string line in rounds)
{
    Console.WriteLine($"Round {index}:{line}");
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
    
    roundDataPt2.Add(new (index, opponent, me, result, points));

    index++;
}

int scorePt1 = roundDataPt1.Sum(r => r.Score);
int scorePt2 = roundDataPt2.Sum(r => r.Score);

Console.WriteLine("Part 1 answers");
Console.WriteLine($"Your score would be: {scorePt1}");
Console.WriteLine("");
Console.WriteLine("Part 2 Answers");
Console.WriteLine($"Your score would be: {scorePt2}");
Console.WriteLine("");
Console.WriteLine("Pres any key to exit");
Console.ReadKey();

int ResultPoints(RoundResult result)
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

int ShapePoints(Shape shapeUsed)
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

Shape GetShape(char move)
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

RoundResult GetResult(Shape opponent, Shape me)
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

RoundResult GetNeededResult(char neededResult)
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