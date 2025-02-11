using UnityEditor.SearchService;
using UnityEngine;

public class MouseRaycastSystem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    // Layer to use for raycast plane checking
    [SerializeField] private LayerMask raycastPlaneLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
      
    }

    // Update is called once per frame
    private void Update()
    {
        
        transform.position = CollectRaycastHitPoint();
    }

    /**
     * @brief Performs the casting and collects the raycastHit.point
     * 
     * The raycastHìt.point is the vector 3 location where the raycast has collided.
     */
    public Vector3 CollectRaycastHitPoint()
    {
        // Create a Ray from the main camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Define Raycast Parameters
        // Get the enum object for raycast and define its range as float and layers where it can be casted.
        Physics.Raycast(ray, out RaycastHit raycastHit, 1500f, raycastPlaneLayer);

        // Return raycast hit world position
        return raycastHit.point;
    }

}
