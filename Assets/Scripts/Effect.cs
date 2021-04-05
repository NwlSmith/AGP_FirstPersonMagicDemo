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
    protected readonly Spell _spell;
    protected readonly float _magnitude;

    public Effect(EffectType type, Spell spell, float magnitude)
    {
        _type = type;
        _spell = spell;
        _magnitude = magnitude;
    }

    public abstract void OnEffectStart();

    protected void ImbueEffects() => EffectEnumToFunction.Trigger(_type, _spell._owningEntity, _magnitude);

}

public class InstantEffect : Effect
{

    public InstantEffect(EffectType type, Spell spell, float magnitude) : base(type, spell, magnitude) { }

    public override void OnEffectStart() => ImbueEffects();

}

public class EffectOverTime : Effect
{
    protected float _elapsedTime;
    protected readonly float _effectDuration;

    public EffectOverTime(EffectType type, Spell spell, float magnitude, float duration) : base(type, spell, magnitude)
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

    public void OnEffectFinished()
    {

    }
}

public class AreaOfEffect : Effect
{
    protected readonly LayerMask layers = 0;

    protected readonly Vector3 _center;
    protected readonly float _radiusOfArea;

    public AreaOfEffect(EffectType type, Spell spell, float magnitude, Vector3 location, float radius) : base(type, spell, magnitude)
    {
        _center = location;
        _radiusOfArea = radius;
    }

    public override void OnEffectStart()
    {

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


