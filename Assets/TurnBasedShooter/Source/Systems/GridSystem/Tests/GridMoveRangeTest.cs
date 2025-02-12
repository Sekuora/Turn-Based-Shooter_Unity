using UnityEngine;

public class GridMoveRangeTest : MonoBehaviour
{
    [SerializeField] private Player player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            player.MoveSystem.CheckValidActionGrids();
        }
    }
}
