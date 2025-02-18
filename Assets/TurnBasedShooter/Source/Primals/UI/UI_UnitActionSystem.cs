using System;
using UnityEngine;

public class UI_UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Transform actionButton;
    [SerializeField] private Transform actionButtonsContainer;

    private void Start()
    {
        UnitsActionSystem.Instance.OnSelectedUnitChanged += Event_OnSelectedUnitChanged;
        CreateUnitActionButtons();
    }

    

    private void CreateUnitActionButtons()
    {
        Player playerUnit = UnitsActionSystem.Instance.GetActivePlayerUnit();

        // Destroy all buttons before updating them
        foreach (Transform button in actionButtonsContainer)
        {
            Destroy(button.gameObject);
        }

        // Iterate over actions
        foreach (PrimalAction action in playerUnit.PrimalActions)
        {
            // Instantiate buttons at action buttons container
            Transform actionButtonTransform = Instantiate(actionButton, actionButtonsContainer);

            // Get the Action Button Component and set it's action to the corresponding action
            UI_ActionButton actionButtonUI = actionButtonTransform.GetComponent<UI_ActionButton>();
            actionButtonUI.SetAction(action);

        }
    }

    private void Event_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        // Update action buttons when changing units
        CreateUnitActionButtons();
    }
}
