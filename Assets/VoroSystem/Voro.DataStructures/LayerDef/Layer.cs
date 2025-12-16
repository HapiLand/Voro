using System.Collections.Generic;
using VoroSystem.Voro.DataStructures.NodeDef;

namespace VoroSystem.Voro.DataStructures.LayerDef {
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