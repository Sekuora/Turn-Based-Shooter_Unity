// Copyright(c) 2025 Fyragic. All rights reserved.
using TMPro;
using UnityEngine;

public class GridDebugAgent : MonoBehaviour
{

    // TextMeshPro Reference
    [SerializeField] private TextMeshPro gridCoordinates;

    private object gridCell;

    public virtual void SetGridCell(object gridCell)
    {
        this.gridCell = gridCell;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        /* Get the grid position of the grid object, transform it to a string
         * Pass, x : z position from the grid position to string method. */
        gridCoordinates.text = gridCell.ToString();
    }
}
