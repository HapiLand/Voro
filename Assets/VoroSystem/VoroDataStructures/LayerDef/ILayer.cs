using System.Collections.Generic;
using VoroSystem.VoroDataStructures.NodeDef;

namespace VoroSystem.VoroDataStructures.LayerDef {
public interface ILayer {
  string Name { get; }
  List<INode> Nodes { get; }
}
}