using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    [SerializeField] protected string _name = "Entity";
    [SerializeField] protected float _currentHealth, _maxHealth = 100f;
    [SerializeField] protected float _currentMana, _maxMana = 100f;

    protected List<Effect> currentEffects = new List<Effect>();

    protected GameObject _gameObject;

    public Entity(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public void Update()
    {
        foreach (Effect effect in currentEffects)
        {
            effect.Update();
        }
    }

    public void AddEffect(Effect effect) => currentEffects.Add(effect);
    public void RemoveEffect(Effect effect) => currentEffects.Remove(effect);


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

    /// <summary>
    /// Check if the Entity has enough mana for this spell.
    /// </summary>
    /// <param name="manaCost">The amount of mana for this spell.</param>
    /// <returns>True if there is enough mana. False otherwise.</returns>
    public bool EnoughManaForSpell(float manaCost)
    {
        return _currentMana - manaCost >= 0f;
    }

    /// <summary>
    /// Changes mana by change.
    /// </summary>
    /// <param name="change">The amount to be added or subtracted from the Entity's mana.</param>
    public void ChangeMana(float change)
    {
        _currentMana = Mathf.Clamp(_currentMana + change, 0, _maxMana);
    }

    public Vector3 Location => _gameObject.transform.position;

    public Vector3 Direction => _gameObject.transform.rotation * Vector3.forward;
}
