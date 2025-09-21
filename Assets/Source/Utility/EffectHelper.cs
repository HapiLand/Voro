using VoroUI.EditorTabs.Nodes;
using VoroWorld.Generation.Effects;
using VoroWorld.Generation.Effects.Base;
using VoroWorld.Generation.Effects.Internal;

namespace Source.Utility {
/// <summary>
///     helper class for dealing with Effects
/// </summary>
public static class EffectHelper {
    public static IEffect CreateIEffectFromName(EffectNames name) {
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

    public static INode CreateINodeFromName(EffectNames name) {
        return name switch
        {
            // "Slope" => new SlopeEffect(new SlopeEffectData()),
            // "Noise" => new NoiseEffect(new NoiseEffectData()),
            // "Terrace" => new TerraceEffect(new TerraceEffectData()),
            // "Null" => new NullEffect(new NullEffectData()),
            // "SetTag" => new SetTagEffect(new SetTagEffectData()),
            // "SetHeight" => new SetHeightEffect(new SetHeightEffectData()),
            _ => new DefaultNode()
        };
    }
}
}