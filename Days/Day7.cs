using app;
namespace Days;
public class Day7 : ISolve
{
    TreeNode root = new TreeNode();
    public string SolvePartOne(string[] input)
    {
        foreach (var lane in input)
        {
            if(lane.StartsWith("$")){
                PerformAction(lane);
            } else 
            {
                FillData(lane);
            }

        }
    }

    public string SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }

    public void PerformAction(string lane)
    {
        if(lane.Equals("cd /")){
            root.pointer = true;
        }
        else if(lane.Equals("ls")){

        }
    } 

    public void FillData(string lane)
    {
        
    }
}

public class TreeNode
{
    public List<TreeNode>? ChilderenDir { get; set; } = new List<TreeNode>();
    public List<TreeFile>? Files { get; set; } = new List<TreeFile>();
    public int Size { get; set; }
    public bool pointer;

    public void AddFile(string name, int size){

    }

    public TreeNode FindPointer(TreeNode node= null)
    {
        if (node == null || this.pointer ){
            return this;
        }
        
        //this.ChilderenDir.Any(x=> x.pointer)
    }
}

public class TreeFile{
    public int Size { get; set; }
    public String Name { get; set; }
}