using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType { Offensive, Defensive, Healing, Utility, Stealth }

public abstract class Spell
{
    public SpellType spellType;

    public float manaCost;

    public Effect effect;

    public abstract void Update();

    /// <summary>
    /// Called when the button is first pressed.
    /// Sometimes this will be the start and end of the spell, sometimes this will begin charging and calling OnCastingOverTime.
    /// </summary>
    public abstract void OnStartCasting();

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

    /// <summary>
    /// Called when button is released and charging is not finished.
    /// Restores spent mana / plays spell fail audio
    /// </summary>
    public abstract void OnAbortCasting();

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
    public void OnEffectTrigger() => effect.OnEffectStart();

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

public class InstantHeal : InstantCastSpell
{

    public Effect effect = new InstantHeal();

}

public abstract class ChargeSpell : Spell
{

    /// <summary>
    /// Called when the button is first pressed.
    /// Sometimes this will be the start and end of the spell, sometimes this will begin charging and calling OnCastingOverTime.
    /// </summary>
    public abstract void OnStartCasting();

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

    /// <summary>
    /// Called when button is released and charging is not finished.
    /// Restores spent mana / plays spell fail audio
    /// </summary>
    public abstract void OnAbortCasting();

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
    public void OnEffectTrigger() => effect.OnEffectStart();

}
