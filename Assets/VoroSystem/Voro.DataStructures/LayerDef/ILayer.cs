using System.Collections.Generic;
using VoroSystem.Voro.DataStructures.NodeDef;

namespace VoroSystem.Voro.DataStructures.LayerDef {
public interface ILayer {
  string Name { get; }
  List<INode> Nodes { get; }
}
}