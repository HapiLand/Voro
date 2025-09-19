namespace VoroUI.Effects.Internal {
/// <summary>
///     helper class for dealing with Effects
/// </summary>
public static class EffectHelper {
    public static IEffect Create(string name) {
        return name switch
        {
            // "Slope" => new SlopeEffect(new SlopeEffectData()),
            // "Noise" => new NoiseEffect(new NoiseEffectData()),
            // "Terrace" => new TerraceEffect(new TerraceEffectData()),
            // "Null" => new NullEffect(new NullEffectData()),
            // "SetTag" => new SetTagEffect(new SetTagEffectData()),
            // "SetHeight" => new SetHeightEffect(new SetHeightEffectData()),
            _ => new DefaultEffect()
        };
    }
}
}