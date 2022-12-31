using app;
namespace Days;
public class Day12 : ISolve
{
    List<List<char>> Map = new List<List<char>>();
    public HashSet<Vertex> Graph { get; set; } = new HashSet<Vertex>();
    int VertivalLimit;
    int HorizontalLimit;

    public string SolvePartOne(string[] input)
    {
        foreach (var line in input)
        {
            Map.Add(line.ToCharArray().ToList());
        }
        FindStartAndEndPoints();
        return ReadMap().ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        return "s";
    }

    private void FindStartAndEndPoints()
    {
        var row = Map.FindIndex(x => x.Contains('S'));
        var col = Map[row].FindIndex(x => x == 'S');
        row = Map.FindIndex(x => x.Contains('E'));
        col = Map[row].FindIndex(x => x == 'E'); ;
        VertivalLimit = this.Map.Count;
        HorizontalLimit = this.Map[0].Count;
    }
    private int ReadMap()
    {
        for (int r = 0; r < VertivalLimit; r++)
        {
            for (int c = 0; c < HorizontalLimit; c++)
            {
                var node = new Vertex((r, c), Map[r][c]);
                Graph.Add(node);
            }
        }
        foreach (var node in Graph)
        {
            AddNodeCandidates(node);
        }
        return FindBestPath();
    }

    private int FindBestPath()
    {
        Queue<(Vertex, int)> list = new Queue<(Vertex, int)>();
        var start = Graph.Where(x => x.ElevationLevel == 'S').First();
        var end = Graph.Where(x => x.ElevationLevel == 'E').First();
        Dictionary<Vertex, int> Visited = new Dictionary<Vertex, int>();
        var step = 0;
        list.Enqueue((start, 0));
        while (list.Any())
        {
            var item = list.Dequeue();
            var node = item.Item1;
            step = item.Item2;
            if (!Visited.ContainsKey(node))
            {
                Visited.Add(node, step);
                foreach (var candidate in node.ValidCandidates)
                {
                    list.Enqueue((candidate, step + 1));
                }
            }
        }
        return Visited[end];
    }

    public void AddNodeCandidates(Vertex node)
    {
        var row = node.Position.Item1;
        var col = node.Position.Item2;
        // up
        if (row != 0)
        {
            var nodeUp = Graph.Where(x => x.Position == (row - 1, col)).ToList();
            if (nodeUp.Count > 1)
            {
                throw new Exception();
            }
            node.AddCandidates(nodeUp[0]);
        }
        // bot
        if (row != VertivalLimit - 1)
        {
            var nodeBot = Graph.Where(x => x.Position == (row + 1, col)).ToList();
            if (nodeBot.Count > 1)
            {
                throw new Exception();
            }
            node.AddCandidates(nodeBot[0]);
        }
        // left
        if (col != 0)
        {
            var nodeLeft = Graph.Where(x => x.Position == (row, col - 1)).ToList();
            if (nodeLeft.Count > 1)
            {
                throw new Exception();
            }
            node.AddCandidates(nodeLeft[0]);
        }
        // right
        if (col != HorizontalLimit - 1)
        {
            var nodeRight = Graph.Where(x => x.Position == (row, col + 1)).ToList();
            if (nodeRight.Count > 1)
            {
                throw new Exception();
            }
            node.AddCandidates(nodeRight[0]);
        }
    }
}

public class Vertex : IEquatable<Vertex>
{
    public HashSet<Vertex> ValidCandidates { get; set; } = new HashSet<Vertex>();
    public char ElevationLevel;
    public (int, int) Position;

    public Vertex((int, int) position, char elevationLevel)
    {
        Position = position;
        ElevationLevel = elevationLevel;
    }

    public void AddCandidates(Vertex node)
    {
        var startElevation = ElevationLevel;
        var targetElevation = node.ElevationLevel;
        if (targetElevation == 'S')
        {
            return;
        }
        if (startElevation == 'S')
        {
            startElevation = 'a';
        }

        if (targetElevation == 'E')
        {
            targetElevation = 'z';
        }

        // check if really neighbor
        var defRow = Math.Abs(Position.Item1 - node.Position.Item1);
        var defCol = Math.Abs(Position.Item2 - node.Position.Item2);

        if (defRow > 1 || defCol > 1 || (defCol == 1 && defRow == 1))
        {
            return;
        }

        var def = targetElevation - startElevation;
        if (def <= 1)
        {
            ValidCandidates.Add(node);
        }
    }

    public bool Equals(Vertex? other)
    {
        return Position == other?.Position;
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj);
    }
}