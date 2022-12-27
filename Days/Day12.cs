using System.Collections.Generic;
using app;
namespace Days;
public class Day12 : ISolve
{
    List<List<char>> Map = new List<List<char>>();
    (int, int) StartPoint;
    (int, int) EndPoint;
    TreeRouteNode Route;
    List<int> possibleRoutes = new List<int>();
    int ShortestPath;

    int VertivalLimit;
    int HorizontalLimit;

    public string SolvePartOne(string[] input)
    {
        foreach (var line in input)
        {
            Map.Add(line.ToCharArray().ToList());
        }
        FindStartAndEndPoints();
        FillRoute(true);
        ReadTreeDept(this.Route);
        possibleRoutes.Sort();
        var answer = possibleRoutes[0].ToString();
        return answer;
    }

    public string SolvePartTwo(string[] input)
    {
        return "s";
    }

    private void FindStartAndEndPoints()
    {
        var row = Map.FindIndex(x => x.Contains('S'));
        var col = Map[row].FindIndex(x => x == 'S');
        StartPoint = (row, col);

        row = Map.FindIndex(x => x.Contains('E'));
        col = Map[row].FindIndex(x => x == 'E');
        EndPoint = (row, col);
        VertivalLimit = this.Map.Count;
        HorizontalLimit = this.Map[0].Count;
    }
    private void FillRoute(bool forwardDirection)
    {
        this.Route = new TreeRouteNode(forwardDirection ? StartPoint : EndPoint, forwardDirection ? 'S' : 'E');

        FindStep(this.Route, forwardDirection);
    }


    private void ReadTreeDept(TreeRouteNode node)
    {
        if (node.Childeren.Count == 0 || node.Destination == true)
        {
            return;
        }
        else
        {
            foreach (var child in node.Childeren)
            {
                // Console.WriteLine($"Who: {child.MyRoute} lenght {child.MyRoute.Length}");
                if (child.Destination)
                {
                    //Console.WriteLine($"Who: {child.MyRoute} lenght {child.MyRoute.Length}");
                    possibleRoutes.Add(child.MyRoute.Length);
                    return;
                }
                ReadTreeDept(child);

            }
            return;
        }
    }

    private bool IsGoodNeighbour(TreeRouteNode sourceNode, char destinationChar, bool forward)
    {
        char sourdeNodeElevation = sourceNode.ElevationLevel;
        if (forward)
        {
            destinationChar = destinationChar == 'E' ? '{' : destinationChar;
            sourdeNodeElevation = sourdeNodeElevation == 'S' ? '`' : sourdeNodeElevation;
            // destination should be 1 higher dat sourse node
            // or as much as wants lower
            // a -> b so b - a = 1
            // a -> a so a - a = 0
            // z -> a so z - a = -24
            return ((int)destinationChar - (int)sourdeNodeElevation) <= 1;
        }
        else
        {
            destinationChar = destinationChar == 'S' ? '`' : destinationChar;
            sourdeNodeElevation = sourdeNodeElevation == 'E' ? '{' : sourdeNodeElevation;
            // b -> a so a - b = -1
            // b -> b so b - b = 0
            // a -> z so a - z = +24
            return ((int)destinationChar - (int)sourdeNodeElevation) >= -1;
        }
    }


