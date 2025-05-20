// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

/**
 * @brief Player Fucntionality
 * 
 * Defines the basic player functionality.
 */
public class Player : MonoBehaviour
{
    // Metadata
    [SerializeField] bool isEnemy;

    public static event EventHandler OnActionPointsChanged;

    public static event EventHandler OnUnitsSpawned;

    public static event EventHandler OnUnitsDead;

    // Player Data
    // Move Speed
    [SerializeField] private float moveSpeed;

    // Rotation Speed
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float height = 1.5f;

    // Player Component References
    // Animator Component
    [SerializeField] private Animator animator;

    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private TurnSystem turnSystem;

    // Action Points
    [SerializeField] private int energyMax = 2;

    [SerializeField] private int energy = 2;

    // Target to move to
    private Vector3 targetPosition;

    // Player's current grid position
    private GridPosition currentGridPosition;

    // Array of primal actions
    private PrimalAction[] primalActions;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        // Don't move to default target
        targetPosition = transform.position;

        turnSystem = UnitsActionSystem.Instance.TurnSystem;

        primalActions = GetComponents<PrimalAction>();
    }

    private void Start()
    {
        energy = energyMax;

        // Get grid position of the player's transform position
        currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        // Set reference to this unit as unit at the current grid position.
        LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);

        turnSystem.OnEndTurnButtonTriggered += OnEndTurnButtonTriggered_Event;

        healthSystem.NoHealth += NoHealth_Event;

        OnUnitsSpawned?.Invoke(this, EventArgs.Empty);
    }

    // Generic Gettter for Primal Actions
    public T GetAction<T>() where T : PrimalAction
    {
        foreach(PrimalAction action in primalActions)
        {
            if(action is T)
            {
                return (T)action;
            }
        }

        return null;
    }


    private void NoHealth_Event(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(currentGridPosition, this);
        UnitsActionSystem.Instance.LastTargetPosition = transform.position;
        Destroy(gameObject);

        OnUnitsDead?.Invoke(this, EventArgs.Empty);
    }

    /**
     *@brief Update Method of the PlayerSystem
     *
     */
    private void Update()
    {
        //moveSystem.MoveToTarget();

        UpdateGridPosition();
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;   
    }

    private void UpdateGridPosition()
    {
        // Update grid position of this player
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != currentGridPosition)
        {
            // Call method to update unit's grid
            GridPosition lastGridPosition = currentGridPosition;
            currentGridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovedGridPosition(this, lastGridPosition, newGridPosition);
            
        }
    }

    public void Damage(int damageAmount)
    {
        healthSystem.DecreseHealth(damageAmount);
        Debug.Log(transform + "took damage!");
    }

    public bool CheckEnoughActionPoints(PrimalAction action)
    {
        return energy >= action.ActionPointsCost;

    }

    public void SpendActionPoints(int amount)
    {
        energy -= amount;
        OnActionPointsChanged?.Invoke(this, EventArgs.Empty);
        
    }

    public float GetEnergyNormalized()
    {
        return (float)energy / (float)energyMax;
    }

    private void OnEndTurnButtonTriggered_Event(object sender, EventArgs e)
    {
        energy = energyMax;
        OnActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }  

    // Getters / Setters - Properties
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    public Animator Animator { get => animator; set => animator = value; }

    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }

    public GridPosition CurrentGridPosition { get => currentGridPosition; set => currentGridPosition = value; }

    public PrimalAction[] PrimalActions { get => primalActions; set => primalActions = value; }

    public int Energy { get => energy; set => energy = value; }
    public bool IsEnemy { get => isEnemy; set => isEnemy = value; }

    public float Height { get => height; set => height = value; }
    public int EnergyMax { get => energyMax; set => energyMax = value; }
    public HealthSystem HealthSystem { get => healthSystem; set => healthSystem = value; }
}
