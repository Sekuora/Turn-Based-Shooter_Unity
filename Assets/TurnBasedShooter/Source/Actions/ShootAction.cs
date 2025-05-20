// Copyright(c) 2025 Fyragic. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootAction : PrimalAction
{
    public EventHandler OnShoot;
    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }

    private State state;

    private float stateTimer;

    private bool canShoot;
    

    [SerializeField]
    private int maxShootDistance = 7;

    private Player playerUnit;

    private Player targetUnit;

    public Player TargetUnit { get => targetUnit; set => targetUnit = value; }
    public int MaxShootDistance { get => maxShootDistance; set => maxShootDistance = value; }


    public override List<GridPosition> CheckValidActionGrids()
    {
        return CheckValidActionGrids(Player.CurrentGridPosition);
    }

    public List<GridPosition> CheckValidActionGrids(GridPosition currentGridPosition)
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
        ActionPointsCost = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActive) { return; }
        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case State.Aiming:

                Vector3 aimDirection = (targetUnit.GetWorldPosition() - Player.GetWorldPosition()).normalized;

                Player.transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * Player.RotationSpeed);

                break;
            case State.Shooting:
                if(canShoot)
                {
                    PerformShootAction();
                    canShoot = false;
                }
                break;
            case State.Cooloff: 
                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }

    }

    private void PerformShootAction()
    {
        OnShoot?.Invoke(this, EventArgs.Empty);
        targetUnit.Damage(damageAmount);
    }

    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                state = State.Cooloff;
                float coolOffStateTime = 0.5f;
                stateTimer = coolOffStateTime;
                break;
            case State.Cooloff:

                ActionComplete();

                break;
        }

        //Debug.Log(state);
    }

    public Player Shoot(Action onActionComplete, GridPosition actionGridPosition)
    {
        targetUnit = LevelGrid.Instance.CollectPlayerUnitAtGridPosition(actionGridPosition);

        Debug.Log(targetUnit);

        Debug.Log("Aiming");

        state = State.Aiming;

        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canShoot = true;

        ActionStart(onActionComplete);

        return targetUnit;
    }

    public override AIActionData GetAIAction(GridPosition gridPosition)
    {
        return new AIActionData
        {
            gridPosition = gridPosition,
            actionValue = 100
        }; 
    }

    public int FetchTargetsAtPosition(GridPosition inGridPosition)
    {

        return CheckValidActionGrids(inGridPosition).Count;
    }
}
