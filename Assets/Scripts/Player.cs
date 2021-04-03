using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    
    /// <summary>
    /// Player dies.
    /// </summary>
    protected override void Die()
    {
        Debug.Log("Player has died.");
    }

    /// <summary>
    /// Check if the Player has enough mana for this spell.
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
    /// <param name="change">The amount to be added or subtracted from the player's mana.</param>
    public void ChangeMana(float change)
    {
        _currentMana = Mathf.Clamp(_currentMana + change, 0, _maxMana);
    }
}
