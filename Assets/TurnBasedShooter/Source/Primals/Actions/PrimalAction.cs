// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrimalAction : PlayerExoskeleton
{
    public static EventHandler OnAnyActionStart;

    public static EventHandler OnAnyActionComplete;

    protected bool IsActive;

    protected Action onActionComplete;

    private string actionName;

    private int actionPointsCost;

    [SerializeField]
    protected int damageAmount;

    // Set or get action name
    public string ActionName { get => actionName; set => actionName = value; }

    public int ActionPointsCost { get => actionPointsCost; set => actionPointsCost = value; }


    protected void ActionStart(Action onActionComplete)
    {
        IsActive = true;
        this.onActionComplete = onActionComplete;

        OnAnyActionStart?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        IsActive = false;
        onActionComplete();

        OnAnyActionComplete?.Invoke(this, EventArgs.Empty);
    }

    public virtual bool IsValidActionGrid(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositions = CheckValidActionGrids();
        // If list contains the grid position passed as parameter returns true, else false
        return validGridPositions.Contains(gridPosition);
    }

    public abstract List<GridPosition> CheckValidActionGrids();
    
    
}
