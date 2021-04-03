
public interface Effect
{

    void OnEffectStart();

}

public interface EffectOverTime : Effect
{
    void OnEffectOverTime();

    void OnEffectFinished();
}

public interface AreaOfEffect : Effect
{
    float RadiusOfArea { get; }

    Entity[] EntitiesWithinArea { get; }
}


