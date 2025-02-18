// Copyright(c) 2025 Fyragic. All rights reserved.
using NUnit.Framework;
using TMPro;

using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : PrimalAction
{

    [SerializeField]
    private int maxMoveDistance = 4;

    private void Awake()
    {
        ActionName = "Move";
    }

    private void Update()
    {
        MoveToTarget();
    }

    /**
    *@brief Moves to a given target.
    * Used to move player to a target such as a mouse press or touch input.
    *
    */
    public void MoveToTarget()
    {
        // Stopping value to avoid rounding problems
        float epsilonStopValue = 0.05f;

        float distanceToTarget = Vector3.Distance(Player.transform.position, Player.TargetPosition);

        //Debug.Log(distanceToTarget);

        if (!IsActive)
        {
            return;
        }
        // Transform Position
        Vector3 moveDirection = (Player.TargetPosition - Player.transform.position).normalized;

        // Moves while distance to target greater than stopping value.
        if (distanceToTarget > epsilonStopValue)
        {
            Player.transform.position += Player.MoveSpeed * Time.deltaTime * moveDirection;

           
            // set animation waling
            Player.Animator.SetBool("IsWalking", true);

        }
        else
        {
            // set animation idle
            Player.Animator.SetBool("IsWalking", false);
            IsActive = false;
            onActionComplete();
        }

        // Transform Rotation
        Player.transform.forward = Vector3.Lerp(Player.transform.forward, moveDirection * Player.RotationSpeed, Time.deltaTime);

    }

    // Go to target: In this case mouse inputs or other peripherals define the target.
    public void SetTargetPosition(GridPosition targetPosition, Action onActionComplete)
    {
        // Pass the onActionComplete delegate state
        this.onActionComplete = onActionComplete;

        // Get the target world position
        Player.TargetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
        IsActive = true;
    }


    // Check Grid Positions List calidity to perform actions
    public override List<GridPosition> CheckValidActionGrids()
    {
        List<GridPosition> validGridPositions = new();

        

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {

                GridPosition offsetGridPosition = new(x, z);
                GridPosition testGridPosition = Player.CurrentGridPosition + offsetGridPosition;

               
                // Check grid positions validity to perform actions
                if(!LevelGrid.Instance.IsGridPositionInRange(testGridPosition))
                {
                    // Check if grid position range valid
                    continue;

                }

                if(Player.CurrentGridPosition == testGridPosition)
                {
                    // Ignore same grid position as the unit
                    continue;
                }
                if(LevelGrid.Instance.IsGridPositionFilled(testGridPosition))
                {
                    // Grid Positions aleady have another unit
                    continue;
                }

                // Add the valid list positions after checking coditions
                validGridPositions.Add(testGridPosition);
                //Debug.Log(testGridPosition);
            }
        }
       

        return validGridPositions;
    }


 

}
