using app;
namespace Days;

using System.Text;
using System.Text.RegularExpressions;

public class DayFive : ISolve
{

    //     [C]             [L]         [T]
    //     [V] [R] [M]     [T]         [B]
    //     [F] [G] [H] [Q] [Q]         [H]
    //     [W] [L] [P] [V] [M] [V]     [F]
    //     [P] [C] [W] [S] [Z] [B] [S] [P]
    // [G] [R] [M] [B] [F] [J] [S] [Z] [D]
    // [J] [L] [P] [F] [C] [H] [F] [J] [C]
    // [Z] [Q] [F] [L] [G] [W] [H] [F] [M]
    //  1   2   3   4   5   6   7   8   9 
    // Starting Stack
    List<Stack<string>> ShipCargo = new List<Stack<string>>();
    Regex reg = new Regex(@"(?:(?:\[|\s){1}([A-Z|\s]){1}(?:\]|\s){1}(?:\s|\n)?)");

    public string SolvePartOne(string[] input)
    {
        FillBaseStacks(input);
        Move(input);
        return readTopStack();
    }

    public string SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }

    public void FillBaseStacks(string[] input)
    {
        // read stacks
        var numberOfStacks = reg.Matches(input[0]).Count;

        var reverseStackOfItems = new List<Stack<string>>();
        for (int i = 0; i < numberOfStacks; i++)
        {
            // fill list with empty stacks
            reverseStackOfItems.Add(new Stack<string>());
            ShipCargo.Add(new Stack<string>());
        }

        foreach (var line in input)
        {
            var matches = reg.Matches(line);
            if (matches.Count != numberOfStacks)
            {
                break;
            }

            for (int i = 0; i < numberOfStacks; i++)
            {
                // every index 2 of matches is value
                var value = matches[i].Value[1];
                reverseStackOfItems[i].Push(value.ToString());
            }
        }

        // reverse this stack, since we add them 
        for (int i = 0; i < numberOfStacks; i++)
        {
            // every index 2 of matches is value
            while(reverseStackOfItems[i].Count != 0)
            {
                var value = reverseStackOfItems[i].Pop();
                if(!string.IsNullOrWhiteSpace(value)){
                    ShipCargo[i].Push(value);
                }
            }
        }
    }

    public void Move(string[] input)
    {
        var RegexNumber = new Regex(@"\d+");
        foreach (var line in input)
        {
            var matches = RegexNumber.Matches(line);
            if(matches.Count != 3){
                continue;
            }
            // move 1 from 5 to 6
            // index 0 = how many
            // index 1 = from
            // index 2 = destination

            int.TryParse(matches[0].Value, out int index);
            int.TryParse(matches[1].Value, out int from);
            int.TryParse(matches[2].Value, out int destination);
            for (int i = 0; i < index; i++)
            {
                var value = ShipCargo[from-1].Pop();
                ShipCargo[destination-1].Push(value);
            }
        }
    }

    public string readTopStack()
    {
        var result = new StringBuilder();
        for (int i = 0; i < ShipCargo.Count; i++)
        {
            result.Append(ShipCargo[i].Peek());
        }
        return result.ToString();
    }
}