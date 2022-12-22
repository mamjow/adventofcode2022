using app;
namespace Days;
public class Day9 : ISolve
{
    HashSet<(int, int)> HeadMoves = new HashSet<(int, int)>();
    (int, int) HeadCurrentPosition = (0, 0);
    (int, int) TailCurrentPosition = (0, 0);
    HashSet<(int, int)> TailMoves = new HashSet<(int, int)>();

    public string SolvePartOne(string[] input)
    {
        HeadMoves.Add(HeadCurrentPosition);
        TailMoves.Add(TailCurrentPosition);
        foreach (var line in input)
        {
            ReadMove(line);
        }
        return TailMoves.Count.ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
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
                    DecideTaileMove();
                }
                break;
            case "D":
                for (int i = 0; i < distance; i++)
                {
                    HeadCurrentPosition = (HeadCurrentPosition.Item1, HeadCurrentPosition.Item2 - 1);
                    HeadMoves.Add(HeadCurrentPosition);
                    DecideTaileMove();
                }
                break;
            case "R":
                for (int i = 0; i < distance; i++)
                {
                    HeadCurrentPosition = (HeadCurrentPosition.Item1 + 1, HeadCurrentPosition.Item2);
                    HeadMoves.Add(HeadCurrentPosition);
                    DecideTaileMove();
                }
                break;
            case "L":
                for (int i = 0; i < distance; i++)
                {
                    HeadCurrentPosition = (HeadCurrentPosition.Item1 - 1, HeadCurrentPosition.Item2);
                    HeadMoves.Add(HeadCurrentPosition);
                    DecideTaileMove();
                }
                break;
            default:
                break;
        }

    }

    private void DecideTaileMove()
    {
        var varticalDistance = HeadCurrentPosition.Item2 - TailCurrentPosition.Item2;
        var horizontalDistance = HeadCurrentPosition.Item1 - TailCurrentPosition.Item1;

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
                TailCurrentPosition.Item2 = (varticalDistance < 0) ? TailCurrentPosition.Item2 - 1 : TailCurrentPosition.Item2 + 1;
                break;
            case (2, 0):
                // simple move
                TailCurrentPosition.Item1 = (horizontalDistance < 0) ? TailCurrentPosition.Item1 - 1 : TailCurrentPosition.Item1 + 1;
                break;
            default:
                TailCurrentPosition.Item1 = (horizontalDistance < 0) ? TailCurrentPosition.Item1 - 1 : TailCurrentPosition.Item1 + 1;
                TailCurrentPosition.Item2 = (varticalDistance < 0) ? TailCurrentPosition.Item2 - 1 : TailCurrentPosition.Item2 + 1;
                break;
        }
        TailMoves.Add(TailCurrentPosition);
    }
}

