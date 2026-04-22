using UnityEngine;
using System;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    // Allow other scripts a readonly property to access current health
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }


    public void ChangeHealth(int amount)
    {
        int oldHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Fire off health change event
        Actions.OnHealthChanged?.Invoke(this, oldHealth, currentHealth);
    }


    public float GetHealthNormalized()
    {
        return (float)currentHealth / maxHealth;
    }


}
