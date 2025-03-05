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

    // Player Data
    // Move Speed
    [SerializeField] private float moveSpeed;

    // Rotation Speed
    [SerializeField] private float rotationSpeed;

    // Player Component References
    // Animator Component
    [SerializeField] private Animator animator;

    // Entity Action Components
    [SerializeField] private MoveAction moveSystem;

    [SerializeField] private TurnSystem turnSystem;

    // Actions
    [SerializeField] private SpinAction spinAction;

    [SerializeField] private ShootAction shootAction;


    // Action Points
    [SerializeField] private int energy_max = 2;

    [SerializeField] private int energy = 2;

    // Target to move to
    private Vector3 targetPosition;

    // Player's current grid position
    private GridPosition currentGridPosition;

    // Array of primal actions
    private PrimalAction[] primalActions;


    
    private void Awake()
    {
        // Don't move to default target
        targetPosition = transform.position;

        moveSystem = GetComponent<MoveAction>();

        spinAction = GetComponent<SpinAction>();

        shootAction = GetComponent<ShootAction>();

        turnSystem = UnitsActionSystem.Instance.TurnSystem;

        primalActions = GetComponents<PrimalAction>();
    }

    private void Start()
    {
        energy = energy_max;

        // Get grid position of the player's transform position
        currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        // Set reference to this unit as unit at the current grid position.
        LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);

        turnSystem.OnEndTurnButtonTriggered += OnEndTurnButtonTriggered_Event;
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

    private void UpdateGridPosition()
    {
        // Update grid position of this player
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != currentGridPosition)
        {
            // Call method to update unit's grid position
            LevelGrid.Instance.UnitMovedGridPosition(this, currentGridPosition, newGridPosition);
            currentGridPosition = newGridPosition;
        }
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

    private void OnEndTurnButtonTriggered_Event(object sender, EventArgs e)
    {
        energy = energy_max;
        OnActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }  

    // Getters / Setters - Properties
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    public Animator Animator { get => animator; set => animator = value; }

    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }

    public MoveAction MoveSystem { get => moveSystem; set => moveSystem = value; }

    public GridPosition CurrentGridPosition { get => currentGridPosition; set => currentGridPosition = value; }
    
    // Actions
    public SpinAction SpinAction { get => spinAction; set => spinAction = value; }
    public PrimalAction[] PrimalActions { get => primalActions; set => primalActions = value; }
    public int Energy { get => energy; set => energy = value; }
    public bool IsEnemy { get => isEnemy; set => isEnemy = value; }
}
