using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataAsset : MonoBehaviour
{
    private List<Player> allUnits;
    private List<Player> playerUnits;
    private List<Player> enemyUnits;

    public List<Player> AllUnits { get => allUnits; set => allUnits = value; }
    public List<Player> PlayerUnits { get => playerUnits; set => playerUnits = value; }
    public List<Player> EnemyUnits { get => enemyUnits; set => enemyUnits = value; }

    private void Awake()
    {
        allUnits = new List<Player>();
        playerUnits = new List<Player>();
        enemyUnits = new List<Player>();
    }

    private void Start()
    {
        Player.OnUnitsSpawned += OnUnitsSpawned_Event;
        Player.OnUnitsDead += OnUnitsDead_Event;
    }

    private void OnUnitsSpawned_Event(object sender, EventArgs e)
    {

        Player unit = sender as Player;

        allUnits.Add(unit);

        if(unit.IsEnemy)
        {
            enemyUnits.Add(unit);
        }
        else
        {
            playerUnits.Add(unit);
        }
    }

    private void OnUnitsDead_Event(object sender, EventArgs e)
    {
        Player unit = sender as Player;

        allUnits.Remove(unit);

        if (unit.IsEnemy)
        {
            enemyUnits.Remove(unit);
        }
        else
        {
            playerUnits.Remove(unit);
        }
    }

}


