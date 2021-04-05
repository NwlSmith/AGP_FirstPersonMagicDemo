using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType { Offensive, Defensive, Healing, Utility, Stealth }
public delegate void SpellUpdateMethod();


/*
 * All Spells have:
 * - A cast procedure
 * - A delivery
 * - An effect
 */
public class Spell
{

    public readonly string _name;

    public SpellType spellType;

    public float manaCost;

    protected Cast _cast;
    protected Delivery _delivery;
    protected Effect _effect;

    public readonly Entity _owningEntity;

    protected SpellUpdateMethod spellUpdateMethods;

    public Spell(Entity entity, string name)
    {
        _owningEntity = entity;
        _name = name;
    }

    /// <summary>
    /// Called when the button is first pressed.
    /// Sometimes this will be the start and end of the spell, sometimes this will begin charging and calling OnCastingOverTime.
    /// </summary>
    public void OnStartCasting()
    {
        _cast.OnStartCasting();
    }

    public void OnAbort()
    {
        Debug.Log($"Spell {_name} aborted.");
    }

    /// <summary>
    /// Cast has called for the delivery method to be employed.
    /// </summary>
    public void CastDelivery() => _delivery.Deliver();


    /// <summary>
    /// Called when a spell's effect is triggered.
    /// </summary>
    public void OnEffectTrigger()
    {
        DeductMana();
        _effect.OnEffectStart();
    }

    public void EffectDelivered(Entity entityToDeliverEffect) // ????? Should probably create a new effect?
    {
        _effect.OnEffectStart();
    }

    public void AddSpellUpdateMethod(SpellUpdateMethod updateMethod) => spellUpdateMethods += updateMethod;

    public void RemoveSpellUpdateMethod(SpellUpdateMethod updateMethod) => spellUpdateMethods -= updateMethod;

    public void Update() => spellUpdateMethods();

    public bool CheckMana => _owningEntity.EnoughManaForSpell(manaCost);

    public void DeductMana() => _owningEntity.ChangeMana(manaCost);

    public Vector3 EntityLocation => _owningEntity.Location;

    public Vector3 EntityDirection => _owningEntity.Direction;

}

/// <summary>
/// Takes in a csv file and creates spells with those attributes.
/// </summary>
public class SpellCreator
{



}
