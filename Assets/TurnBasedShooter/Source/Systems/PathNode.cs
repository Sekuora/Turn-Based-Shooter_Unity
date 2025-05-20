using UnityEngine;

public class PathNode
{
    private GridPosition gridPosition;

    private int gCost;
    private int hCost;
    private int fCost;
    private PathNode lastPathNode;

    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public int GCost { get => gCost; set => gCost = value; }
    public int HCost { get => hCost; set => hCost = value; }
    public int FCost { get => fCost; set => fCost = value; }
    public GridPosition GridPosition { get => gridPosition; set => gridPosition = value; }
    public PathNode LastPathNode { get => lastPathNode; set => lastPathNode = value; }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void ResetLastPathNode()
    {
        lastPathNode = null;
    }

}
