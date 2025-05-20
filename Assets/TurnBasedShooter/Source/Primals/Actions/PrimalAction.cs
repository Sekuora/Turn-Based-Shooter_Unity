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
    

    public AIActionData sortAIActionProbability()
    {
        // List AI Actions
        List<AIActionData> AIActions = new List<AIActionData>();

        List<GridPosition> validActionGridPositions = CheckValidActionGrids();

        foreach (GridPosition gridPosition in validActionGridPositions)
        {
            // Get AI Action from each action return AI Action implementation
            AIActionData unitAIAction = GetAIAction(gridPosition);

            // Store them to AI Actions List
            AIActions.Add(unitAIAction);
        }

        // If Actions are found
        if (AIActions.Count > 0)
        {
            // Sort Algorithm that takes two actions an return its difference
            AIActions.Sort((AIActionData a, AIActionData b) => b.actionValue - a.actionValue);

            // Return the action at index 0
            return AIActions[0];
        }
        else
        {
            return null;
        }

    }

    public abstract AIActionData GetAIAction(GridPosition gridPosition);
  
}
