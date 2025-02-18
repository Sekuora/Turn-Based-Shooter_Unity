using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrimalAction : PlayerExoskeleton
{
    protected bool IsActive;

    protected Action onActionComplete;

    private string actionName;

    // Set or get action name
    public string ActionName { get => actionName; set => actionName = value; }

    public virtual bool IsValidActionGrid(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositions = CheckValidActionGrids();
        // If list contains the grid position passed as parameter returns true, else false
        return validGridPositions.Contains(gridPosition);
    }

    public abstract List<GridPosition> CheckValidActionGrids();
}
