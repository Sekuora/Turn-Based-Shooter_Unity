// Copyright(c) 2025 Fyragic. All rights reserved.
using UnityEngine;

public class GridMoveRangeTest : MonoBehaviour
{
    [SerializeField] private Player player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            // Hide positions by default
            GridVFX.Instance.HideAllGridPositions();

            // Show only valid grid positions
            GridVFX.Instance.ShowGridPositions(player.MoveAction.CheckValidActionGrids(), GridVFX.GridTileType.White);

            
        }
    }
}
