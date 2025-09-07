using System;
using ConfigEditor.V2.Effects.Internal;
using UnityEngine;

namespace ConfigEditor.V2.Effects {
/// <summary>
///     concrete implementation of a foo-effect
///     uses FooEffectData to define the configuration for the effect
///     --
///     this effect is what will be executed when the editor processes a voro
/// </summary>
public class SlopeEffect : Effect<SlopeEffectData> {
    public SlopeEffect(SlopeEffectData data) : base("Slope", data) { }

    public override void Compute() {
        Debug.Log($"Compute effect: {EffectName}");
    }
}

/// <summary>
///     data structure to configure any effects which are a Slope Effect
///     -----
///     Slope/Noise/Terrace alter height
///     SetGroup is an effect that does not set height, only a group
///     what this means is one category can use FooEffectData
///     another category BarEffectData
///     -----
///     another option is bespoke one-of-a-kind effects can use their own GenericEffectData
/// </summary>
[Serializable]
public class SlopeEffectData : IEffectData {
    public float slopeDirection;
    public float slopeScale;

    public override string ToString() {
        return $"SlopeEffectData {nameof(slopeDirection)}: {slopeDirection}, {nameof(slopeScale)}: {slopeScale}";
    }
}
}