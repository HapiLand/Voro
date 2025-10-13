using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;

namespace VoroSystem.GraphEditor.Effects.Types {
public class SlopeEffect : BaseEffect {
    SlopeEffect(List<IBaseControl> controls) : base("Slope", EffectType.Slope, controls) { }

    public static SlopeEffect CreateInstance() {
        var controls = new List<IBaseControl>
        {
            ControlFactory.ToggleControl("Reverse", false),
            ControlFactory.FloatSliderControl("Direction", 0f, 360f, 0f),
            ControlFactory.FloatSliderControl("Steepness", 0f, 1f, 1f)
        };
        return new SlopeEffect(controls);
    }

    public override void Draw() {
        base.Draw();
    }
}
}