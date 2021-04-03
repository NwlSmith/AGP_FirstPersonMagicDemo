using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    [SerializeField] protected string _name = "Entity";
    [SerializeField] protected float _currentHealth, _maxHealth = 100f;
    [SerializeField] protected float _currentMana, _maxMana = 100f;

    /// <summary>
    /// Changes health by change.
    /// If health is lower than 0, the Entity dies.
    /// </summary>
    /// <param name="change">The amount to be added or subtracted from the Entity's health.</param>
    public void ChangeHealth(float change)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + change, 0, _maxHealth);
        if (_currentHealth <= 0f)
            Die();
    }

    /// <summary>
    /// Entity dies.
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log($"Entity {_name} has died.");
    }
}
