using System.Text;
using app;
namespace Days;
public class Day10 : ISolve
{
    int Cycles = 0;
    int SignalPower = 0;
    int X = 1;
    List<char> output = new List<char>();
    public string SolvePartOne(string[] input)
    {
        foreach (var line in input)
        {
            readLine(line);
        }
        return SignalPower.ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        var result = new StringBuilder();
        result.Append(Environment.NewLine);
        for (int i = 0; i < output.Count; i++)
        {
            result.Append(output[i]);
            if ((i + 1) % 40 == 0)
            {
                result.Append(Environment.NewLine);
            }
        }

        return result.ToString();
    }

    private void readLine(string line)
    {
        var actionAndValue = line.Split(" ");
        var action = actionAndValue[0];

        if (action.Equals("addx"))
        {
            var value = int.Parse(actionAndValue[1]);
            for (int i = 0; i < 2; i++)
            {
                Cycles++;
                FillOutput();
                UpdateSignalPower();
            }
            X += value;
        }
        else
        {
            // noop
            Cycles++;
            FillOutput();
            UpdateSignalPower();
        }
    }
    private void UpdateSignalPower()
    {
        if (Cycles == 20 || (Cycles - 20) % 40 == 0)
        {
            SignalPower += (X * Cycles);
        }
    }

    private void FillOutput()
    {
        var horizontalPosition = (Cycles % 40);
        if (horizontalPosition == 0)
        {
            horizontalPosition = 40;
        }
        if (horizontalPosition >= X && horizontalPosition <= (X + 2))
        {
            output.Add('#');
        }
        else
        {
            output.Add(' ');
        }
    }
}