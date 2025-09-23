using VoroWorld.Diagrams;

namespace VoroWorld.Generation.Effects.Base {
public abstract class Effect<TEffectData> : EffectBase, IEffect {
    public TEffectData Data;

    public Effect(string name, TEffectData data) {
        Name = name;
        Data = data;
    }

    public abstract void Compute(ref VoroDiagram diagram);
    public string Name { get; }
}
}