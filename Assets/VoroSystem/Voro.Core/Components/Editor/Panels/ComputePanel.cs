using System;
using UnityEngine.UIElements;
using VoroSystem.Voro.Core.Components.Editor.Panels.Base;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Core.Components.Editor.Panels {
[Serializable]
public class ComputePanel : BasePanel {
  public ComputePanel(string title) : base("compute", title) {
    Add(new Button(() => { ComputeEvents.GetInstance().RaiseCompute(); })
    {
      text = "Compute",
      style =
      {
        marginTop = 10,
        marginBottom = 10,
        marginLeft = 10,
        marginRight = 10
      }
    });
  }
}
}