using System.Collections.Generic;
using VoroUI.EditorTabs.Nodes.Controls.Base;
using VoroWorld.Generation.Effects.Internal;

namespace VoroUI.EditorTabs.Nodes {
/// <summary>
///     interface of data Node requires for the node to be later used for Computing
///     Node[] --> List[IEffect] --> Compute
/// </summary>
public interface INode {
    /// <summary>
    ///     use to look up IEffect type
    /// </summary>
    EffectNames Name { get; }

    /// <summary>
    ///     use to store IEffectData
    /// </summary>
    List<ControlElementBase> Controls { get; }
}
}