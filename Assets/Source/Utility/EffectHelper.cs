using System.Collections.Generic;
using VoroUI.Elements.Base;
using VoroWorld.Generation.Effects;
using VoroWorld.Generation.Effects.Base;
using VoroWorld.Generation.Effects.Internal;

namespace Source.Utility {
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