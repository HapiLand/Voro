using System;
using VoroEditor.Effects.Internal;
using VoroEditor.Source;
using VoroEditor.Utility;
using Display = VoroEditor.Elements.Display;

namespace VoroEditor.Effects {
/// <summary>
///     concrete implementation of a foo-effect
///     uses FooEffectData to define the configuration for the effect
///     --
///     this effect is what will be executed when the editor processes a voro
/// </summary>
public class NullEffect : Effect<NullEffectData> {
    Display _display;
    public NullEffect(NullEffectData data) : base("Null", data) { }

    public override Display Display {
        get
        {
            if (_display == null) {
                _display = UIHelper.CreateDisplay("NullDisplay");
            }

            return _display;
        }
    }

    public override void Compute(ref VoroDiagram voroDiagram) { }
}


[Serializable]
public class NullEffectData : IEffectData { }
}