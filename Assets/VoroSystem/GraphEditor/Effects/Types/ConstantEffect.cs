using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;

namespace VoroSystem.GraphEditor.Effects.Types {
public class ConstantEffect : BaseEffect {
    ConstantEffect(List<IBaseControl> controls) : base("Constant", EffectType.Constant, controls) { }

    public static ConstantEffect CreateInstance() {
        var controls = new List<IBaseControl>
        {
            ControlFactory.ToggleControl("Overwrite Existing", true),
            ControlFactory.FloatControl("Amount", 0f)
        };
        return new ConstantEffect(controls);
    }

    public override void Draw() {
        base.Draw();
    }
}
}