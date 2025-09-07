namespace ConfigEditor.V2.Effects.Internal {
/// <summary>
/// utility class for accessing and creating effect instances
/// </summary>
public static class EffectFactory {
    
    public static IEffect2 Create(string name)
    {
        return name switch
        {
            "Slope" => new SlopeEffect(new SlopeEffectData()),
            "Noise" => new NoiseEffect(new NoiseEffectData()),
            "Terrace" => new TerraceEffect(new TerraceEffectData()),
            "Null" => new NullEffect(new NullEffectData()),
            "SetTag" => new SetTagEffect(new SetTagEffectData()),
            _ => null
        };
    }
}
}