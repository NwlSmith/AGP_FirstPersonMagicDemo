using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cast
{
    protected readonly Spell _spell;

    public Cast(Spell spell)
    {
        _spell = spell;
    }

    /// <summary>
    /// Called when the button is first pressed.
    /// Sometimes this will be the start and end of the spell, sometimes this will begin charging and calling OnCastingOverTime.
    /// </summary>
    public abstract void OnStartCasting();

}

public abstract class InstantCast : Cast
{
    public InstantCast(Spell spell) : base(spell)
    {

    }

    public override void OnStartCasting() => _spell.OnEffectTrigger();
}

public abstract class OverTimeCast : Cast
{

    public OverTimeCast(Spell spell) : base(spell)
    {

    }

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

public abstract class ChargeCast : OverTimeCast
{

    protected float _elapsedTime;
    protected readonly float _chargeDuration;

    public ChargeCast(Spell spell, float duration) : base(spell)
    {
        _chargeDuration = duration;
    }

    public override void OnCastingOverTime()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _chargeDuration)
        {
            OnFinishCasting();
        }
        else
        {
            // Check if let go of input, if yes, abort casting
        }
    }

    /// <summary>
    /// Called when button is released and charging is not finished.
    /// Restores spent mana / plays spell fail audio
    /// </summary>
    public void OnAbortCasting()
    {
        _spell.OnAbort();
    }
}

public abstract class ContinuousCast : OverTimeCast
{
    public ContinuousCast(Spell spell) : base(spell)
    {

    }
}


