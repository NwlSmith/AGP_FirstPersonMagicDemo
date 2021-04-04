/*
 * Delivery method of the spell effect.
 */

public abstract class Delivery
{
    protected readonly Spell _spell;

    public Delivery(Spell spell)
    {
        _spell = spell;
    }

    // Deliver the spell to an entity/entities
    public abstract void Deliver();

}

public abstract class SelfDelivery : Delivery
{

    public SelfDelivery(Spell spell, float radius) : base(spell)
    {

    }

    public override void Deliver()
    {
        _spell.EffectDelivered(_spell._owningEntity);
    }
}

public abstract class AreaOfDelivery : Delivery
{
    protected readonly float _radiusOfArea;

    public AreaOfDelivery(Spell spell, float radius) : base(spell)
    {
        _radiusOfArea = radius;
    }

    protected abstract Entity[] GetEntitiesWithinArea();
}

public abstract class Projectile : Delivery
{
    protected readonly float _speed;

    public Projectile(Spell spell, float speed) : base(spell) => _speed = speed;

    /// <summary>
    /// Called while a projectile spell is traveling.
    /// </summary>
    public abstract void OnTraveling();

    /// <summary>
    /// Called when a spell's projectile collides with something.
    /// </summary>
    public abstract void OnCollision();
}