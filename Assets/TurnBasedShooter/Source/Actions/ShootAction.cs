// Copyright(c) 2025 Fyragic. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootAction : PrimalAction
{

    private float totalSpinAmount;

    [SerializeField]
    private int maxShootDistance = 7;

    Player playerUnit;


    public override List<GridPosition> CheckValidActionGrids()
    {
        List<GridPosition> validGridPositions = new();

        // Define Grid Range for Action
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {

                GridPosition offsetGridPosition = new(x, z);
                GridPosition testGridPosition = Player.CurrentGridPosition + offsetGridPosition;


                // Check grid positions validity to perform actions
                if (!LevelGrid.Instance.IsGridPositionInRange(testGridPosition))
                {
                    // Check if grid position range valid
                    continue;

                }

                // Check if Grid Position is empty
                if (!LevelGrid.Instance.IsGridPositionFilled(testGridPosition))
                {
                  
                    continue;
                }

                Player targetUnit = LevelGrid.Instance.CollectPlayerUnitAtGridPosition(testGridPosition);
                
                // If units IsEnemy state is the same, then units are on the same time
                if(targetUnit.IsEnemy == Player.IsEnemy)
                {
                    // Pass execution
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if(testDistance > maxShootDistance)
                {
                    continue;
                }

                // Add the valid list positions after checking coditions
                validGridPositions.Add(testGridPosition);
                //Debug.Log(testGridPosition);
            }
        }


        return validGridPositions;
    }

    private void Awake()
    {
        ActionName = "Shoot";
        ActionPointsCost = 3;
    }

    // Update is called once per frame
    void Update()
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
            IsActive = false;
            // Call delegate, binded to UnitAction System ready state
            onActionComplete();
        }
    }


    public void Spin(Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        totalSpinAmount = 0f;
        IsActive = true;
    }
}