    private void FindStep(TreeRouteNode node, bool forward)
    {
        // (row , col)
        //Console.WriteLine(node.MyRoute);
        PrintNodePath(node);
        var row = node.Position.Item1;
        var col = node.Position.Item2;
        char elevationLevel = node.ElevationLevel;
        // check ur location
        // check limits
        if (elevationLevel == 'v')
        {
            Thread.Sleep(10);
        }
        var beenThere = false;
        if (col == 0 || col == HorizontalLimit - 1)
        {
            // x is 0 then its at edge of
            char neighbour = col == 0 ? (Map[row][col + 1]) : (Map[row][col - 1]);

            if (neighbour == (forward ? 'E' : 'S') && IsGoodNeighbour(node, neighbour, forward))
            {
                // boom
                node.Destination = true;
                return;
            }

            beenThere = node.RouteHistory.Contains((row, col == 0 ? col + 1 : col - 1));
            if (IsGoodNeighbour(node, neighbour, forward) && !beenThere)
            {
                node.Childeren.Add(new TreeRouteNode((row, col == 0 ? col + 1 : col - 1), neighbour, node));
            }
        }
        else
        {
            // then 2 veritcal buurman
            char neighbourLeft = Map[row][col - 1];
            char neighbourRight = Map[row][col + 1];
            if ((neighbourLeft == (forward ? 'E' : 'S') && IsGoodNeighbour(node, neighbourLeft, forward)) || (IsGoodNeighbour(node, neighbourRight, forward) && neighbourRight == (forward ? 'E' : 'S')))
            {
                // booom
                node.Destination = true;
                return;
            }

            beenThere = node.RouteHistory.Contains((row, col - 1));
            if (IsGoodNeighbour(node, neighbourLeft, forward) && !beenThere)
            {
                node.Childeren.Add(new TreeRouteNode((row, col - 1), neighbourLeft, node));
            }

            beenThere = node.RouteHistory.Contains((row, col + 1));
            if (IsGoodNeighbour(node, neighbourRight, forward) && !beenThere)
            {
                node.Childeren.Add(new TreeRouteNode((row, col + 1), neighbourRight, node));
            }
        }

        if (row == 0 || row == VertivalLimit - 1)
        {
            // Y is 0 then its at edge of map
            char neighbour = row == 0 ? (Map[row + 1][col]) : (Map[row - 1][col]);
            if (neighbour == (forward ? 'E' : 'S') && IsGoodNeighbour(node, neighbour, forward))
            {
                // booom
                node.Destination = true;
                return;
            }

            beenThere = node.RouteHistory.Contains((row == 0 ? row + 1 : row - 1, col));
            if (IsGoodNeighbour(node, neighbour, forward) && !beenThere)
            {
                node.Childeren.Add(new TreeRouteNode((row == 0 ? row + 1 : row - 1, col), neighbour, node));
            }
        }
        else
        {
            // then 2 veritcal buurman
            char neighbourTop = Map[row + 1][col];
            char neighbourBot = Map[row - 1][col];

            if ((neighbourTop == (forward ? 'E' : 'S') && IsGoodNeighbour(node, neighbourTop, forward)) || (neighbourBot == (forward ? 'E' : 'S') && IsGoodNeighbour(node, neighbourBot, forward)))
            {
                // booom
                node.Destination = true;
                return;
            }

            beenThere = node.RouteHistory.Contains((row + 1, col));
            if (IsGoodNeighbour(node, neighbourTop, forward) && !beenThere)
            {
                node.Childeren.Add(new TreeRouteNode((row + 1, col), neighbourTop, node));
            }

            beenThere = node.RouteHistory.Contains((row - 1, col));
            if (IsGoodNeighbour(node, neighbourBot, forward) && !beenThere)
            {
                node.Childeren.Add(new TreeRouteNode((row - 1, col), neighbourBot, node));
            }
        }
        // check all neighbours

        // if got "s" means mission accomplished
        // else recoursive
        // exit if no good neighbours
        foreach (var child in node.Childeren)
        {
            // if is not parent
            // if (latestPathEnd == node.ElevationLevel)
            // {
            //Console.WriteLine($"elevation {child.ElevationLevel} cost: {child.RouteHistory.Count}");
            // if (child.RouteHistory.Count > ShortestPath)
            //     continue;
            //}
            //this.ShortestPath = child.RouteHistory.Count <= ShortestPath ? child.RouteHistory.Count : ShortestPath;
            FindStep(child, forward);
        }
    }

    private void PrintNodePath(TreeRouteNode node)
    {

        Console.Clear();
        Console.WriteLine($"{node.ElevationLevel}-{node.MyRoute}-{node.RouteHistory.Count}");
        Console.Write(Environment.NewLine);
        List<List<char>> map = new List<List<char>>();
        var history = node.RouteHistory;
        history.Add(node.Position);
        for (int i = 0; i < 42; i++)
        {
            for (int j = 0; j < 133; j++)
            {

                if (history.Contains((i, j)))
                {
                    if ((i, j) == node.Position)
                    {
                        Console.Write("+");
                    }
                    else
                    {
                        Console.Write("█");
                    }
                }
                else
                {
                    Console.Write("░");
                }

            }
            Console.Write(Environment.NewLine);
        }

    }
}


public class TreeRouteNode
{
    public List<TreeRouteNode> Childeren { get; set; } = new List<TreeRouteNode>();
    public TreeRouteNode? Parent { get; set; }
    public (int, int) Position;
    public HashSet<(int, int)> RouteHistory
    {
        get
        {
            var initialset = new HashSet<(int, int)> { Position };
            if (Parent != null)
            {
                initialset.UnionWith(Parent.RouteHistory);
            }
            return initialset;
        }
    }
    public bool Destination = false;
    public char ElevationLevel;

    public string MyRoute
    {
        get
        {
            return Parent?.MyRoute + ElevationLevel.ToString();
        }
    }

    public TreeRouteNode((int, int) position, char level, TreeRouteNode? mama)
    {
        this.Position = position;

        this.Parent = mama;

        this.ElevationLevel = level;
    }

    public TreeRouteNode((int, int) position, char level)
    {
        this.Position = position;

        this.ElevationLevel = level;
    }

}