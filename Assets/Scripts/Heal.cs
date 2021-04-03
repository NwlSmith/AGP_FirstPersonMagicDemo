using UnityEngine;

public abstract class Heal
{
    protected float healAmount = 1f;

    protected void HealPlayer() => Services.Player.ChangeHealth(healAmount);

    protected void HealEntity(Entity entity) => entity.ChangeHealth(healAmount);
}

public class HealSelfInstant : Heal, Effect
{
    public void OnEffectStart() => HealPlayer();
}

public class HealSelfOverTime : Heal, EffectOverTime
{
    public void OnEffectStart()
    {
        Debug.Log("Player has started healing");
    }

    public void OnEffectOverTime() => Services.Player.ChangeHealth(healAmount);

    public void OnEffectFinished()
    {
        Debug.Log("Player is finished healing");
    }
}

public class HealAreaOfEffect : Heal, AreaOfEffect
{

    public float RadiusOfArea { get; private set; }

    public Entity[] EntitiesWithinArea { get; private set; }

    public void OnEffectStart()
    {
        foreach (Entity player in EntitiesWithinArea)
        {
            HealEntity(player);
        }
        HealPlayer();
    }

}