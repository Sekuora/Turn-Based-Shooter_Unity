// Copyright(c) 2025 Fyragic. All rights reserved.
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridVFX : MonoBehaviour
{
    public static GridVFX Instance { get; private set; }

    [SerializeField] private Transform GridSystemTile;


    private GridTile[,] individualTiles;


    public enum GridTileType
    {
        White,
        Blue,
        Red,
        Yellow,

        OpaqueRed
    }

    [Serializable]
    public struct GridTileMaterial
    {
        public GridTileType tileType;
        public Material material;

    }

    [SerializeField]
    private List<GridTileMaterial> tileMaterialsList;

    private void Awake()
    {
        // Set Instance
        if (Instance != null)
        {
            Debug.LogWarning("Warning: Instance of: " + Instance + " already exists");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        individualTiles = new GridTile[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
        LevelGrid.Instance.GetWidth();
        LevelGrid.Instance.GetHeight();

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new(x, z);
                // Instantiate grid tiles at each grid positon
                Transform gridTileTransform = Instantiate(GridSystemTile, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                // From each gridTilePosition, get its grid tile component
                individualTiles[x, z] = gridTileTransform.GetComponent<GridTile>();
            }
        }

        UnitsActionSystem.Instance.OnSelectedActionChanged += OnSelectedActionChanged_Event;
        LevelGrid.Instance.OnAnyUnitMovedGridPosition += OnAnyUnitMovedGridPosition_Event;

        UpdateGridTile();
    }

    private void OnAnyUnitMovedGridPosition_Event(object sender, EventArgs e)
    {
        UpdateGridTile();
    }

    private void OnSelectedActionChanged_Event(object sender, EventArgs e)
    {
        UpdateGridTile();
    }

    public void HideAllGridPositions()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {

                // From each gridTilePosition, get its grid tile component
                individualTiles[x, z].Hide();
            }
        }
    }

    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridTileType tileType)
    {
        List<GridPosition> gridPositions = new List<GridPosition>();

        for(int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                // Check if Grid Position is empty
                if (!LevelGrid.Instance.IsGridPositionInRange(testGridPosition))
                {

                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > range)
                {
                    continue;
                }

                gridPositions.Add(testGridPosition);
            }
        }

        ShowGridPositions(gridPositions, tileType);
    }

    public void ShowGridPositions(List<GridPosition> gridPositions, GridTileType tileType)
    {
        foreach (GridPosition gridPosition in gridPositions)
        {
            individualTiles[gridPosition.x, gridPosition.z].Show(CollectGridTileMaterial(tileType));
        }
    }

    // Update tile if is valid for current actions
    private void UpdateGridTile()
    {
        Player playerUnit = UnitsActionSystem.Instance.GetActivePlayerUnit();
        PrimalAction activeAction = UnitsActionSystem.Instance.ActiveAction;
        // Hide positions by default
        HideAllGridPositions();

        GridTileType gridTileType;

        switch(activeAction)
        {
            default:
            case MoveAction moveAction:

                gridTileType = GridTileType.White;
                break;

            case SpinAction spinAction:
                gridTileType = GridTileType.Blue;
                break;

            case ShootAction shootAction:
                gridTileType = GridTileType.Red;

                ShowGridPositionRange(playerUnit.CurrentGridPosition, shootAction.MaxShootDistance, GridTileType.OpaqueRed);

                break;


        }
        // Show only valid grid positions
        ShowGridPositions(activeAction.CheckValidActionGrids(), gridTileType);

    }


    private Material CollectGridTileMaterial(GridTileType tileType)
    {
        foreach (GridTileMaterial gridTileMaterial in tileMaterialsList)
        {
            if(gridTileMaterial.tileType == tileType)
            {
                return gridTileMaterial.material;
            }
        }

        Debug.LogError("Could not find a material for tile type");
        return null;
    }
}
