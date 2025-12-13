using System.Collections.Generic;
using VoroSystem.Voro.DataStructures.LayerDef;

namespace VoroSystem.Voro.DataStructures.DiagramDef {
public interface IDiagram {
  string Name { get; set; }
  List<ILayer> Layers { get; set; }
}
}