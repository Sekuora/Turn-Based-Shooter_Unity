// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using UnityEngine;

public class EnemyActionSystem : MonoBehaviour
{
    [SerializeField] TurnSystem turnSystem;


    private float turnTimer;

    private void Awake()
    {
        turnSystem = UnitsActionSystem.Instance.TurnSystem;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        turnSystem.OnEndTurnButtonTriggered += OnEndTurnButtonTriggered_Event;
    }

    private void OnEndTurnButtonTriggered_Event(object sender, EventArgs e)
    {
        turnTimer = 2f;
    }

    private void Update()
    {
        if(turnSystem.IsPlayerTurn)
        {
            return;
        }

        turnTimer -= Time.deltaTime;
        if(turnTimer <= 0f)
        {
            turnSystem.NextTurn();
        }
    }

}
