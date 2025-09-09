namespace ConfigEditor.Source.Effects.Internal {
/// <summary>
///     data structure to configure any effects which are a certain Effect
///     -----
///     Slope/Noise/Terrace alter height
///     SetGroup is an effect that does not set height, only a group
///     what this means is one category can use FooEffectData
///     another category BarEffectData
///     -----
///     another option is bespoke one-of-a-kind effects can use their own GenericEffectData
/// </summary>
public interface IEffectData { }
}