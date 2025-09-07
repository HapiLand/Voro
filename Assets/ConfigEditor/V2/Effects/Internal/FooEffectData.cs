using System;

namespace ConfigEditor.V2.Effects.Internal {
/// <summary>
///     data structure to configure any effects that use a FooEffect
///     -----
///     the theory is that Slope/Noise/Terrace are effects dealing in height
///     SetGroup is an effect that does not alter height
///     what this means is one category can use FooEffectData
///     another category BarEffectData
///     -----
///     another option is bespoke one-of-a-kind effects can use their own GenericEffectData
/// </summary>
[Serializable]
public class FooEffectData {
    public float Foo;
    public float Bar;
    public float Pee;

    public override string ToString() {
        return $"{nameof(Foo)}: {Foo}, {nameof(Bar)}: {Bar}, {nameof(Pee)}: {Pee}";
    }
}
}