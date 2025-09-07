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
public class SetTagEffect : Effect<SetTagEffectData> {
    public SetTagEffect(SetTagEffectData data) : base("SetTag", data) { }

    public override void Compute() {
        Debug.Log($"Compute effect: {EffectName}");
    }
}

/// <summary>
///     data structure to configure any effects which are a Set Tag Effect
///     -----
///     Slope/Noise/Terrace alter height
///     SetGroup is an effect that does not set height, only a group
///     what this means is one category can use FooEffectData
///     another category BarEffectData
///     -----
///     another option is bespoke one-of-a-kind effects can use their own GenericEffectData
/// </summary>
[Serializable]
public class SetTagEffectData : IEffectData {
    public string tagName;
    public int tagID;

    public override string ToString() {
        return $"SetTagEffectData {nameof(tagName)}: {tagName}, {nameof(tagID)}: {tagID}";
    }
}
}