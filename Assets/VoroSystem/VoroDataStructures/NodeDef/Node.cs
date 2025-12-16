using System.Collections.Generic;
using VoroSystem.VoroDataStructures.ControlDef;

namespace VoroSystem.VoroDataStructures.NodeDef {
public class Node : INode {
  public Node(string name, string mode, List<IControl<ControlDataBase>> controls) {
    Name = name;
    Mode = mode;
    Controls = controls;
  }

  #region INode Members
  public string Name { get; }
  public string Mode { get; }
  public List<IControl<ControlDataBase>> Controls { get; }
  #endregion
}
}