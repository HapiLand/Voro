using System;

namespace VoroSystem.Voro.DataStructures.ControlDef {
public static class ControlFactory {
  public static IControl<ControlDataBase> CreateControl(string type, string name, object value) {
    return type switch
    {
      "FloatInput" => new Control<FloatInputData>(name, type, new FloatInputData
      {
        Value = Convert.ToSingle(value)
      }),
      "Toggle" => new Control<ToggleData>(name, type, new ToggleData
      {
        Value = Convert.ToBoolean(value)
      })
    };
  }
}
}