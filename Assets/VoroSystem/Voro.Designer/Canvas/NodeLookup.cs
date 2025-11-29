using System.Collections.Generic;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Designer.Canvas.Core;
using VoroSystem.Voro.Designer.Canvas.Fields;

namespace VoroSystem.Voro.Designer.Canvas {
public static class NodeLookup {
    static readonly Dictionary<EffectName, Node> Effects = new()
    {
        {
            EffectName.Slope, new Node(EffectName.Slope)
            {
                fields = new List<FieldBase>
                {
                    new Radial("Direction", 0f),
                    new FloatField("Steepness", 0.1f),
                    new Toggle("Reverse", false)
                }
            }
        },
        {
            EffectName.Noise, new Node(EffectName.Noise)
            {
                fields = new List<FieldBase>
                {
                    new FloatSlider("Size", 0.75f, 0.2f, 0.75f),
                    new FloatSlider("Steepness", 0.5f, 0.15f, 1f)
                }
            }
        },
        {
            EffectName.Terrace, new Node(EffectName.Terrace)
            {
                fields = new List<FieldBase>
                {
                    new FloatSlider("MinStepSize", 0.15f, 0f, 1f),
                    new FloatSlider("MaxStepSize", 0.75f, 0f, 1f),
                    new FloatSlider("StepSize", 1f, 0f, 1f),
                    new Radial("direction", 0f),
                    new IntSlider("iterations", 3, 0, 10)
                }
            }
        },
        {
            EffectName.Flat, new Node(EffectName.Flat)
            {
                fields = new List<FieldBase>
                {
                    new FloatSlider("Height", 0f, 0f, 1f)
                }
            }
        }
    };

    public static IEnumerable<EffectName> Names => Effects.Keys;

    public static Node Get(EffectName name) {
        return Effects.GetValueOrDefault(name);
    }
}
}