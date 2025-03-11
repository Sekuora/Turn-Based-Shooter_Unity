// Copyright(c) 2025 Fyragic. All rights reserved.
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class UI_UnitData : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private float barFillSpeed;

    // Mana
    [SerializeField] private TextMeshProUGUI energyPointsText;

    [SerializeField] private Image energyBarImage;

    // Health
    [SerializeField] private Image healthBarImage;

    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private HealthSystem healthSystem;

 

    private void Awake()
    {
        //UpdateEnergyPointsText();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Player.OnActionPointsChanged += OnActionPointsChanged_Event;
        healthSystem.OnHealthDecreased += OnHealthDecreased_Event;

        UpdateEnergyPointsText();
    }

    private void OnHealthDecreased_Event(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void OnActionPointsChanged_Event(object sender, EventArgs e)
    {
        UpdateEnergyPointsText();
    }

    private void UpdateEnergyPointsText()
    {
        energyPointsText.text = player.Energy + "/" + player.EnergyMax.ToString();

        energyBarImage.fillAmount = player.GetEnergyNormalized();
    }

    private void UpdateHealthBar()
    {
        healthText.text = healthSystem.Health.ToString() + "/" + healthSystem.HealthMax.ToString();

        healthBarImage.fillAmount = healthSystem.GetHealthNormalized(healthSystem.Health);

    }

}
