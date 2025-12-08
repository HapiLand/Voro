using System;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class FloatInputControl : TypedControlBase<float> {
  public FloatInputControl(string name, float defaultValue)
    : base(name, defaultValue, ControlType.FloatField) { }
}
}