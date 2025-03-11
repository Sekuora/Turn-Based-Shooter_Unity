// Copyright(c) 2025 Fyragic. All rights reserved.
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void Show(Material material)
    {
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }


    public void Hide()
    {

        meshRenderer.enabled = false;
    }
}
