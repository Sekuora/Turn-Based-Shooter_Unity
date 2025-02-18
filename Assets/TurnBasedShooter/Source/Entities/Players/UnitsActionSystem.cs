// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using UnityEngine.EventSystems;
using UnityEngine;


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

    private bool IsReady;

    /* Event thrown when unit selection occurs
       Pass sender object, args for system events are left empty.*/
    public event EventHandler OnSelectedUnitChanged;

    // Component References
    [SerializeField] private Player activePlayerUnit;

    [SerializeField] private MouseRaycastSystem raycastSystem;

    [SerializeField] private LayerMask unitMask;

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
        if (!IsReady) { return; }

        // Return if mouse is over a button
        if(EventSystem.current.IsPointerOverGameObject()) { return; }


        // Move unit to raycast point

        if (WhenUnitSelected()) return;

        WhenActiveAction();

    }

    private void WhenActiveAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(raycastSystem.CollectRaycastHitPoint());

            switch (activeAction)
            {
                case MoveSystem moveSystem:
                    // Call MoveAction if grid is valid
                    if (moveSystem.IsValidActionGrid(mouseGridPosition))
                    {
                        SetNotReadyState();
                        moveSystem.SetTargetPosition(mouseGridPosition, SetReadyState);
                    }
                    break;

                case SpinAction spinAction:
                    SetNotReadyState();
                    spinAction.Spin(SetReadyState);
                    break;

            }
        }
    }


    // Set ready steate to true
    private void SetReadyState()
    {
        IsReady = true;
    }

    // Set ready steate to false
    private void SetNotReadyState()
    {
        IsReady = false;
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
    }

    /* Active player getter */
    public Player GetActivePlayerUnit()
    {
        return activePlayerUnit;
    }

    // Getters / Setters
    public PrimalAction ActiveAction { get => activeAction; set => activeAction = value; }

}
