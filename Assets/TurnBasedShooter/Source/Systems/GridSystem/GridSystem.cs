// Copyright(c) 2025 Fyragic. All rights reserved.
using UnityEngine;

/*
* Remove monobehavior to get a normal c# class
*/
public class GridSystem
{
    // Grid Data
    private int width;
    private int height;
    private float cellSize;

    // Define 2D Array for grid cells. Each cell can store its x, z positions.
    private GridCell[,] gridCellsArray;

    // Constructor
    public GridSystem(int width, int height, float cellSize)
    {
        // Grid Data
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        // Create new grid objects array
        gridCellsArray = new GridCell[width, height];

        // Generate grid: x for columns, z for rows.
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                
                // Assign grid positions to x, z
                GridPosition gridPosition = new GridPosition(x, z);
                // Store grid positions inside an arrray of grid positions.
                gridCellsArray[x, z] = new GridCell(this, gridPosition); 
            }
        }
    }

    // Define world vector - Grid to world position transforms.
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;

    }

    // Define grid position, used to return current grid position
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize),Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    // Debug UI I made to shows numbers in game
    // DebugDataAgent is the physical representation in game that holds the data for each GridCell.
    public void CreateDebugData(Transform debugAgent)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                /* For each x, z ; column, row in the grid,
                 * instantiate a new debug data agent at each grid position. */
                Transform debugAgentTransform = GameObject.Instantiate(
                    debugAgent, 
                    GetWorldPosition(gridPosition), 
                    Quaternion.identity
                    );

                // Create debug agents
                GridDebugAgent gridDebugAgent = debugAgentTransform.GetComponent<GridDebugAgent>();

                // Set agent to a grid cell
                gridDebugAgent.SetGridCell(GetGridCell(gridPosition));
            }
        }
    }

    // Get grid cell in a grid position
    public GridCell GetGridCell(GridPosition gridPosition)
    {
        try
        {
            return gridCellsArray[gridPosition.x, gridPosition.z];
        }
        catch (System.IndexOutOfRangeException ex)
        {
            Debug.Log("Exception: " + ex);
            Debug.Log("Location: " + gridPosition.x + "_" + gridPosition.z);
        }
        return null;
    }
 
} 
