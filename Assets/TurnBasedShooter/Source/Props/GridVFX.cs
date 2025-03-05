// Copyright(c) 2025 Fyragic. All rights reserved.
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridVFX : MonoBehaviour
{
    public static GridVFX Instance { get; private set; }

    [SerializeField] private Transform GridSystemTile;


    private GridTile[,] individualTiles;


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
    }

    private void Update()
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

    public void ShowGridPositions(List<GridPosition> gridPositions)
    {
        foreach (GridPosition gridPosition in gridPositions)
        {
            individualTiles[gridPosition.x, gridPosition.z].Show();
        }
    }

    // Update tile if is valid for current actions
    private void UpdateGridTile()
    {

        PrimalAction activeAction = UnitsActionSystem.Instance.ActiveAction;
        // Hide positions by default
        HideAllGridPositions();

        // Show only valid grid positions
        ShowGridPositions(activeAction.CheckValidActionGrids());

    }
}
