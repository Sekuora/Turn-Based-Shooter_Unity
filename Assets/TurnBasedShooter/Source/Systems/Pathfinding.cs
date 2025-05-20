using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private int width;
    private int height;
    private float cellSize;

    private const int TBS_MoveStraightCost = 10;
    private const int TBS_MoveDiagonalCost = 14;

    [SerializeField]
    private LevelGrid levelGrid;
    private GridSystem<PathNode> gridSystem;

    [SerializeField]
    private Transform gridDebugAgent;


    private void Awake()
    {
        gridSystem = new GridSystem<PathNode>(levelGrid.Width, levelGrid.Height, levelGrid.CellSize, (GridSystem<PathNode> gridSystem, GridPosition gridPosition) => new PathNode(gridPosition));

        gridSystem.CreateDebugData(gridDebugAgent);
    }

    public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition)
    {
        // Nodes available to search path
        List<PathNode> openList = new List<PathNode>();

        // Nodes already searched
        List<PathNode> closedList = new List<PathNode>();

        // Start Node
        PathNode startNode = gridSystem.GetGridCell(startGridPosition);

        // End Node
        PathNode endNode = gridSystem.GetGridCell(endGridPosition);

        openList.Add(startNode);

        for (int x = 0; x < gridSystem.Width; x++)
        {
            for (int z = 0; z < gridSystem.Height; z++)
            {
                // Get each object's grid position
                GridPosition gridPosition = new GridPosition(x, z);

                // Get grid cells at each grid position
                PathNode pathNode = gridSystem.GetGridCell(gridPosition);

                // Calculate Pathfinding Values
                pathNode.GCost = int.MaxValue;
                pathNode.HCost = 0;
                pathNode.CalculateFCost();

                // Reset Last Walked Path Node
                pathNode.ResetLastPathNode();
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CaculateDistanceToGrid(startGridPosition, endGridPosition);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neighbourNode in CollectNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) { continue; }

                int tentativeGCost = currentNode.GCost + CaculateDistanceToGrid(currentNode.GridPosition, neighbourNode.GridPosition);
                
                if (tentativeGCost < neighbourNode.GCost)
                {
                    neighbourNode.LastPathNode = currentNode;
                    neighbourNode.GCost = tentativeGCost;
                    neighbourNode.HCost = CaculateDistanceToGrid(neighbourNode.GridPosition, endGridPosition);

                    neighbourNode.CalculateFCost();

                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
                
            }
        }

        // No Path Found
        return null;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodes = new List<PathNode>();

        pathNodes.Add(endNode);

        PathNode currentNode = endNode;

        while(currentNode.LastPathNode != null)
        {
            pathNodes.Add(currentNode.LastPathNode);

            currentNode = currentNode.LastPathNode;
        }

        pathNodes.Reverse();

        List<GridPosition> gridPositions = new List<GridPosition>();

        foreach(PathNode pathNode in pathNodes)
        {
            gridPositions.Add(pathNode.GridPosition);
        }

        return gridPositions;
    
    }

    public int CaculateDistanceToGrid(GridPosition gridPositionA, GridPosition gridPositionB)
    {
        GridPosition gridPositionDistance = gridPositionA - gridPositionB;

        int distance = Mathf.Abs(gridPositionDistance.x) + Mathf.Abs(gridPositionDistance.z);

        int xDistance = Mathf.Abs(gridPositionDistance.x);

        int zDistance = Mathf.Abs(gridPositionDistance.z);

        int distanceToNode = Mathf.Abs(xDistance - zDistance); 

        return TBS_MoveDiagonalCost * Mathf.Min(xDistance, zDistance) + TBS_MoveStraightCost * distanceToNode;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostPathNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].FCost < lowestFCostPathNode.FCost)
            {
                lowestFCostPathNode = pathNodeList[i];
            }
        }

        return lowestFCostPathNode;
    }

    private List<PathNode> CollectNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourNodes = new List<PathNode>();

        GridPosition gridPosition = currentNode.GridPosition;

        /// Define Neighbour Nodes ///

        // Left Nodes
        if (gridPosition.x - 1 >= 0)
        {
            // Left Node
            neighbourNodes.Add(GetPathNodeCoordinates(gridPosition.x - 1, gridPosition.z));

            if (gridPosition.z - 1 >= 0)
            {

                // Left Down Node
                neighbourNodes.Add(GetPathNodeCoordinates(gridPosition.x - 1, gridPosition.z - 1));
            }

            if (gridPosition.z + 1  < gridSystem.Height)
            {

                // Left Up Node
                neighbourNodes.Add(GetPathNodeCoordinates(gridPosition.x - 1, gridPosition.z + 1));
            }
        }

        // Right Nodes
        if (gridPosition.x + 1 < gridSystem.Width)

        {
            // Right Node
            neighbourNodes.Add(GetPathNodeCoordinates(gridPosition.x + 1, gridPosition.z));

            if (gridPosition.z - 1 >= 0)
            {
                // Right Down Node
                neighbourNodes.Add(GetPathNodeCoordinates(gridPosition.x + 1, gridPosition.z - 1));
            }

            if (gridPosition.z + 1 < gridSystem.Height)
            {
                // Right Up Node
                neighbourNodes.Add(GetPathNodeCoordinates(gridPosition.x + 1, gridPosition.z + 1));
            }

        }
        if (gridPosition.z - 1 >= 0)
        {
            // Down Node
            neighbourNodes.Add(GetPathNodeCoordinates(gridPosition.x, gridPosition.z - 1));
        }

        if (gridPosition.z + 1 < gridSystem.Height)
        {
            // Up Node
            neighbourNodes.Add(GetPathNodeCoordinates(gridPosition.x, gridPosition.z + 1));
        }
       
        return neighbourNodes;
    }

    private PathNode GetPathNodeCoordinates(int x, int z)
    {
        return gridSystem.GetGridCell(new GridPosition(x, z));
    }

}
