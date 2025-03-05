// Copyright(c) 2025 Fyragic. All rights reserved.
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Grid cells that can store data such as player units stepping over.
public class GridCell
{
    // Entity Referenced Components
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    // Player Reference
    private List<Player> playerUnits;

    // Constructor
    public GridCell(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;

        playerUnits = new List<Player>();
    }

    // Getters
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    // Get Player Units List
    public List<Player> GetPlayerUnits()
    {
        return playerUnits;
    }

    // Add Player Unit to List
    public void AddPlayerUnit(Player inPlayerUnit)
    {
        playerUnits.Add(inPlayerUnit);
        
    }

    // Remove Player Unit from List
    public void RemovePlayerUnit(Player inPlayerUnit)
    {
        playerUnits.Remove(inPlayerUnit);

    }

    // print unit coordinates and player unit
    public override string ToString()
    {
        string unitString = "";
        foreach(Player playerUnit in playerUnits)
        {
            unitString += playerUnit.name + "\n";
        }

        return gridPosition.ToString() + "\n" + unitString;
    }

    public bool GridCellContainsPlayers()
    {
        return playerUnits.Count > 0;
    }

    public Player CollectPlayerUnitInCell()
    {
        if (GridCellContainsPlayers())
        {
            return playerUnits[0];
        }
        else
        {
            return null;
        }
    }
}
