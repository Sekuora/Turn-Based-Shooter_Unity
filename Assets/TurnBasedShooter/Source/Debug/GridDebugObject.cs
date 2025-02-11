using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{

    // TextMeshPro Reference
    [SerializeField] private TextMeshPro textMeshPro;

    private GridObject gridObject;

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    // Update is called once per frame
    void Update()
    {
        /* Get the grid position of the grid object, transform it to a string
         * Pass, x : z position from the grid position to string method. */
        textMeshPro.text = gridObject.ToString();
    }
}
