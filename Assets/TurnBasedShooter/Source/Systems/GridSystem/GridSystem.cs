// Copyright(c) 2025 Fyragic. All rights reserved.
using UnityEngine;

/*
* Remove monobehavior to get a normal c# class
*/
public class GridSystem
{

    private int width;
    private int height;
    private float cellSize;

    // Define 2D Array
    private GridObject[,] gridObjectArray;

    // Entry Point for Grid System
    public GridSystem(int width, int height, float cellSize)
    {
        // Grid Data
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        // Generate grid: x for columns, z for rows.
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    // Define world vector
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    // Define grid position
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
        Mathf.RoundToInt(worldPosition.x / cellSize),
        Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    public void CreateDebugObjects(Transform debugObject)
    {
        // Repeat creation of grid system
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform debugObjectTransform = GameObject.Instantiate(debugObject, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugObjectTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

} 
