using System.Collections.Generic;
using VoroSystem.Voro.DataStructures.NodeDef;

namespace VoroSystem.Voro.DataStructures.LayerDef {
public static class LayerFactory {
  public static Layer ConvertToLayer(LayerDataTransferObject dto) {
    var nodes = new List<INode>();
    foreach (var nodeDTO in dto.Nodes)
    {
      nodes.Add(NodeFactory.ConvertToNode(nodeDTO));
    }
    var layer = new Layer(dto.Name, nodes);
    return layer;
  }
}
}