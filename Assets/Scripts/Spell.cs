
public enum SpellType { Offensive, Defensive, Healing, Utility, Stealth }
public delegate void SpellUpdateMethod();

/*
 * All Spells have:
 * - A cast procedure
 * - A delivery
 * - An effect
 */
public abstract class Spell
{

    public readonly string _name;

    public SpellType spellType;

    public float manaCost;

    protected Cast _cast;
    protected Delivery _delivery;
    protected Effect _effect;

    public readonly Entity _owningEntity;

    protected SpellUpdateMethod spellUpdateMethods;

    public Spell(Entity entity, string name)
    {
        _owningEntity = entity;
        _name = name;
    }

    /// <summary>
    /// Called when the button is first pressed.
    /// Sometimes this will be the start and end of the spell, sometimes this will begin charging and calling OnCastingOverTime.
    /// </summary>
    public abstract void OnStartCasting();

    public abstract void OnAbort();


    /// <summary>
    /// Called when a spell's effect is triggered.
    /// </summary>
    public void OnEffectTrigger()
    {
        DeductMana();
        _effect.OnEffectStart();
    }

    public void EffectDelivered(Entity entityToDeliverEffect) // ?????
    {

    }

    public void AddSpellUpdateMethod(SpellUpdateMethod updateMethod) => spellUpdateMethods += updateMethod;

    public void RemoveSpellUpdateMethod(SpellUpdateMethod updateMethod) => spellUpdateMethods -= updateMethod;

    public void Update() => spellUpdateMethods();

    public bool CheckMana => _owningEntity.EnoughManaForSpell(manaCost);

    public void DeductMana() => _owningEntity.ChangeMana(manaCost);

}

/*

public abstract class InstantCastSpell : Spell
{

    public InstantCastSpell(Entity entity) : base(entity) { }

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
*/