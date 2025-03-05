// Copyright(c) 2025 Fyragic. All rights reserved.
using UnityEngine;

// Test code for grid system
public class GridTestSystem : MonoBehaviour
{
    private GridSystem gridSystem;
    [SerializeField] private Transform debugAgent;
    [SerializeField] private MouseRaycastSystem raycastSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugData(debugAgent);
      
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(raycastSystem.CollectRaycastHitPoint()));
    }
}
