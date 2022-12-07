using app;
namespace Days;

public class DayThree : ISolve
{
    private List<char> alhpabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();

    private int GetLaneScore(string lane)
    {
        var firstHalf = lane.Substring(0, lane.Length / 2).ToCharArray();
        var secondHalf = lane.Substring(lane.Length / 2).ToCharArray();
        var incomon = firstHalf.Where(x => secondHalf.Contains(x)).ToList();
        return alhpabets.IndexOf(incomon[0]) + 1;
    }

    private int Get3LaneScore(string[] lane)
    {
        if (lane.Length > 3)
        {
            throw new Exception();
        }
        var firstLane = lane[0].ToCharArray();

        var incomon = firstLane.Where(c => lane[1].Contains(c) && lane[2].Contains(c)).FirstOrDefault();
        return alhpabets.IndexOf(incomon) + 1;
    }

    public int SolvePartOne(string[] input)
    {
        var score = 0;
        foreach (var item in input)
        {
            score += GetLaneScore(item);
        }     
        return score;
    }
    public int SolvePartTwo(string[] input)
    {
        var score = 0;
        for (int i = 0; i < input.Length; i += 3)
        {
            var list = input.ToList().GetRange(i, 3).ToArray();

            score += Get3LaneScore(list);
        }
        return score;
    }
}