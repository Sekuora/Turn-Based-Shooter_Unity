using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_UnitActionSystem : MonoBehaviour
{
    // Action Button transforms
    [SerializeField] private Transform actionButton;
    [SerializeField] private Transform actionButtonsContainer;

    [SerializeField] private TextMeshProUGUI EnergyCostText;

    [SerializeField] private TurnSystem turnSystem;

    // List to store action buttons
    private List<UI_ActionButton> actionButtons;


    private void Awake()
    {
        actionButtons = new List<UI_ActionButton>();
    }


    private void Start()
    {
        // Subscribe to unit changed event
        UnitsActionSystem.Instance.OnSelectedUnitChanged += Event_OnSelectedUnitChanged;

        UnitsActionSystem.Instance.OnSelectedActionChanged += Event_OnSelecteActionChanged;

        UnitsActionSystem.Instance.OnActionTriggered += Event_OnActionTriggered;

        turnSystem = UnitsActionSystem.Instance.TurnSystem;

        turnSystem.OnEndTurnButtonTriggered += OnEndTurnButtonTriggered_Event;

        Player.OnActionPointsChanged += OnActionPointsChanged_Event;

        // Create action buttons dinamically
        CreateUnitActionButtons();

        // Update selected action pointer
        UpdateSelectedPointer();

        // Selected Player Unit Energy Points
        UpdateCurrentUnitEnergyPoints();
    }

    private void OnActionPointsChanged_Event(object sender, EventArgs e)
    {
        EnergyCostText.text = "Energy: " + UnitsActionSystem.Instance.GetActivePlayerUnit().Energy;
    }

    private void OnEndTurnButtonTriggered_Event(object sender, EventArgs e)
    {
        UnitsActionSystem.Instance.CurrentTargetUnit = null;
        
        EnergyCostText.text = "Energy: " + UnitsActionSystem.Instance.GetActivePlayerUnit().Energy;

   
    }


    private void CreateUnitActionButtons()
    {
        Player playerUnit = UnitsActionSystem.Instance.GetActivePlayerUnit();

        // Destroy all buttons before updating them
        foreach (Transform button in actionButtonsContainer)
        {
            Destroy(button.gameObject);
        }

        // Clear Action Buttons List
        actionButtons.Clear();

        // Iterate over actions
        foreach (PrimalAction action in playerUnit.PrimalActions)
        {
            // Instantiate buttons at action buttons container
            Transform actionButtonTransform = Instantiate(actionButton, actionButtonsContainer);

            // Get the Action Button Component and set it's action to the corresponding action
            UI_ActionButton actionButtonUI = actionButtonTransform.GetComponent<UI_ActionButton>();
            actionButtonUI.SetAction(action);

            // Add action buttons to list
            actionButtons.Add(actionButtonUI);
        }
    }

    // Manage Selected Unit Changed Event
    private void Event_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();

        UpdateSelectedPointer();

        UpdateCurrentUnitEnergyPoints();
    }

    // Manage Selected Action Changed Event
    private void Event_OnSelecteActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedPointer();
    }

    private void Event_OnActionTriggered(object sender, EventArgs e)
    {
        UpdateCurrentUnitEnergyPoints();
    }

    // Update the selected pointer vfx (border to indicate selected image)
    private void UpdateSelectedPointer()
    {
        foreach (UI_ActionButton actionButtonUI in actionButtons)
        {
            actionButtonUI.UpdateSelectedImage();
        }
    }

    public void UpdateCurrentUnitEnergyPoints()
    {
        EnergyCostText.text = "Energy: " + UnitsActionSystem.Instance.GetActivePlayerUnit().Energy;
    }

    public void UpdateCurrentUnitEnergyPoints(Player player)
    {
        EnergyCostText.text = "Energy: " + player.Energy;
    }
}
