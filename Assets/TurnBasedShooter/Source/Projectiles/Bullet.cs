using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer bulletTrail;

    [SerializeField]
    private Transform bulletHitFX;

    [SerializeField]
    private float moveSpeed = 200f;

    private Vector3 targetPosition;

    // Update is called once per frame
    private void Update()
    {
        Player targetUnit = UnitsActionSystem.Instance.CurrentTargetUnit;
        if (!targetUnit)
        {
            targetPosition = UnitsActionSystem.Instance.LastTargetPosition;
        }
        else
        {
            targetPosition = targetUnit.GetWorldPosition();
        }

        // Offset y to target height
        Vector3 fixedTargetPosition = new(targetPosition.x, targetPosition.y + UnitsActionSystem.Instance.CurrentTargetUnit.Height, targetPosition.z);

        // Trace vector from this bullet position to current target unit
        Vector3 moveDirection = (fixedTargetPosition - transform.localPosition).normalized;

        float initialDistanceToTarget = Vector3.Distance(transform.localPosition, targetPosition);

        transform.localPosition += moveSpeed * Time.deltaTime * moveDirection;

        float currentDistanceToTarget = Vector3.Distance(transform.localPosition, targetPosition);

        // If bullet surpassess initial distance to target, it has reached the target
        if(initialDistanceToTarget < currentDistanceToTarget)
        {
            transform.position = fixedTargetPosition;

            // Unparent trail from bullet
            bulletTrail.transform.parent = null;


            // Destroy this game object
            Destroy(gameObject);


            Instantiate(bulletHitFX, fixedTargetPosition, Quaternion.identity);
        }
    }
}
