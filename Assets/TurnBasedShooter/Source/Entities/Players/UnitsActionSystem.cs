// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
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


    /* Event thrown when unit selection occurs
       Pass sender object, args for system events are left empty.*/
    public event EventHandler OnSelectedUnitChanged;

    // Component References
    [SerializeField] private PlayerMovementSystem activePlayer;

    [SerializeField] private MouseRaycastSystem raycastSystem;

    [SerializeField] private LayerMask unitMask;


    private void Awake()
    {
        if (!Instance) 
        Instance = this;
        else
        {
            Debug.LogWarning("Warning: Instance of: " + Instance +  "already exists");
            Destroy(gameObject);
        }

        unitMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        // Move unit to raycast point
        if (Input.GetMouseButtonDown(0))

        {
            if (WhenUnitSelected()) return;
            activePlayer.SetTargetPosition(raycastSystem.CollectRaycastHitPoint());

        }
    }

    private bool WhenUnitSelected()
    {
        // Cast a ray from camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000f, unitMask))
        {
            // When raycast tocuhes upon a unit's movement component, out / return said player movemnt system reference.
            if (raycastHit.transform.TryGetComponent<PlayerMovementSystem>(out PlayerMovementSystem player))
            {
                // Assign player reference gathered from raycastHit as the active player. 
                SetSelectedUnit(player);

                return true;
            }

        }

        // return false until conditions meet.
        return false;
    }

    /*
     * Perform unit selection change and throw corresponding event.
     */
    private void SetSelectedUnit(PlayerMovementSystem player)
    {
        activePlayer = player;
        Debug.Log("Switching player");
        // If events exist trigger event.
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        
    }

    /* Active player getter */
    public PlayerMovementSystem GetPlayer()
    {
        return activePlayer;
    }
}
