namespace Days;
public class DayTwo
{
    // A   X   Rock      1
    // B   Y   Paper     2
    // C   Z   Scissors  3

    // lose = 0
    // draw = 3
    // win = 6

    // Part 2
    // x = lose
    // y = draw
    // y = win

    public int CalculateScorePart1(string elf, string me)
    {
        return GetMoveScore(Decrypt(me)) + GetGameScore(elf, Decrypt(me));
    }

    public int CalculateScorePart2(string elf, string me)
    {
        var gameScore = DecryptPart2(me);
        var moveScore = GetMoveScorePart2(elf, gameScore);
        return gameScore + moveScore;
    }

    private int DecryptPart2(string gameResult)
    {
        switch (gameResult)
        {
            case "X":
                return 0;
            case "Y":
                return 3;
            case "Z":
                return 6;
            default:
                throw new InvalidDataException();
        }
    }

    private string Decrypt(string whatIplayed)
    {
        switch (whatIplayed)
        {
            case "X":
                return "A";
            case "Y":
                return "B";
            case "Z":
                return "C";
            default:
                throw new InvalidDataException();
        }
    }

    private int GetMoveScore(string move)
    {
        switch (move)
        {
            case "A":
                return 1;
            case "B":
                return 2;
            case "C":
                return 3;
            default:
                throw new InvalidDataException();
        };
    }

    private int GetMoveScorePart2(string elfPlayes, int result)
    {
        switch (result)
        {
            case 0:
                return GetMoveScore(Lose(elfPlayes));
            case 3:
                return GetMoveScore(elfPlayes);
            case 6:
                return GetMoveScore(Win(elfPlayes));
            default:
                throw new InvalidDataException();
        };
    }
    private int GetGameScore(string elf, string me)
    {
        if (elf.Equals(me))
        {
            return 3;
        }
        else if ((elf.Equals("A") && me.Equals("B")) || (elf.Equals("B") && me.Equals("C")) || (elf.Equals("C") && me.Equals("A")))
        {
            return 6;
        }
        else
        {
            return 0;
        }
    }

    private string Lose(string play)
    {

        if (play.Equals("A"))
        {
            return "C";
        }
        else if (play.Equals("B"))
        {
            return "A";
        }
        else
        {
            return "B";
        }

    }
    private string Win(string play)
    {

        if (play.Equals("A"))
        {
            return "B";
        }
        else if (play.Equals("B"))
        {
            return "C";
        }
        else
        {
            return "A";
        }

    }
}