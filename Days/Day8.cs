using app;
namespace Days;
public class Day8 : ISolve
{
    public List<List<int>> Matrix = new List<List<int>>();
    public int VerticalLength;
    public int HorizontalLength;
    public List<int> Corners = new List<int>();
    public string SolvePartOne(string[] input)
    {
        Matrix.Clear();
        foreach (var item in input)
        {
            var newList = item.ToList().Select(item =>
            {
                int.TryParse(item.ToString(), out int num);
                return num;
            }).ToList();
            Matrix.Add(newList);
        }

        return readVisibleTrees().ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        Matrix.Clear();
        foreach (var item in input)
        {
            var newList = item.ToList().Select(item =>
            {
                int.TryParse(item.ToString(), out int num);
                return num;
            }).ToList();
            Matrix.Add(newList);
        }

        return FindBestView();
    }

    public int readVisibleTrees()
    {
        VerticalLength = this.Matrix.Count;
        HorizontalLength = this.Matrix[0].Count;
        Corners = new List<int> { 0, (HorizontalLength - 1), (VerticalLength - 1) };
        var totalVisibleTrees = 0;
        for (int yAxis = 0; yAxis < HorizontalLength; yAxis++)
        {
            for (int xAxis = 0; xAxis < VerticalLength; xAxis++)
            {
                if (IsVisible(xAxis, yAxis))
                {
                    totalVisibleTrees++;
                }
            }
        }

        return totalVisibleTrees;
    }

    public string FindBestView()
    {
        VerticalLength = this.Matrix.Count;
        HorizontalLength = this.Matrix[0].Count;
        Corners = new List<int> { 0, (HorizontalLength - 1), (VerticalLength - 1) };
        var bestView = 0;
        for (int yAxis = 0; yAxis < HorizontalLength; yAxis++)
        {
            for (int xAxis = 0; xAxis < VerticalLength; xAxis++)
            {
                if (IsVisible(xAxis, yAxis))
                {
                    var lastValue = calculateView(xAxis,yAxis);
                    bestView = (bestView < lastValue) ? lastValue: bestView;
                }
            }
        }
        return bestView.ToString();
    }

    public bool IsVisible(int xAxis, int yAxis)
    {
        var treeHeight = Matrix[yAxis][xAxis];
        if (Corners.Contains(xAxis) || Corners.Contains(yAxis))
        {
            return true;
        }
        return CheckDirection(xAxis, yAxis);
    }

    public int calculateView(int xAxis, int yAxis)
    {
        var treeHeight = Matrix[yAxis][xAxis];
        var top = new List<int>();
        var bottom = new List<int>();
        // yAxis
        for (int i = 0; i < VerticalLength; i++)
        {
            if (i > yAxis)
            {
                bottom.Add(Matrix[i][xAxis]);  
            }
            else if (i < yAxis)
            {
                top.Add(Matrix[i][xAxis]);
            }
        }
        top.Reverse();
        // xAxis
        var row = Matrix[yAxis];
        var left = row.GetRange(0, xAxis);
        left.Reverse();
        var right = row.GetRange(xAxis + 1, (HorizontalLength - left.Count - 1));

        var leftValue = left.FindIndex(x => x >= treeHeight);
        var rightValue = right.FindIndex(x => x >= treeHeight);
        var topValue = top.FindIndex(x => x >= treeHeight) ;
        var botValue = bottom.FindIndex(x => x >= treeHeight);

        leftValue = leftValue < 0 ? left.Count : leftValue  + 1;
        rightValue = rightValue < 0 ? right.Count : rightValue  + 1;
        topValue = topValue < 0 ? top.Count : topValue  + 1;
        botValue = botValue < 0 ? bottom.Count : botValue  + 1;
        return leftValue  * rightValue * topValue * botValue ;
    }
    public bool CheckDirection(int xAxis, int yAxis)
    {
        var treeHeight = Matrix[yAxis][xAxis];

        var top = new List<int>();
        var bottom = new List<int>();
        // yAxis
        for (int i = 0; i < VerticalLength; i++)
        {
            if (i > yAxis)
            {
                top.Add(Matrix[i][xAxis]);
            }
            else if (i < yAxis)
            {
                bottom.Add(Matrix[i][xAxis]);
            }
        }
        // xAxis
        var row = Matrix[yAxis];
        var left = row.GetRange(0, xAxis);
        var right = row.GetRange(xAxis + 1, (HorizontalLength - left.Count - 1));
        return left.Max() < treeHeight || right.Max() < treeHeight || top.Max() < treeHeight || bottom.Max() < treeHeight;
    }
}