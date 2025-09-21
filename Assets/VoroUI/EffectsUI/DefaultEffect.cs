using System.Collections.Generic;
using UnityEngine;
using VoroUI.EffectsUI.Base;
using VoroUI.Elements.Base;
using VoroWorld.Diagrams;
using VoroWorld.Generation.Effects.Internal;
using IEffectData = VoroUI.EffectsUI.Internal.IEffectData;

namespace VoroUI.EffectsUI {
public class DefaultEffectData : IEffectData {
    public float Float;
    public int Int;
    public float LogFloat;
}

public class DefaultEffect : Effect<DefaultEffectData> {
    /// <summary>
    ///     stores the field controls for the effect
    /// </summary>
    List<ControlElementBase> _controls;
    public DefaultEffect() : base(nameof(EffectNames.DefaultFX), new DefaultEffectData()) { }

    public override List<ControlElementBase> Controls {
        get
        {
            if (_controls == null) {
                // build controls the first time they are accessed
                _controls = new List<ControlElementBase>();

                CreateFloatSlider(
                    "Float Value",
                    () => Data.Float,
                    val => Data.Float = val,
                    0f,
                    1f,
                    0f
                );

                CreateIntSlider(
                    "Int Value",
                    () => Data.Int,
                    val => Data.Int = val,
                    0,
                    360,
                    180
                );

                CreateLogFloatSlider(
                    "Log Float Value",
                    () => Data.LogFloat,
                    val => Data.LogFloat = val,
                    0f,
                    1f,
                    0f
                );
            }

            return _controls;
        }
    }
}
}