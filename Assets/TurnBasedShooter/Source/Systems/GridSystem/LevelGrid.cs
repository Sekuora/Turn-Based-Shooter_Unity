using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    // Level Grid Instance
    public static LevelGrid Instance { get; private set; }

    // Entity Referenced Components
    [SerializeField] private Transform gridDebugAgent;
    private GridSystem gridSystem;

    // Pre start function initialiazation
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

        // Create The Grid System
        gridSystem = new GridSystem(10, 10, 2f);
        // Create Debug Data Agents
        gridSystem.CreateDebugData(gridDebugAgent);
    }

    // Get Grid Object and sets a unit in it.
    public void AddUnitAtGridPosition(GridPosition gridPosition, Player playerUnit)
    {
        // From grid system get the gridcell at specified grid position.
        GridCell gridCell = gridSystem.GetGridCell(gridPosition);
        gridCell.AddPlayerUnit(playerUnit);
    }

    // Returns the unit at a grid position.
    public List<Player> GetUnitsAtGridPosition(GridPosition gridPosition)
    {
        GridCell gridCell = gridSystem.GetGridCell(gridPosition);
        return gridCell.GetPlayerUnits();
    }

    // Clears unit reference at a grid position
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Player playerUnit)
    {
        GridCell gridCell = gridSystem.GetGridCell(gridPosition);
        gridCell.RemovePlayerUnit(playerUnit);
    }


    public void UnitMovedGridPosition(Player playerUnit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, playerUnit);
        AddUnitAtGridPosition(toGridPosition, playerUnit);
    }

    // Getter for Grid Position pertaining to a grid system.
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

}
