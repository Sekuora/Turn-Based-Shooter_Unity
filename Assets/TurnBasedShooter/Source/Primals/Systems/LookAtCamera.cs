using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private bool invert;
    private Transform cameraTransform;

    private void Awake()
    {
        invert = true;
        cameraTransform = Camera.main.transform;

    }

    private void LateUpdate()
    {
        if (invert)
        {
            // Look at opposite direction of camera, useful to invert ui texts
            Vector3 directionToCamera = (cameraTransform.position - this.transform.position).normalized;
            transform.LookAt(transform.position + directionToCamera * -1);
        }
        else
        {
            // Look at camera direction
            transform.LookAt(cameraTransform);
        }
    }


}
