using app;
namespace Days;
public class Day9 : ISolve
{
    HashSet<(int, int)> HeadMoves = new HashSet<(int, int)>();
    (int, int) HeadCurrentPosition = (0, 0);
    List<(int, int)> TailsCurrentPosition = new List<(int, int)>();
    List<HashSet<(int, int)>> TailsMoves = new List<HashSet<(int, int)>>();


    public string SolvePartOne(string[] input)
    {
        return SolveGameWithNumberOfTails(input, 1);
    }

    public string SolvePartTwo(string[] input)
    {
        return SolveGameWithNumberOfTails(input, 9);
    }

    private string SolveGameWithNumberOfTails(string[] input, int numberOfTails)
    {
        // lets clean history and all shits
        HeadMoves.Clear();
        HeadCurrentPosition = (0,0);
        TailsCurrentPosition.Clear();
        TailsMoves.Clear();

        HeadMoves.Add(HeadCurrentPosition);
        // Set initial state of all tails
        for (int i = 0; i < numberOfTails; i++)
        {
            TailsCurrentPosition.Add((0, 0));
            TailsMoves.Add(new HashSet<(int, int)>() { TailsCurrentPosition[i] });
        }

        foreach (var line in input)
        {
            ReadMove(line);
        }
        return TailsMoves[^1].Count.ToString();
    }

    private void ReadMove(string move)
    {
        var moveInfo = move.Split(" ");
        var direction = moveInfo[0];
        var distance = int.Parse(moveInfo[1]);
        RecordHeadMove(direction, distance);
    }

    private void RecordHeadMove(string direction, int distance)
    {
        switch (direction)
        {
            case "U":
                for (int i = 0; i < distance; i++)
                {
                    HeadCurrentPosition = (HeadCurrentPosition.Item1, HeadCurrentPosition.Item2 + 1);
                    HeadMoves.Add(HeadCurrentPosition);
                    for (int tailIndex = 0; tailIndex < TailsMoves.Count; tailIndex++)
                    {
                        DecideTaileMove(tailIndex);
                    }

                }
                break;
            case "D":
                for (int i = 0; i < distance; i++)
                {
                    HeadCurrentPosition = (HeadCurrentPosition.Item1, HeadCurrentPosition.Item2 - 1);
                    HeadMoves.Add(HeadCurrentPosition);
                    for (int tailIndex = 0; tailIndex < TailsMoves.Count; tailIndex++)
                    {
                        DecideTaileMove(tailIndex);
                    }
                }
                break;
            case "R":
                for (int i = 0; i < distance; i++)
                {
                    HeadCurrentPosition = (HeadCurrentPosition.Item1 + 1, HeadCurrentPosition.Item2);
                    HeadMoves.Add(HeadCurrentPosition);
                    for (int tailIndex = 0; tailIndex < TailsMoves.Count; tailIndex++)
                    {
                        DecideTaileMove(tailIndex);
                    }
                }
                break;
            case "L":
                for (int i = 0; i < distance; i++)
                {
                    HeadCurrentPosition = (HeadCurrentPosition.Item1 - 1, HeadCurrentPosition.Item2);
                    HeadMoves.Add(HeadCurrentPosition);
                    for (int tailIndex = 0; tailIndex < TailsMoves.Count; tailIndex++)
                    {
                        DecideTaileMove(tailIndex);
                    }
                }
                break;
            default:
                break;
        }

    }

    private void DecideTaileMove(int index)
    {
        (int, int) headPosition;
        (int, int) tailPosition;
        if (index == 0)
        {
            // then head is REAL head
            tailPosition = TailsCurrentPosition[index];
            headPosition = HeadCurrentPosition;
        }
        else
        {
            tailPosition = TailsCurrentPosition[index];
            headPosition = TailsCurrentPosition[index - 1];
        }

        var varticalDistance = headPosition.Item2 - tailPosition.Item2;
        var horizontalDistance = headPosition.Item1 - tailPosition.Item1;

        var vertivalAbsolute = Math.Abs(varticalDistance);
        var horizontelAbsolute = Math.Abs(horizontalDistance);
        if (vertivalAbsolute <= 1 && horizontelAbsolute <= 1)
        {
            // no need to move

            return;
        }
        switch ((horizontelAbsolute, vertivalAbsolute))
        {
            case (0, 2):
                //simple move
                tailPosition.Item2 = (varticalDistance < 0) ? tailPosition.Item2 - 1 : tailPosition.Item2 + 1;
                break;
            case (2, 0):
                // simple move
                tailPosition.Item1 = (horizontalDistance < 0) ? tailPosition.Item1 - 1 : tailPosition.Item1 + 1;
                break;
            default:
                tailPosition.Item1 = (horizontalDistance < 0) ? tailPosition.Item1 - 1 : tailPosition.Item1 + 1;
                tailPosition.Item2 = (varticalDistance < 0) ? tailPosition.Item2 - 1 : tailPosition.Item2 + 1;
                break;
        }
        // update  position 
        TailsCurrentPosition[index] = tailPosition;
        // add history
        TailsMoves[index].Add(tailPosition);
    }
}

