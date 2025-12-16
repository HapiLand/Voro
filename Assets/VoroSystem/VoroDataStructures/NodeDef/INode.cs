using System.Collections.Generic;
using VoroSystem.VoroDataStructures.ControlDef;

namespace VoroSystem.VoroDataStructures.NodeDef {
public interface INode {
  string Name { get; }
  string Mode { get; }
  List<IControl<ControlDataBase>> Controls { get; }
}
}