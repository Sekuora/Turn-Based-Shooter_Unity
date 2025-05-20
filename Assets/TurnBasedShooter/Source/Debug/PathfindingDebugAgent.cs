using TMPro;
using UnityEngine;

public class PathfindingDebugAgent : GridDebugAgent
{

    [SerializeField]
    private TextMeshPro gCostText;

    [SerializeField]
    private TextMeshPro hCostText;

    [SerializeField]
    private TextMeshPro fCostText;

    private PathNode pathNode;

    public override void SetGridCell(object gridCell)
    {
        base.SetGridCell(gridCell);

        pathNode = (PathNode)gridCell;
    }

    protected override void Update()
    {
        base.Update();

        gCostText.text = "G Cost: " + pathNode.GCost.ToString();
        hCostText.text = "H Cost: " + pathNode.HCost.ToString();
        fCostText.text = "F Cost: " + pathNode.FCost.ToString();
    }

}
