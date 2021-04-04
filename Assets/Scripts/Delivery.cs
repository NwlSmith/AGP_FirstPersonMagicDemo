using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Delivery method of the spell effect.
 */

public abstract class Delivery
{

    protected readonly LayerMask layers = 0;
    protected readonly Spell _spell;

    public Delivery(Spell spell)
    {
        _spell = spell;
    }

    // Deliver the spell to an entity/entities
    public abstract void Deliver();

}

public class SelfDelivery : Delivery
{

    public SelfDelivery(Spell spell) : base(spell)
    {

    }

    public override void Deliver()
    {
        _spell.EffectDelivered(_spell._owningEntity);
    }
}

public class AreaOfDelivery : Delivery
{
    protected readonly Vector3 _center;
    protected readonly float _radiusOfArea;

    public AreaOfDelivery(Spell spell, Vector3 location, float radius) : base(spell)
    {
        _radiusOfArea = radius;
        _center = location;
    }

    public override void Deliver()
    {
        foreach (Entity entity in GetEntitiesWithinArea())
        {
            _spell.EffectDelivered(entity);
        }
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

public class Projectile : Delivery
{
    protected readonly float _speed;
    protected readonly float _range;

    public Projectile(Spell spell, float speed = 5f, float range = 10f) : base(spell)
    {
        _speed = speed;
        _range = range;
    }

    public override void Deliver()
    {
        Entity entity = GetEntityInPath();
        if (entity != null)
            _spell.EffectDelivered(entity);
    }

    protected Entity GetEntityInPath()
    {
        if (Physics.Raycast(_spell.EntityLocation, _spell.EntityDirection, out RaycastHit hit, _range, layers, QueryTriggerInteraction.Ignore))
        {
            Entity entity = hit.collider.gameObject.GetComponent<Entity>();
            return entity;
        }
        return null;
    }

    /// <summary>
    /// Called while a projectile spell is traveling.
    /// </summary>
    public void OnTraveling() { }

    /// <summary>
    /// Called when a spell's projectile collides with something.
    /// </summary>
    public void OnCollision() { }
}