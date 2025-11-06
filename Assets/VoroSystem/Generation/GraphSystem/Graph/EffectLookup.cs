using System.Collections.Generic;
using VoroSystem.Generation.GraphSystem.Fields;

namespace VoroSystem.Generation.GraphSystem.Graph {
public static class EffectLookup {
    static readonly Dictionary<string, LayerEffect> Effects = new()
    {
        ["Slope"] = new LayerEffect("Slope")
        {
            Fields = new List<EffectFieldBase>
            {
                new FloatSlider("Direction", 0f, 0f, 1f),
                new FloatSlider("Steepness", 0f, 0f, 1f),
                new Toggle("Reverse", false),
            }
        },
        ["Noise"] = new LayerEffect("Noise")
        {
            Fields = new List<EffectFieldBase>
            {
                new FloatSlider("Size", 0f, 0f, 1f),
                new FloatSlider("Steepness", 0f, 0f, 1f),
            }
        },
        ["Flat"] = new LayerEffect("Flat")
        {
            Fields = new List<EffectFieldBase>
            {
                new FloatSlider("Height", 0f, 0f, 1f)
            }
        },
        ["Terrace"] = new LayerEffect("Terrace")
        {
            Fields = new List<EffectFieldBase>
            {
                new FloatSlider("StepSize", 0f, 0f, 1f),
                new FloatSlider("Randomness", 0f, 0f, 1f),
            }
        }
    };

    public static IEnumerable<string> Names => Effects.Keys;

    public static LayerEffect Get(string name) {
        return Effects.GetValueOrDefault(name);
    }
}
}