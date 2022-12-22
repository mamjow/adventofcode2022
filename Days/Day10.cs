using app;
namespace Days;
public class Day10 : ISolve
{
    int Cycles = 0;
    int SignalPower = 0;
    int X = 1;
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
        throw new NotImplementedException();
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
                if (Cycles == 20 || (Cycles - 20) % 40 == 0)
                {
                    var a = (X * Cycles);
                    SignalPower += a;
                }
            }
            X += value;
        }
        else
        {
            // noop
            Cycles++;
            if (Cycles == 20 || (Cycles - 20) % 40 == 0)
            {
                var a = (X * Cycles);
                SignalPower += a;
            }
        }
    }
}