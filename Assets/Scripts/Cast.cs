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

public class InstantCast : Cast
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

    public override void OnStartCasting()
    {
        _spell.AddSpellUpdateMethod(OnCastingOverTime);
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
    public virtual void OnFinishCasting()
    {
        _spell.RemoveSpellUpdateMethod(OnCastingOverTime);
    }

    /// <summary>
    /// Called when button is released and charging is not finished.
    /// Restores spent mana / plays spell fail audio
    /// </summary>
    public void OnAbortCasting()
    {
        _spell.RemoveSpellUpdateMethod(OnCastingOverTime);
        _spell.OnAbort();
    }
}

public class ChargeCast : OverTimeCast
{

    protected float _elapsedTime;
    protected readonly float _chargeDuration;

    public ChargeCast(Spell spell, float duration) : base(spell)
    {
        _chargeDuration = duration;
    }

    public override void OnStartCasting()
    {
        base.OnStartCasting();
        _elapsedTime = 0f;
    }

    public override void OnCastingOverTime()
    {
        _elapsedTime += Time.deltaTime;

        if (Input.GetButtonUp("Fire1"))
        {
            if (_elapsedTime >= _chargeDuration)
            {
                OnFinishCasting();
            }
            else
            {
                OnAbortCasting();
            }
        }
    }

    public override void OnFinishCasting()
    {
        base.OnFinishCasting();
        _spell.OnEffectTrigger();
    }
}

public class ContinuousCast : OverTimeCast
{
    public ContinuousCast(Spell spell) : base(spell)
    {

    }

    public override void OnStartCasting() => base.OnStartCasting();

    public override void OnCastingOverTime()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            OnFinishCasting();
            return;
        }
        else if (!_spell.CheckMana)
        {
            OnAbortCasting();
            return;
        }
        
        _spell.OnEffectTrigger();
    }

}


