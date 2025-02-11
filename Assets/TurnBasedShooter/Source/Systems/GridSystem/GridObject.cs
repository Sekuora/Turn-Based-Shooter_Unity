using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private PlayerMovementSystem playerUnit;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    // Getters
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }



    public PlayerMovementSystem GetPlayerUnit()
    {
        return playerUnit;
    }

    public void SetPlayerUnit(PlayerMovementSystem inPlayerUnit)
    {
        playerUnit = inPlayerUnit;
    }

    // print unit coordinates and player unit
    public override string ToString()
    {
        return gridPosition.ToString() + "\n" + playerUnit;
    }
}
