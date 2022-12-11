using System.Text;
using app;
namespace Days;

public class DayFour : ISolve
{
    public string SolvePartOne(string[] input)
    {
        var score = 0;
        foreach (var item in input)
        {
            var pairElves = item.Split(",");
            var firstElf = GetSectionId(pairElves[0]);
            var secondElf = GetSectionId(pairElves[1]);

            var aContainB = (firstElf.Item1 >= secondElf.Item1 && firstElf.Item2 <= secondElf.Item2);
            var BContainA = (firstElf.Item1 <= secondElf.Item1 && firstElf.Item2 >= secondElf.Item2);
            if(BContainA || aContainB )
            {
                score ++;
            }
        }
        return score.ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        var score = 0;
        foreach (var item in input)
        {
            var pairElves = item.Split(",");
            var firstElf = GetSectionId(pairElves[0]);
            var secondElf = GetSectionId(pairElves[1]);

            var overlapCaseOne = (firstElf.Item1 >= secondElf.Item1 && firstElf.Item1 <= secondElf.Item2);
            var overlapCaseTwo = (secondElf.Item1 >= firstElf.Item1 && secondElf.Item1 <= firstElf.Item2);
            if(overlapCaseOne || overlapCaseTwo )
            {
                score ++;
            }
        }
        return score.ToString();
    }


    public (int, int) GetSectionId(string sectionShortHand)
    {
            var startAndEndnr = sectionShortHand.Split("-");
            int.TryParse(startAndEndnr[0], out int start);
            int.TryParse(startAndEndnr[1], out int end);
            return (start,end);
    }

}
