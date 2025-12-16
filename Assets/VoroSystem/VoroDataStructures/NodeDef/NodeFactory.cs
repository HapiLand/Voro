using System.Collections.Generic;
using VoroSystem.VoroDataStructures.ControlDef;

namespace VoroSystem.VoroDataStructures.NodeDef {
public static class NodeFactory {
  public static Node ConvertToNode(NodeDataTransferObject dto) {
    var controls = new List<IControl<ControlDataBase>>();
    foreach (var controlDTO in dto.Controls) {
      controls.Add(ControlFactory.CreateControl(controlDTO.Type, controlDTO.Name, controlDTO.Value));
    }

    var node = new Node(dto.Name, dto.Mode, controls);
    return node;
  }
}
}