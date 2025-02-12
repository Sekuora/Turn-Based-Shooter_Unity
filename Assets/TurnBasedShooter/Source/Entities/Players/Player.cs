// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

/**
 * @brief Player Fucntionality
 * 
 * Defines the basic player functionality.
 */
public class Player : MonoBehaviour
{
    // Player Data
    // Move Speed
    [SerializeField] private float moveSpeed;

    // Rotation Speed
    [SerializeField] private float rotationSpeed;

    // Player Component References
    // Animator Component
    [SerializeField] private Animator animator;

    // Target to move to
    private Vector3 targetPosition;
   
    // Entity Referenced Components
    private GridPosition currentGridPosition;

    // Entity Action Components
    [SerializeField] private MoveSystem moveSystem;


    private void Awake()
    {
        // Don't move to default target
        targetPosition = transform.position;

        moveSystem = GetComponent<MoveSystem>();
    }

    private void Start()
    {

        // Get grid position of the player's transform position
        currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        // Set reference to this unit as unit at the current grid position.
        LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);
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



    // Getters / Setters - Properties
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    public Animator Animator { get => animator; set => animator = value; }

    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }

    public MoveSystem MoveSystem { get => moveSystem; set => moveSystem = value; }

    public GridPosition CurrentGridPosition { get => currentGridPosition; set => currentGridPosition = value; }
}
