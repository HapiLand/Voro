using System.Collections.Generic;
using VoroSystem.Voro.DataStructures.LayerDef;

namespace VoroSystem.Voro.DataStructures.DiagramDef {
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