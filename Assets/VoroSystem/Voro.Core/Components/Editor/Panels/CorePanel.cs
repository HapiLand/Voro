using System;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Core.Components.Editor.Panels.Base;

namespace VoroSystem.Voro.Core.Components.Editor.Panels {
[Serializable]
public class CorePanel : BasePanel {
  public CorePanel(string title, Diagram diagram) : base("core", title) {
    Add(new DiagramPanel("Diagram", diagram));
  }
}
}