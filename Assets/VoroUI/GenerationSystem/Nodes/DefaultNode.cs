using System.Collections.Generic;
using VoroUI.EditorTabs.Nodes.Controls.Base;
using VoroWorld.Generation.Effects.Internal;

namespace VoroUI.EditorTabs.Nodes {
public class DefaultControlData : IControlData {
    public float Height;
}

public class DefaultNode : Node<DefaultControlData> {
    List<ControlElementBase> _controls;

    public DefaultNode() : base(EffectNames.DefaultEffect, new DefaultControlData()) { }

    public override List<ControlElementBase> Controls {
        get
        {
            if (_controls == null) {
                _controls = new List<ControlElementBase>();

                CreateFloatSlider(
                    "Set Height",
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