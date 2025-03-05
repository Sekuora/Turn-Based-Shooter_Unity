using System;
using Unity.VisualScripting;
using UnityEngine;

public class UI_ReadyState : MonoBehaviour
{

    [SerializeField]
    private GameObject readyBanner;

    private void Awake()
    {
        readyBanner.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UnitsActionSystem.Instance.OnReadyStateChanged += Event_OnReadyStateChanged;
    }


    private void Event_OnReadyStateChanged(object sender, EventArgs e)
    {
        if (UnitsActionSystem.Instance.IsReady)
        {
            readyBanner.SetActive(false);
        }
        else
        {
            readyBanner.SetActive(true);
        }
    }

}
