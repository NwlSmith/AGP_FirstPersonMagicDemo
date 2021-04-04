using UnityEngine;

public abstract class HealEffect
{
    protected float _healAmount;
    protected Entity _affectedEntity;

    public HealEffect(float healAmount, Entity effected)
    {
        _healAmount = healAmount;
        _affectedEntity = effected;
    }

    protected void HealPlayer() => Services.Player.ChangeHealth(_healAmount);

    protected void HealEntity(Entity entity) => entity.ChangeHealth(_healAmount);

}

public class HealSelfInstantEffect : HealEffect, Effect
{
    public HealSelfInstantEffect(float healAmount, Entity effected) : base(healAmount, effected) { }

    public void OnEffectStart() => HealPlayer();
}

public class HealSelfOverTimeEffect : HealEffect, EffectOverTime
{
    public HealSelfOverTimeEffect(float healAmount, Entity effected) : base(healAmount, effected) { }

    public void OnEffectStart()
    {
        Debug.Log("Player has started healing");
    }

    public void OnEffectOverTime() => Services.Player.ChangeHealth(_healAmount);

    public void OnEffectFinished()
    {
        Debug.Log("Player is finished healing");
    }
}

public class HealAOEEffect : HealEffect, AreaOfEffect
{

    public float RadiusOfArea { get; private set; }

    public Entity[] EntitiesWithinArea { get; private set; }

    public HealAOEEffect(float healAmount, Entity effected, float radius, Entity[] entities) : base(healAmount, effected)
    {
        RadiusOfArea = radius;
        EntitiesWithinArea = entities;
    }

    public void OnEffectStart()
    {
        foreach (Entity entity in EntitiesWithinArea)
        {
            HealEntity(entity);
        }
    }

}