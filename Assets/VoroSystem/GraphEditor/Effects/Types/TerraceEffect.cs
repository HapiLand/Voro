using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;

namespace VoroSystem.GraphEditor.Effects.Types {
public class TerraceEffect : BaseEffect {
    TerraceEffect(List<IBaseControl> controls) : base("Terrace", EffectType.Terrace, controls) { }

    public static TerraceEffect CreateInstance() {
        var controls = new List<IBaseControl>
        {
            ControlFactory.FloatControl("Step Size", 0.25f),
            ControlFactory.IntControl("Randomness", 1)
        };
        return new TerraceEffect(controls);
    }

    public override void Draw() {
        base.Draw();
    }
}
}