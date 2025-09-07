namespace ConfigEditor.V2.Effects.Internal {
/// <summary>
///     the contract for every effect
///     as they all require a name and the method to run the effect
/// </summary>
public interface IEffect {
    string EffectName { get; }
    void Compute(); // execute the effect
}
}