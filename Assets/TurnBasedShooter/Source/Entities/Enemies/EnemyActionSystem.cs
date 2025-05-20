// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using System.Threading;
using UnityEngine;

public class EnemyActionSystem : MonoBehaviour
{

    //public event EventHandler OnActionTriggered;

    [SerializeField]
    private UnitDataAsset unitDataAsset;

    [SerializeField]
    private UI_UnitActionSystem actionSystemUI;

    // States
    private enum State
    {
        WaitingForTurn,
        TakingTurn,
        Busy
    }

    private State state;

    // Turn Management
    TurnSystem turnSystem;

    private float turnTimer;

    private void Awake()
    {
        turnSystem = UnitsActionSystem.Instance.TurnSystem;

        state = State.WaitingForTurn;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        turnSystem.OnEndTurnButtonTriggered += OnEndTurnButtonTriggered_Event;

    }


    private void Update()
    {
        if (turnSystem.IsPlayerTurn)
        {
            return;
        }

        // States
        switch (state)
        {
            case State.WaitingForTurn:
                break;
            case State.TakingTurn:
                turnTimer -= Time.deltaTime;
                if (turnTimer <= 0f)
                {
                    if (CheckCanTakeAIAction(SetTakingTurnState))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        turnSystem.NextTurn();
                    }

                }
                break;
            case State.Busy:
                break;
        }


    }

    private void SetTakingTurnState()
    {
        turnTimer = 0.5f;
        state = State.TakingTurn;

    }


    // Event Bind Methods
    private void OnEndTurnButtonTriggered_Event(object sender, EventArgs e)
    {
        if (turnSystem.IsPlayerTurn) { return; };

        state = State.TakingTurn;
        turnTimer = 2f;
    }

    private bool CheckCanTakeAIAction(Action onAIActionComplete)
    {
        foreach (Player enemyUnit in unitDataAsset.EnemyUnits)
        {
            if (CheckCanTakeAIAction(enemyUnit, onAIActionComplete))
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckCanTakeAIAction(Player aiUnit, Action onAIActionComplete)
    {
        AIActionData currentBestAIAction = null;
        PrimalAction actionToCast = null;

        // Get Actions assigned to AI Unit
        foreach (PrimalAction action in aiUnit.PrimalActions)
        {
            // Not enough Action Points for Action
            if (!aiUnit.CheckEnoughActionPoints(action))
            {
                continue;
            }

            // Check Best Action Probability
            if (currentBestAIAction == null)
            {
                // for each action check the action with the highest probability
                currentBestAIAction = action.sortAIActionProbability();
                actionToCast = action;
            }
            // Check New Best Action Probability
            else
            {
                AIActionData newBestAIAction = action.sortAIActionProbability();

                if (newBestAIAction != null && newBestAIAction.actionValue > currentBestAIAction.actionValue)
                {
                    currentBestAIAction = newBestAIAction;
                    actionToCast = action;
                }
            }
        }

        // Perform Actions Casting
        if (currentBestAIAction != null && aiUnit.CheckEnoughActionPoints(actionToCast))
        {
            aiUnit.SpendActionPoints(actionToCast.ActionPointsCost);

            // Throw Action Triggered Event
            actionSystemUI.UpdateCurrentUnitEnergyPoints(aiUnit);

            switch (actionToCast)
            {
                case MoveAction moveAction:
                    moveAction.SetTargetPosition(currentBestAIAction.gridPosition, onAIActionComplete);
                    break;

                case SpinAction spinAction:
                    spinAction.Spin(onAIActionComplete);
                    break;

                case ShootAction shootAction:
                    // Set units action system current target thorugh shoot action's function return value
                    shootAction.Shoot(onAIActionComplete, currentBestAIAction.gridPosition);
                    break;
            }

            return true;
        }
        else
        {
            return false;
        }

    }
}
