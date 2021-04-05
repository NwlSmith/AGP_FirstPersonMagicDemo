using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { FireDamage, IceDamage, ElectricDamage, ShieldBuff, Healing, Utility, Stealth }
public delegate void EffectFunction(Entity entity, float magnitude);

public static class EffectEnumToFunction
{
    private static readonly Dictionary<EffectType, EffectFunction> dictionary;

    static EffectEnumToFunction()
    {
        dictionary.Add(EffectType.FireDamage, FireDamage);
        dictionary.Add(EffectType.IceDamage, IceDamage);
        dictionary.Add(EffectType.ElectricDamage, ElectricDamage);
        dictionary.Add(EffectType.ShieldBuff, ShieldBuff);
        dictionary.Add(EffectType.Healing, Healing);
        dictionary.Add(EffectType.Utility, Utility);
        dictionary.Add(EffectType.Stealth, Stealth);
    }

    public static void Trigger(EffectType effectType, Entity entity, float magnitude) => dictionary[effectType](entity, magnitude);

    private static void FireDamage(Entity entity, float magnitude) => entity.ChangeHealth(-1 * magnitude);
    private static void IceDamage(Entity entity, float magnitude) => entity.ChangeHealth(-1 * magnitude);
    private static void ElectricDamage(Entity entity, float magnitude) => entity.ChangeHealth(-1 * magnitude);
    private static void ShieldBuff(Entity entity, float magnitude) { }
    private static void Healing(Entity entity, float magnitude) => entity.ChangeHealth(magnitude);
    private static void Utility(Entity entity, float magnitude) { }
    private static void Stealth(Entity entity, float magnitude) { }
}

public abstract class Effect
{
    protected readonly EffectType _type;
    protected readonly Entity _entity;
    protected readonly Spell _spell;
    protected readonly float _magnitude;

    public Effect(EffectType type, Entity entity, Spell spell, float magnitude)
    {
        _type = type;
        _entity = entity;
        _spell = spell;
        _magnitude = magnitude;
    }

    public abstract void Update();

    public abstract void OnEffectStart();

    protected void ImbueEffects() => EffectEnumToFunction.Trigger(_type, _entity, _magnitude);

}

public class InstantEffect : Effect
{

    public InstantEffect(EffectType type, Entity entity, Spell spell, float magnitude) : base(type, entity, spell, magnitude) { }

    public override void OnEffectStart() => _entity.AddEffect(this);

    public override void Update()
    {
        ImbueEffects();
        _entity.RemoveEffect(this);
    }

}

public class EffectOverTime : Effect
{
    protected float _elapsedTime;
    protected readonly float _effectDuration;

    public EffectOverTime(EffectType type, Entity entity, Spell spell, float magnitude, float duration) : base(type, entity, spell, magnitude)
    {
        _effectDuration = duration;
    }

    public override void OnEffectStart()
    {
        _elapsedTime = 0f;
        _entity.AddEffect(this);
    }

    public override void Update() => OnEffectOverTime();

    public void OnEffectOverTime()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _effectDuration)
        {
            OnEffectFinished();
            return;
        }

        ImbueEffects();
    }

    public void OnEffectFinished()
    {
        _entity.RemoveEffect(this);
    }
}

// Kind of defunct
public class AreaOfEffect : Effect
{
    protected readonly LayerMask layers = 0;

    protected readonly Vector3 _center;
    protected readonly float _radiusOfArea;

    public AreaOfEffect(EffectType type, Entity entity, Spell spell, float magnitude, Vector3 location, float radius) : base(type, entity, spell, magnitude)
    {
        _center = location;
        _radiusOfArea = radius;
    }

    public override void OnEffectStart() => _entity.AddEffect(this);

    public override void Update()
    {
        ImbueEffects();
        _entity.RemoveEffect(this);
    }

    protected Entity[] GetEntitiesWithinArea()
    {
        Collider[] hits = Physics.OverlapSphere(_center, _radiusOfArea, layers, QueryTriggerInteraction.Ignore);
        List<Entity> entities = new List<Entity>();

        foreach (Collider hit in hits)
        {
            Entity entity = hit.GetComponent<Entity>();
            if (entity != null && entity != _spell._owningEntity)
                entities.Add(entity);
        }

        return entities.ToArray();
    }
}


