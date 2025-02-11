// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

/**
 * @brief Player Fucntionality
 * 
 * Defines the basic player functionality.
 */
public class PlayerMovementSystem : MonoBehaviour
{
    // Player Data
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    // Player Component References
    [SerializeField] private Animator animator;

    // Target to move to
    private Vector3 targetPosition;

    private void Awake()
    {
        // Don't move to default target
        targetPosition = transform.position;
    }

    private void Start()
    {
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(this.transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(gridPosition, this);
    }

    /**
     *@brief Update Method of the PlayerSystem
     *
     */
    private void Update()
    {

        MoveToTarget();

    }

    /**
     *@brief Moves to a given target.
     * Used to move player to a target such as a mouse press or touch input.
     *
     */
    private void MoveToTarget()
    {
        // Stopping value to avoid rounding problems
        float epsilonStopValue = 0.01f;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        //Debug.Log(distanceToTarget);

        // Moves while distance to target greater than stopping value.
        if (distanceToTarget > epsilonStopValue)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            transform.position += moveSpeed * Time.deltaTime * moveDirection;

            // Look towards target position with linear interpolation
            transform.forward = Vector3.Lerp(transform.forward, moveDirection * rotationSpeed, Time.deltaTime);

            // set animation waling
            animator.SetBool("IsWalking", true);
        }
        else
        {   
            // set animation idle
            animator.SetBool("IsWalking", false);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
