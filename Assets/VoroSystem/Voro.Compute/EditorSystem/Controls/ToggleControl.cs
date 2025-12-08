using System;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class ToggleControl : TypedControlBase<bool> {
  public ToggleControl(string name, bool defaultValue)
    : base(name, defaultValue, ControlType.Toggle) { }
}
}