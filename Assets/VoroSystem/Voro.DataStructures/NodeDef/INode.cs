using System.Collections.Generic;
using VoroSystem.Voro.DataStructures.ControlDef;

namespace VoroSystem.Voro.DataStructures.NodeDef {
public interface INode {
  string Name { get; }
  string Mode { get; }
  List<IControl<ControlDataBase>> Controls { get; }
}
}