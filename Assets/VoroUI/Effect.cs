namespace VoroUI {
public abstract class EffectBase { }

public interface IEffect {
    string Name { get; }
}

public abstract class Effect<TEffectData> : EffectBase, IEffect {
    protected TEffectData Data;

    public Effect(string name, TEffectData data) {
        Name = name;
        Data = data;
    }

    public string Name { get; }
}

public class DefaultEffect : Effect<DefaultEffectData> {
    public DefaultEffect() : base("Default", new DefaultEffectData()) { }
}

public interface IEffectData { }

public class DefaultEffectData : IEffectData { }
}