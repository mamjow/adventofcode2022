using app;
namespace Days;
public class Day6 : ISolve
{
    public string SolvePartOne(string[] input)
    {
        int score = 0;
        foreach (var line in input)
        {
            score += findMarkerindex(line, 4);
        }
        return score.ToString();
    }

    public string SolvePartTwo(string[] input)
    {
                int score = 0;
        foreach (var line in input)
        {
            score += findMarkerindex(line, 14);
        }
        return score.ToString();
    }

    public int findMarkerindex(string lane,int length)
    {
        for (int i = 0; i < lane.Length-length-1; i++)
        {
            var forChart = lane.Substring(i,length).ToHashSet();
            if(forChart.Count == length){
                return i+length;
            }
        }
        throw new Exception();
    }
}