// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Buffers;


/*
 * Manage action for multiple units.
 * 
 * Utilized for unit selection and unit actions.
 * @author Fyragic
 */
public class UnitsActionSystem : MonoBehaviour
{
    // Instance of this class
    public static UnitsActionSystem Instance { get; private set; }

    private bool isReady;

    /* Event thrown when unit selection occurs
       Pass sender object, args for system events are left empty.*/
    public event EventHandler OnSelectedUnitChanged;

    // Event for when currently active action changes
    public event EventHandler OnSelectedActionChanged;

    // Event for when currently ready state changes
    public event EventHandler OnReadyStateChanged;

    public event EventHandler OnActionTriggered;

    // Component References
    [SerializeField] private Player activePlayerUnit;

    [SerializeField] private MouseRaycastSystem raycastSystem;

    [SerializeField] private LayerMask unitMask;

    [SerializeField] private TurnSystem turnSystem;

    private PrimalAction activeAction;


    private void Awake()
    {
        // Define instance of this class
        if (!Instance)
            Instance = this;
        else
        {
            Debug.LogWarning("Warning: Instance of: " + Instance + "already exists");
            Destroy(gameObject);
        }

        unitMask = LayerMask.GetMask("Player");
    }

    private void Start()
    {
        SetReadyState();
        SetSelectedUnit(activePlayerUnit);
    }

    // Update is called once per frame
    private void Update()
    {
        // Return if action state is busy
        if (!isReady) { return; }

        if (!turnSystem.IsPlayerTurn) { return; }

        // Return if mouse is over a button
        if(EventSystem.current.IsPointerOverGameObject()) { return; }


        // Move unit to raycast point

        if (WhenUnitSelected()) return;

        WhenActiveAction();

    }

    // Define what happens when an action is active
    private void WhenActiveAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(raycastSystem.CollectRaycastHitPoint());

            // Return if raycast is not on a valid action grid.
            if (!activeAction.IsValidActionGrid(mouseGridPosition)) { return; }

            // Check if active unit has enough points to cast action
            if (activePlayerUnit.CheckEnoughActionPoints(activeAction))
            {
                // Spend points to cast action
                activePlayerUnit.SpendActionPoints(activeAction.ActionPointsCost);

                // Throw Action Triggered Event
                OnActionTriggered?.Invoke(this, EventArgs.Empty);


                switch (activeAction)
                {
                    case MoveAction moveAction:
                        // Call MoveAction if grid is valid
                        if (moveAction.IsValidActionGrid(mouseGridPosition))
                        {
                            // Perform action
                            SetNotReadyState();
                            moveAction.SetTargetPosition(mouseGridPosition, SetReadyState);

                        }
                        break;

                    case SpinAction spinAction:
                        SetNotReadyState();
                        spinAction.Spin(SetReadyState);
                        break;

                    case ShootAction shootAction:
                        SetNotReadyState();
                        shootAction.Spin(SetReadyState);
                        break;


                }
            }
        }
    }


    // Set ready steate to true
    private void SetReadyState()
    {
        isReady = true;
        OnReadyStateChanged?.Invoke(this, EventArgs.Empty);
    }

    // Set ready steate to false
    private void SetNotReadyState()
    {
        isReady = false;
        OnReadyStateChanged?.Invoke(this, EventArgs.Empty);
    }

    // Return true if a player unit is selected
    private bool WhenUnitSelected()
    {

        if (Input.GetMouseButtonDown(0))
        {

            // Cast a ray from camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000f, unitMask))
            {
                // When raycast tocuhes upon a unit's movement component, out / return said player movemnt system reference.
                if (raycastHit.transform.TryGetComponent<Player>(out Player player))
                {
                    // If player has the unit already selected return false
                    if(player == activePlayerUnit)
                    {
                        return false;
                    }

                    // If unit is an enemy don't select it
                    if (player.IsEnemy)
                    {   
                        return false;
                    }

                    // Assign player reference gathered from raycastHit as the active player. 
                    SetSelectedUnit(player);

                    return true;
                }
            }

        }

        // return false until conditions meet.
        return false;
    }

    /*
     * Perform unit selection change and throw corresponding event.
     */
    private void SetSelectedUnit(Player player)
    {
        // Set current selected unit
        activePlayerUnit = player;

        // Set active action to player's movement system /action
        SetSelectedAction(player.MoveSystem);

        Debug.Log("Switching player");

        // If events exist trigger event.
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);

    }

    public void SetSelectedAction(PrimalAction action)
    {
        activeAction = action;

        // Throw selected action changed event
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    /* Active player getter */
    public Player GetActivePlayerUnit()
    {
        return activePlayerUnit;
    }

    // Getters / Setters
    public PrimalAction ActiveAction { get => activeAction; set => activeAction = value; }

    public bool IsReady { get => isReady; }
    public TurnSystem TurnSystem { get => turnSystem; set => turnSystem = value; }
}
