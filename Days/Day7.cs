using app;
namespace Days;
public class Day7 : ISolve
{
    TreeNode root = new TreeNode("root");
    public TreeNode pointer;

    public int DiskSpace = 70000000;
    public int DemandedFreeSpace = 30000000;
    public string SolvePartOne(string[] input)
    {
        ReadTree(input);
        return FindChildwithinThatLimit(this.root).Select(x => x.Size).ToList().Sum().ToString(); ;
    }

    public string SolvePartTwo(string[] input)
    {
        ReadTree(input);
        var totalUsed = this.root.Size;
        var empty = this.DiskSpace - totalUsed;
        var demandToDelet = this.DemandedFreeSpace - empty;
        var list = FindChildwithinThatLimit(this.root, demandToDelet, false).Select(x => x.Size).ToList();
        list.Sort();
        return list[0].ToString();
    }

    public void ReadTree(string[] input)
    {
        this.root = new TreeNode("root");
        this.pointer = root;
        foreach (var lane in input)
        {
            if (lane.StartsWith("$"))
            {
                PerformAction(lane);
            }
            else
            {
                FillData(lane);
            }
        }
    }
    public void PerformAction(string lane)
    {
        if (lane.Equals("$ cd /"))
        {
            this.pointer = this.root;
        }
        else if (lane.Equals("$ cd .."))
        {
            this.pointer = this.pointer.Parent;
        }
        else if (!lane.Equals("$ ls"))
        {
            // then is cd to a folder. we should have such a folder in the pointer node
            // '$ cd '  = 5 lenght
            var folderName = lane.Substring(5);
            this.pointer = this.pointer.ChilderenDir.First(x => x.Name.Equals(folderName));
        }
    }
    public void FillData(string lane)
    {
        if (lane.StartsWith("dir"))
        {
            var folderNam = lane.Substring(4);
            this.pointer?.ChilderenDir.Add(new TreeNode(folderNam, this.pointer));
        }
        else
        {
            var sizeAndFile = lane.Split(" ");
            int.TryParse(sizeAndFile[0], out var size);
            this.pointer.AddFile(size, sizeAndFile[1]);
        }

    }



    

    public List<TreeNode> FindChildwithinThatLimit(TreeNode node, int limit = 100000, bool smaller = true)
    {
        var list = node.ChilderenDir.Where(x => smaller? ( x.Size < limit) : ( x.Size > limit)).ToList();

        foreach (var child in node.ChilderenDir)
        {
            list.AddRange(FindChildwithinThatLimit(child, limit,smaller));
        }
        return list;
    }
}

public class TreeNode
{
    public List<TreeNode> ChilderenDir { get; set; } = new List<TreeNode>();
    public TreeNode? Parent { get; set; }
    public List<TreeFile> Files { get; set; } = new List<TreeFile>();
    public int Size
    {
        get
        {
            var totalFileSize = this.Files.Sum(x => x.Size);
            var totalFolderSize = this.ChilderenDir.Sum(x => x.Size);
            return totalFolderSize + totalFileSize;
        }
    }
    public string Name { get; set; }
    public TreeNode(string name, TreeNode? parent = null)
    {
        this.Name = name;
        this.Parent = parent;
    }

    public void AddFile(int size, string name)
    {
        this.Files.Add(new TreeFile() { Size = size, Name = name });
    }

}

public class TreeFile
{
    public int Size { get; set; }
    public String Name { get; set; }
}