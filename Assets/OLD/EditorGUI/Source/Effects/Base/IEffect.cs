using EditorGUI.Elements;
using EditorGUI.Source.Voro.Grids;

namespace EditorGUI.Source.Effects.Base {
public interface IEffect {
    /// <summary>
    ///     the display name of the effect
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     the visual elements that control the effect data
    /// </summary>
    InspectorElement InspectorControls { get; }

    /// <summary>
    ///     executes the effect
    /// </summary>
    /// <param name="diagram"></param>
    void Compute(ref WorldTile tile);
}
}