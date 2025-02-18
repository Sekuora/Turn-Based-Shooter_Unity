using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionButton : MonoBehaviour
{


    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [SerializeField]
    private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
    }


    public void SetAction(PrimalAction action)
    {
        // Set action text to ui button
        textMeshPro.text = action.ActionName.ToUpper();

        // Set button through lambda without parameters
        button.onClick.AddListener(() => { UnitsActionSystem.Instance.SetSelectedAction(action); });
    }

}
