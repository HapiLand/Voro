using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Core.Components.Editor.Panels.Base;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Core.Components.Editor.Panels {
[Serializable]
public class ComputePanel : BasePanel {
  #region Serialized Fields

  [SerializeField] Button computeButton;

  #endregion

  public ComputePanel(string title) : base("compute", title) {
    computeButton = new Button(() => { ComputeEvents.GetInstance().RaiseCompute(); })
    {
      text = "Compute"
    };
    Add(computeButton);
  }
}
}