using System.Collections.Generic;
using VoroSystem.VoroDataStructures.NodeDef;

namespace VoroSystem.VoroDataStructures.LayerDef {
public class Layer : ILayer {
  public Layer(string name, List<INode> nodes) {
    Name = name;
    Nodes = nodes;
  }

  #region ILayer Members
  public string Name { get; }
  public List<INode> Nodes { get; }
  #endregion
}
}