using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using app;
namespace Days;
public class Day11 : ISolve
{

    string MonkeyPointerRegex = @"^Monkey\s(\d):$";
    string MonkeySlotItemsRegex = @"^\s\sStarting\sitems:\s(?:([\d]{1,3})[,|\s]*)+$";
    string MonkeyOperationRegex = @"^\s\sOperation:\snew\s=\s([\d\S]*)\s([*+-/])\s([\d\S]*)$";
    string MonkeyTestRegex = @"^\s{2}Test:\sdivisible\sby\s([\d]{1,2})$";
    string MonkeyStateRegex = @"^\s{4}If\s([\S]*):[\sA-Za-z]+(\d){1,2}$";


    List<Monkey> PlayingMonkeys = new List<Monkey>();

    public string SolvePartOne(string[] input)
    {
        foreach (var lane in input)
        {
            ReadLane(lane);
        }
        for (int i = 0; i < 20; i++)
        {
            PlayOneRound(true);
        }
        var inspectCounter = PlayingMonkeys.Select(x => x.InspectedItems).ToList();
        inspectCounter.Sort();
        return (inspectCounter[^1] * inspectCounter[^2]).ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        var inspectCounter = new List<long>();
        PlayingMonkeys.Clear();
        foreach (var lane in input)
        {
            ReadLane(lane);
        }
        var lcm = new HashSet<int>(PlayingMonkeys.Select(x => x.TestValue).ToList()).Aggregate((a, b) => a * b);

        for (int i = 0; i < 10_000; i++)
        {
            PlayOneRound(false, lcm);
            inspectCounter = PlayingMonkeys.Select(x => x.InspectedItems).ToList();
            inspectCounter.Sort();
        }

        long c = inspectCounter[^1] * inspectCounter[^2];
        return (inspectCounter[^1] * inspectCounter[^2]).ToString();
    }

    public void ReadLane(string line)

    {
        switch (line)
        {
            case var _ when new Regex(MonkeyPointerRegex).IsMatch(line):
                var monkey = new Monkey();
                this.PlayingMonkeys.Add(monkey);
                break;
            case var _ when new Regex(MonkeySlotItemsRegex).IsMatch(line):
                var matchSlot = new Regex(@"((\d{1,3}))").Matches(line);
                foreach (Match item in matchSlot)
                {
                    PlayingMonkeys[^1].Items.Enqueue(long.Parse(item.Value));
                }
                break;
            case var _ when new Regex(MonkeyOperationRegex).IsMatch(line):
                var matchOperation = new Regex(MonkeyOperationRegex).Match(line);
                var operation = matchOperation.Groups[2];
                var operationValue = matchOperation.Groups[3];
                PlayingMonkeys[^1].OperationValue = operationValue.Value;
                PlayingMonkeys[^1].Operation = operation.Value;
                break;
            case var _ when new Regex(MonkeyTestRegex).IsMatch(line):
                Match matchTestValue = new Regex(@"(\d{1,3})").Match(line);
                PlayingMonkeys[^1].TestValue = int.Parse(matchTestValue.Value);
                break;
            case var _ when new Regex(MonkeyStateRegex).IsMatch(line):
                Match monkeyNumber = new Regex(@"(\d{1,3})").Match(line);
                if (new Regex("(true)").Match(line).Success)
                {
                    PlayingMonkeys[^1].TrueState = int.Parse(monkeyNumber.Value);
                }
                else
                {
                    PlayingMonkeys[^1].FalseState = int.Parse(monkeyNumber.Value);
                }
                break;
            default:
                break;
        }
    }
    public void PlayOneRound(bool uCanStayCalm, int lcm = 3)
    {
        foreach (var monkey in PlayingMonkeys)
        {
            while (monkey.Items.Count != 0)
            {
                var itemGameResult = monkey.InspectItem(uCanStayCalm, lcm);
                PlayingMonkeys[itemGameResult.Item1].Items.Enqueue(itemGameResult.Item2);
            }

        }
    }
}

public class Monkey
{
    public Queue<long> Items = new Queue<long>();
    public int TestValue;
    public int TrueState;
    public int FalseState;

    public string Operation = string.Empty;
    public string? OperationValue;

    public long InspectedItems;
    public (int, long) InspectItem(bool uCanStayCalm, int lcm)
    {
        InspectedItems++;
        // git first item in list
        long worryLevel = Items.Dequeue();
        // item doe operation
        worryLevel = DoOperation(worryLevel);
        // get bored and divide item by 3 and then round down to nearest intg
        if (uCanStayCalm)
        {
            worryLevel = worryLevel / lcm;
            return worryLevel % TestValue == 0 ? (TrueState, worryLevel) : (FalseState, worryLevel);
        }
        // do test
        // return value and pasing monkey invetory.
        worryLevel %= lcm;
        return worryLevel % TestValue == 0 ? (TrueState, worryLevel) : (FalseState, worryLevel);
    }

    private long DoOperation(long incomingValue)
    {
        // return value = oldvalue Operation OperationValue;
        return Operation switch
        {
            "+" => incomingValue + GetOperationValue(incomingValue),
            "-" => incomingValue - GetOperationValue(incomingValue),
            "/" => incomingValue / GetOperationValue(incomingValue),
            "*" => incomingValue * GetOperationValue(incomingValue),
            _ => throw new Exception("Oh Noo")
        };
    }

    private long GetOperationValue(long incomingValue)
    {
        if (long.TryParse(OperationValue, out var value))
        {
            return value;
        }
        else
        {
            return incomingValue;
        }
    }
}