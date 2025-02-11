using UnityEngine;

public class TestSystem : MonoBehaviour
{
    private GridSystem gridSystem;
    [SerializeField] private Transform debugPrebaf;

    [SerializeField] private MouseRaycastSystem raycastSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(debugPrebaf);
      
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(raycastSystem.CollectRaycastHitPoint()));
    }
}
