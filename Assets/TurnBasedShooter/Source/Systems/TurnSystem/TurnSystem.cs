// Copyright(c) 2025 Fyragic. All rights reserved.
using UnityEngine;
using System;

public class TurnSystem : MonoBehaviour
{
    private int turnNumber = 1;

    private bool isPlayerTurn = true;

    public event EventHandler OnEndTurnButtonTriggered;

    public void NextTurn()
    {
        turnNumber++;

        isPlayerTurn = !isPlayerTurn;

        OnEndTurnButtonTriggered?.Invoke(this, EventArgs.Empty);
    }

    public int TurnNumber { get => turnNumber; set => turnNumber = value; }
    public bool IsPlayerTurn { get => isPlayerTurn; set => isPlayerTurn = value; }
}
