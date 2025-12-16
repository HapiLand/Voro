using System.Collections.Generic;
using VoroSystem.VoroDataStructures.LayerDef;

namespace VoroSystem.VoroDataStructures.DiagramDef {
public interface IDiagram {
  string Name { get; set; }
  List<ILayer> Layers { get; set; }
}
}