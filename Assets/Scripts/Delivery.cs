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