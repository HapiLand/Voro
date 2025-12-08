using System;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public abstract class TypedControlBase<T> : ControlBase {
  protected TypedControlBase(string name, T defaultValue, ControlType controlType)
    : base(name, defaultValue, controlType) { }

  public T Value {
    get => (T)defaultValue;
    set => defaultValue = value;
  }
}
}