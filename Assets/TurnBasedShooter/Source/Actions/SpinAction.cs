// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : PrimalAction
{
    private float totalSpinAmount;
    private void Awake()
    {
        ActionName = "Spin";
        ActionPointsCost = 1;
    }

    private void Update()
    {
        if (!IsActive) { return; }

        // Rotate if active is true
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;

        Debug.Log(transform.eulerAngles.y);


        // Check if player has completed 1 360 degree spin and then stop
        if (totalSpinAmount >= 360f)
        {
            ActionComplete();

        }
    }

    public void Spin(Action onActionComplete)
    {
        totalSpinAmount = 0f;

        ActionStart(onActionComplete);
    }

    public override List<GridPosition> CheckValidActionGrids()
    {
        List<GridPosition> unitGridPosition = new();
        unitGridPosition.Add(Player.CurrentGridPosition);

        return unitGridPosition;
    }

    public override AIActionData GetAIAction(GridPosition gridPosition)
    {

        return new AIActionData
        {
            gridPosition = gridPosition,
            actionValue = 0
        };
    }
}
