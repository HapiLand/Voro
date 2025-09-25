using System.Collections.Generic;
using Voro.Jen.Compute.FX;
using Voro.Jen.Compute.FX.Internal;
using Voro.UI.EditorTabs.Nodes.Controls.Base;

namespace Voro.UI.EditorTabs.Nodes {
public class ConstantHeightNode : Node<ConstantHeightData> {
    List<ControlElementBase> _controls;

    public ConstantHeightNode() : base(EffectName.ConstantHeight, new ConstantHeightData()) { }

    public override List<ControlElementBase> Controls {
        get
        {
            if (_controls == null) {
                _controls = new List<ControlElementBase>();

                CreateFloatSlider(
                    "Constant Height",
                    () => Data.Height,
                    val => Data.Height = val,
                    0f,
                    10f,
                    0f
                );
            }

            return _controls;
        }
    }
}
}