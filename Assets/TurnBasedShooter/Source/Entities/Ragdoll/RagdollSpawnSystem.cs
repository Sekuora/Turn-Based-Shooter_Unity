// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using UnityEngine;

public class RagdollComponent : MonoBehaviour
{

    [SerializeField]
    private Transform ragdoll;

    [SerializeField]
    private Transform unitRootBone;

    private HealthSystem healthSystem;


    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.NoHealth += NoHealth_Event;
    }

    private void NoHealth_Event(object sender, EventArgs e)
    {
        Transform ragdolnstance = Instantiate(ragdoll, transform.position, transform.rotation);

        RagdollEntity ragdollEntity = ragdolnstance.GetComponent<RagdollEntity>();
        
        ragdollEntity.Setup(unitRootBone);

    }
}
