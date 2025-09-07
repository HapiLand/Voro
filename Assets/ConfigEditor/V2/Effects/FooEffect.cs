using ConfigEditor.V2.Effects.Internal;
using UnityEngine;

namespace ConfigEditor.V2.Effects {
/// <summary>
///     concrete implementation of a foo-effect
///     uses FooEffectData to define the configuration for the effect
///     --
///     this effect is what will be executed when the editor processes a voro
/// </summary>
public class FooEffect : Effect<FooEffectData> {
    public FooEffect(FooEffectData data) : base("Foo", data) { }

    public override void Compute() {
        Debug.Log($"Compute effect: {EffectName}");
    }
}
}