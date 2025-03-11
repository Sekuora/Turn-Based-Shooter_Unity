// Copyright(c) 2025 Fyragic. All rights reserved.
using Unity.VisualScripting;
using UnityEngine;

public class RagdollEntity : MonoBehaviour
{
    [SerializeField]
    private Transform ragdollRootBone;

    [SerializeField]
    private float explosionForce = 300f;

    private float explosionForceIncrement = 150f;

    [SerializeField]
    private float explosionRange = 25f;

    public void Setup(Transform unitRootBone)
    {
        CloneUnitBones(unitRootBone, ragdollRootBone);

        AddImpulse(ragdollRootBone);
    }

    private void CloneUnitBones(Transform unitRootBone, Transform ragdollRootBone)
    {
        // Assign bones from unit to the ragdoll if their names match
        foreach (Transform child in unitRootBone)
        {
            // Get the position an rotation of the original bones into the ragdoll clone bones
            Transform cloneBone = ragdollRootBone.Find(child.name);
            if(cloneBone != null)
            {
                cloneBone.SetPositionAndRotation(child.position, child.rotation);

                // Recursively find nested children bones to clone
                CloneUnitBones(child, cloneBone);
            }
        }
    }

    private void AddImpulse(Transform unitRootBone)
    {
        foreach (Transform child in unitRootBone)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                // Random Explosion force
                float randomForce = Random.Range(explosionForce, explosionForce + explosionForceIncrement);

                childRigidbody.AddExplosionForce(randomForce, this.transform.position, explosionRange);
            }

            AddImpulse(child);
        }

      
    }

}
