
public enum EffectType { FireDamage, IceDamage, ElectricDamage, ShieldBuff, Healing, Utility, Stealth }

public abstract class Effect
{
    protected readonly Spell _spell;

    public Effect(Spell spell)
    {
        _spell = spell;
    }

    public abstract void OnEffectStart();

    protected abstract void ImbueEffects();

}

public abstract class EffectOverTime : Effect
{
    protected float _elapsedTime;
    protected readonly float _effectDuration;

    public EffectOverTime(Spell spell, float duration) : base(spell)
    {
        _effectDuration = duration;
    }

    public override void OnEffectStart()
    {

    }

    public void OnEffectOverTime()
    {
        ImbueEffects();
    }

    public abstract void OnEffectFinished();
}

public abstract class AreaOfEffect : Effect
{
    protected readonly float _radiusOfArea;

    public AreaOfEffect(Spell spell, float radius) : base(spell)
    {
        _radiusOfArea = radius;
    }

    protected abstract Entity[] GetEntitiesWithinArea();
}


