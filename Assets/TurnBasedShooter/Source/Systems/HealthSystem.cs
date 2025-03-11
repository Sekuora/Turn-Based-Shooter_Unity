using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int health = 10;

    [SerializeField]
    private int healthMax = 10;

    [SerializeField]
    private int currentHealth;

    public int HealthMax { get => healthMax; set => healthMax = value; }
    public int Health { get => health; set => health = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    public event EventHandler NoHealth;
    public event EventHandler OnHealthDecreased;


    private void Awake()
    {
        health = healthMax;
    }


    public void DecreseHealth(int amount)
    {
        currentHealth = health;
        Debug.Log(currentHealth);

        health -= amount;

        Debug.Log(currentHealth);

        if (health < 0)
        {
            health = 0;
        }

        OnHealthDecreased?.Invoke(this, EventArgs.Empty);


        if (health == 0)
        {
            NoHealth?.Invoke(this, EventArgs.Empty);
        }

        Debug.Log(health);
    }

    public float GetHealthNormalized(int healthValue)
    {
        return (float)healthValue / (float)healthMax;
    }
}
