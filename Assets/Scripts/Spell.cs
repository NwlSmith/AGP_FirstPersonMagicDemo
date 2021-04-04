using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType { Offensive, Defensive, Healing, Utility, Stealth }

/*
 * All Spells have:
 * - A cast procedure
 * - A delivery
 * - An effect
 */
public abstract class Spell
{
    public SpellType spellType;

    public float manaCost;

    protected Cast _cast;
    protected Delivery _delivery;
    protected Effect _effect;

    /// <summary>
    /// Called when the button is first pressed.
    /// Sometimes this will be the start and end of the spell, sometimes this will begin charging and calling OnCastingOverTime.
    /// </summary>
    public abstract void OnStartCasting();

    public abstract void OnAbort();

    /// <summary>
    /// Called while a projectile spell is traveling.
    /// </summary>
    public abstract void OnTraveling();

    /// <summary>
    /// Called when a spell's projectile collides with something.
    /// </summary>
    public abstract void OnCollision();

    /// <summary>
    /// Called when a spell's effect is triggered.
    /// </summary>
    public void OnEffectTrigger() => _effect.OnEffectStart();

    public abstract void Update();

}

public abstract class InstantCastSpell : Spell
{
    /// <summary>
    /// Called when the button is first pressed.
    /// Sometimes this will be the start and end of the spell, sometimes this will begin charging and calling OnCastingOverTime.
    /// </summary>
    public override void OnStartCasting() => OnEffectTrigger();

    public override void Update()
    {
        return;
    }
}

public class HealSelfInstantSpell : InstantCastSpell
{

    private HealSelfInstantEffect _effect;

    public HealSelfInstantSpell()
    {
        _effect = new HealSelfInstantEffect(10, Services.Player);
    }

}

public abstract class SpellOverTime : Spell
{

    /// <summary>
    /// Called when button is pressed continuously.
    /// Ongoing mana drain/casting animations.
    /// </summary>
    public abstract void OnCastingOverTime();

    /// <summary>
    /// Called when button is released.
    /// Triggers charged spells to be triggered or ongoing spells to finish.
    /// </summary>
    public abstract void OnFinishCasting();
    

}

public abstract class ChargeSpell : SpellOverTime
{

    /// <summary>
    /// Called when button is released and charging is not finished.
    /// Restores spent mana / plays spell fail audio
    /// </summary>
    public abstract void OnAbortCasting();

}

public abstract class ContinuousSpell : SpellOverTime
{

}
