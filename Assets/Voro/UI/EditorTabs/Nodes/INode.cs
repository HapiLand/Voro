using System.Collections.Generic;
using Voro.Jen.Compute.FX.Internal;
using Voro.UI.EditorTabs.Nodes.Controls.Base;

namespace Voro.UI.EditorTabs.Nodes {
/// <summary>
///     interface of data Node requires for the node to be later used for Computing
///     Node[] --> List[IEffect] --> Compute
/// </summary>
public interface INode {
    /// <summary>
    ///     use to look up IEffect type
    /// </summary>
    EffectName Name { get; }

    /// <summary>
    ///     use to store IEffectData
    /// </summary>
    List<ControlElementBase> Controls { get; }
}
}