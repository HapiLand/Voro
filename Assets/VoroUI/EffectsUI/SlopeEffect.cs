using System.Collections.Generic;
using UnityEngine;
using VoroUI.EffectsUI.Base;
using VoroUI.Elements.Base;
using VoroWorld.Diagrams;
using VoroWorld.Generation.Effects.Internal;
using IEffectData = VoroUI.EffectsUI.Internal.IEffectData;

namespace VoroUI.EffectsUI {
public class SlopeEffectData : IEffectData {
    public float Direction;
    public float Scale;
}

public class SlopeEffect : Effect<SlopeEffectData> {
    /// <summary>
    ///     stores the field controls for the effect
    /// </summary>
    List<ControlElementBase> _controls;

    public SlopeEffect() : base(nameof(EffectNames.Slope), new SlopeEffectData()) { }

    public override List<ControlElementBase> Controls {
        get
        {
            if (_controls == null) {
                // build controls the first time they are accessed
                _controls = new List<ControlElementBase>();

                CreateFloatSlider(
                    "Direction",
                    () => Data.Direction,
                    val => Data.Direction = val,
                    0f,
                    360f,
                    0f
                );

                CreateFloatSlider(
                    "Scale",
                    () => Data.Scale,
                    val => Data.Scale = val,
                    0f,
                    1f,
                    1f
                );
            }

            return _controls;
        }
    }

}
}