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
                new Radial("Direction", 0f),
                new FloatSlider("Steepness", 0.1f, 0f, 1f),
                new Toggle("Reverse", false)
            }
        },
        ["Noise"] = new LayerEffect("Noise")
        {
            Fields = new List<EffectFieldBase>
            {
                new FloatSlider("Size", 0.75f, 0.2f, 0.75f),
                new FloatSlider("Steepness", 0.5f, 0.15f, 1f)
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
                new FloatSlider("min_step_size", 0.15f, 0f, 1f),
                new FloatSlider("max_step_size", 0.75f, 0f, 1f),
                new FloatSlider("StepSize", 1f, 0f, 1f),
                new Radial("direction", 0f),
                new IntSlider("iterations", 3, 0, 10)
            }
        }
    };

    public static IEnumerable<string> Names => Effects.Keys;

    public static LayerEffect Get(string name) {
        return Effects.GetValueOrDefault(name);
    }
}
}