// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using UnityEngine;

public class SelectedPointerVFX : PlayerExoskeleton
{

    // Mesh for selected unit pointer
    private MeshRenderer selectedUnitPointer;

    private void Awake()
    {
        
        selectedUnitPointer = GetComponent<MeshRenderer>();
        selectedUnitPointer.enabled = false;
    }

    protected override void Start()
    {
        base.Start();
        // Set up delegate for selected unit changed evet
        UnitsActionSystem.Instance.OnSelectedUnitChanged += UnitsActionSysten_OnSelectedUnitChanged;
        // Run Pointer Update on start
        UpdateSelectedPointer();
    }

    // Activate pointer only when unit change event is triggered
    private void UnitsActionSysten_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateSelectedPointer();
    }

    private void UpdateSelectedPointer()
    {
        /* We access to the UnitsActionSystem Instance to avoid referencing it in this class.
        * From it we get the active player and compare it to the pointer player.
        */
        if (UnitsActionSystem.Instance.GetActivePlayer() == Player)
        {
            selectedUnitPointer.enabled = true;

        }
        else
            selectedUnitPointer.enabled = false;
    }
}
