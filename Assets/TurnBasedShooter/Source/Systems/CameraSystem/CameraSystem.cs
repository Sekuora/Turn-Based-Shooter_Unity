using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraSystem : MonoBehaviour
{
    // Camera Movement Data
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float zoomSpeed = 2f;

    private Vector3 targetFollowOffset;

    /* 
     * Define constants with the game initials TBS
     * And then with default pascal case.
     */
    private const float TBS_maxZoomInValue = -5f;
    private const float TBS_maxZoomOutValue = 10f;

    // Camera Rig
    private CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        targetFollowOffset = orbitalFollow.TargetOffset;
    }

    // Update is called once per frame
    private void Update()
    {
        CameraMovement();

        CameraRotation();

        CameraZoom();
    }

    private void CameraZoom()
    {
        Vector3 zoomOffset = orbitalFollow.TargetOffset;

        float zoomIncrementAmount = 1f;

        // Zoom Logic
        // If scroll wheel is positive, zoom in.
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomIncrementAmount;
        }
        // Else zoom out.
        else if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomIncrementAmount;
        }

        // Clamp Zoom value to avoid clipping through floor or going too far up.
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, TBS_maxZoomInValue, TBS_maxZoomOutValue);

        // Save new clamped vector
        Vector3 clampedZoomOffset = new(0, zoomOffset.y, 0);

        // Interpolate current zoom value to target clamped zoom
        orbitalFollow.TargetOffset = Vector3.Lerp(orbitalFollow.TargetOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }

    private void CameraRotation()
    {
        Vector3 rotationVector = new(0, 0, 0);

        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }

        /*
        * Rotate in euler angles to align rotated view with movement
        */
        Vector3 rotationTransform = transform.eulerAngles += rotationSpeed * Time.deltaTime * rotationVector;
    }

    private void CameraMovement()
    {
        // Vector for movement input
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        // Camera movement input
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = 1f;
        }


        /* 
        * Calculate both directions according to the input move direction
        * This allows to base the movement according to the current camera rotation.
        * Set rotation to local in editor.
        */
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveSpeed * Time.deltaTime * moveVector.normalized;
    }
}
