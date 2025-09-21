using System.Collections.Generic;
using VoroUI.EditorTabs.Nodes.Controls.Base;
using VoroWorld.Generation.Effects.Internal;

namespace VoroUI.EditorTabs.Nodes {
public interface INode {
    EffectNames Name { get; }
    List<ControlElementBase> Controls { get; }
}
}