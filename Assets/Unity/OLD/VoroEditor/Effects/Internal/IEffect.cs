using OLD.VoroEditor.Elements;
using OLD.VoroEditor.Source;

namespace OLD.VoroEditor.Effects.Internal {
/// <summary>
///     the contract for every effect
///     as they all require a name and the method to run the effect
/// </summary>
public interface IEffect {
    string EffectName { get; }
    Display Display { get; } // inspector display
    void Compute(ref VoroDiagram voroDiagram); // execute the effect
}
}