using System.Collections.Generic;
using VoroSystem.VoroDataStructures.LayerDef;

namespace VoroSystem.VoroDataStructures.DiagramDef {
public class Diagram : IDiagram {
  public Diagram(string name, List<ILayer> layers) {
    Name = name;
    Layers = layers;
  }

  #region IDiagram Members
  public string Name { get; set; }
  public List<ILayer> Layers { get; set; }
  #endregion
}
}