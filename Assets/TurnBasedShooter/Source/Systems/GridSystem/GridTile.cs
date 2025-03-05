// Copyright(c) 2025 Fyragic. All rights reserved.
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        meshRenderer.enabled = true;
    }

    public void Hide()
    {

        meshRenderer.enabled = false;
    }
}
