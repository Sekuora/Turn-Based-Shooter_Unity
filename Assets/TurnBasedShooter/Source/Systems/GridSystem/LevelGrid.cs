using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    // Level Grid Instance
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private Transform gridDebugObject;
    private GridSystem gridSystem;

    private void Awake()
    {
        // Set Instance
        if (Instance != null)
        {
            Debug.LogWarning("Warning: Instance of: " + Instance + "already exists");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Create Grid System
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObject);
    }

    // Get Grid Object and sets a unit in it.
    public void SetUnitAtGridPosition(GridPosition gridPosition, PlayerMovementSystem playerUnit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.SetPlayerUnit(playerUnit);
    }

    public PlayerMovementSystem GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetPlayerUnit();
    }

    public void ClearUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.SetPlayerUnit(null);
    }

    // Get Grid Position
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

}
