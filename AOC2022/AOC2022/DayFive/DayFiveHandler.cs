using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using AOC.Library.Interfaces;
using AOC.Library.Tools;
using AOC2022.Model;
using System.Linq;
using System.Linq.Expressions;

namespace AOC2022.DayFive;

public class DayFiveHandler : IPuzzle
{
    public string Label { get; }
    public int OrderIndex { get; }

    //private readonly string _boxLineMatch = @"((\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}(\[\D\]\s{1}|\s{4}){1}){1}";
    private readonly string _boxLineMatch = @"((\[\D\]\s{1}|\s{4}){1}){1}";
    private readonly string _stackNumberMatch = @"(\s{1}\d{1}\s{3}\d{1}\s{3}\d\s{1}\d{1}\s{3}\d{1}\s{3}\d\s{1}\d{1}\s{3}\d{1}\s{3}\d){1}";
    private readonly string _commandMatch = @"(move\s{1}\d{1,}\s{1}from\s{1}\d\s{1}to\s{1}\d){1}";
    public async Task ExecutePuzzle(CancellationToken ct)
    {
        ConsoleLogger.LogMessage("");
        ConsoleLogger.LogMessage("Lets resolve the puzzles for Day 5.");
        
        var data = File.ReadLines("DayFive/input.txt");

        List<CleaningPair> pairs = new();
        int index = 1;
        string partOne = ""; 
        int partTwo = 0;

        int boxlines = 0, stacklines = 0, commandLines = 0;
        
        Dictionary<int, Stack<Char>> stacks = new();
        stacks[0] = new();
        stacks[1] = new();
        stacks[2] = new();
        stacks[3] = new();
        stacks[4] = new();
        stacks[5] = new();
        stacks[6] = new();
        stacks[7] = new();
        stacks[8] = new();
        
        Queue<StackMove> commands = new();

        foreach (string line in data)
        {
            //each box is 3 chars with a space between 1/2 and 2/3. total of 11 char
            //stack numbers: space <number> 3 spaces <number> 3 spaces <number> space 
            //BLANK LINE
            //commands: move <number> from <number> to <number>
            if (Regex.IsMatch(line, _boxLineMatch))
            {
                string itemOne = line.Substring(0, 3).Trim();
                string itemTwo = line.Substring(4, 3).Trim();
                string itemThree = line.Substring(8, 3).Trim();
                string itemFour = line.Substring(12, 3).Trim();
                string itemFive = line.Substring(16, 3).Trim();
                string itemSix = line.Substring(20, 3).Trim();
                string itemSeven = line.Substring(24, 3).Trim();
                string itemEight = line.Substring(28, 3).Trim();
                string itemNine = line.Substring(32, 3).Trim();
                
                if(!string.IsNullOrWhiteSpace(itemOne))
                    stacks[0].Push(itemOne[1]);
                if(!string.IsNullOrWhiteSpace(itemTwo))
                    stacks[1].Push(itemTwo[1]);
                if(!string.IsNullOrWhiteSpace(itemThree))
                    stacks[2].Push(itemThree[1]);
                if(!string.IsNullOrWhiteSpace(itemFour))
                    stacks[3].Push(itemFour[1]);
                if(!string.IsNullOrWhiteSpace(itemFive))
                    stacks[4].Push(itemFive[1]);
                if(!string.IsNullOrWhiteSpace(itemSix))
                    stacks[5].Push(itemSix[1]);
                if(!string.IsNullOrWhiteSpace(itemSeven))
                    stacks[6].Push(itemSeven[1]);
                if(!string.IsNullOrWhiteSpace(itemEight))
                    stacks[7].Push(itemEight[1]);
                if(!string.IsNullOrWhiteSpace(itemNine))
                    stacks[8].Push(itemNine[1]);
                
                boxlines++;
            }
            else if (Regex.IsMatch(line, _stackNumberMatch))
            {
                stacklines++;
            }
            else if (Regex.IsMatch(line, _commandMatch))
            {
                string[] parts = line.Split(' ');

                int quantity = Convert.ToInt32(parts[1]);
                int source = Convert.ToInt32(parts[3]);
                int target = Convert.ToInt32(parts[5]);

                commands.Enqueue(new(quantity, source, target));
                
                commandLines++;
            }
            
        }

        ReverseStacks(stacks);

        int moveCount = 1;
        while (commands.Count > 0)
        {
            /*
             * For part one, run as is. For Part 2 comment out the first for loop and uncomment the
             * second for loop and the following while loop.
             * 
             */
            
            StackMove command = commands.Dequeue();
            
            // PART 1
            for (int move = 0; move < command.Quantity; move++)
            {
                char box = stacks[command.Source - 1].Pop();
                stacks[command.Target -1].Push(box);
            }
            
            // PART 2
            // Stack<char> temp = new();
            //
            // for (int move = 0; move < command.Quantity; move++)
            // {
            //     char box = stacks[command.Source - 1].Pop();
            //     temp.Push(box);
            // }
            //
            // while (temp.Count > 0)
            // {
            //     stacks[command.Target -1].Push(temp.Pop());
            // }
                

            moveCount++;
        }

        List<char> topItems = GetTopItems(stacks).ToList();

        topItems.ForEach(c => partOne += c);

        ConsoleLogger.LogPuzzleResult($"Part 1 answer: {partOne}");
        ConsoleLogger.LogPuzzleResult("");
        ConsoleLogger.LogPuzzleResult($"Part 2 Answer: {partTwo}");
        ConsoleLogger.LogPuzzleResult("");
    }

    private bool InBetween(int start, int end, int point)
    {
        if (start <= point && end >= point) return true;

        return false;
    }

    private void ReverseStacks(Dictionary<int, Stack<char>> stacks)
    {
        foreach (int key in stacks.Keys)
        {
            stacks[key] = Reverse(stacks[key]);
        }
    }

    private IEnumerable<char> GetTopItems(Dictionary<int, Stack<char>> stacks)
    {
        foreach (int key in stacks.Keys)
        {
            yield return stacks[key].Pop();
        }
    }
    
    private Stack<char> Reverse(Stack<char> stack)
    {
        Stack<char> temp = new();
        
        while (stack.Count > 0)
        {
            temp.Push(stack.Pop());
        }
        
        return temp;
    } 
}