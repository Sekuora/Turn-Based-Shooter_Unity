using TMPro;
using UnityEngine;

public class GridDebugAgent : MonoBehaviour
{

    // TextMeshPro Reference
    [SerializeField] private TextMeshPro textMeshPro;

    private GridCell gridCell;

    public void SetGridCell(GridCell gridCell)
    {
        this.gridCell = gridCell;
    }

    // Update is called once per frame
    private void Update()
    {
        /* Get the grid position of the grid object, transform it to a string
         * Pass, x : z position from the grid position to string method. */
        textMeshPro.text = gridCell.ToString();
    }
}
