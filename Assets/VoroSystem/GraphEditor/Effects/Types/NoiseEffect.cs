using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;

namespace VoroSystem.GraphEditor.Effects.Types {
public class NoiseEffect : BaseEffect {
    NoiseEffect(List<IBaseControl> controls) : base("Noise", EffectType.Noise, controls) { }

    public static NoiseEffect CreateInstance() {
        var controls = new List<IBaseControl>
        {
            ControlFactory.FloatSliderControl("Size", 0f, 1f, 1f),
            ControlFactory.FloatSliderControl("Strength", 0f, 1f, 1f),
            ControlFactory.IntSliderControl("Randomness", 0, 10, 1),
            ControlFactory.ToggleControl("Rough Noise", false)
        };
        return new NoiseEffect(controls);
    }

    public override void Draw() {
        base.Draw();
    }
}
}