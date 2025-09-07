using UnityEngine.UIElements;

namespace ConfigEditor.V2.Effects.Internal {
/// <summary>
///     the contract for every effect
///     as they all require a name and the method to run the effect
/// </summary>
public interface IEffect {
    string EffectName { get; }
    VisualElement InspectorDisplay { get; } // inspector display
    void Compute(); // execute the effect
}
}