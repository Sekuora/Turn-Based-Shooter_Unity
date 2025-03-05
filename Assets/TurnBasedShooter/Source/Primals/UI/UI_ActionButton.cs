using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionButton : MonoBehaviour
{


    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [SerializeField]
    private Button button;

    [SerializeField]
    private GameObject selectedPointer;

    // store currently selected action
    private PrimalAction currentAction;

    public void SetAction(PrimalAction action)
    {
        currentAction = action;

        // Set action text to ui button
        textMeshPro.text = action.ActionName.ToUpper();

        // Set button through lambda without parameters
        button.onClick.AddListener(() => { UnitsActionSystem.Instance.SetSelectedAction(action); });
    }

    public void UpdateSelectedImage()
    {
        // compare if selected action equals the ui button assgned action update selected image
        PrimalAction selectedAction = UnitsActionSystem.Instance.ActiveAction;
        selectedPointer.SetActive(selectedAction == currentAction);
    }

}
