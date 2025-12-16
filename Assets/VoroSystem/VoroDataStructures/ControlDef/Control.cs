namespace VoroSystem.VoroDataStructures.ControlDef {
public class Control<T> : IControl<T> where T : ControlDataBase {
  public Control(string name, string type, T value) {
    Name = name;
    Type = type;
    Value = value;
  }

  #region IControl<T> Members
  public string Name { get; }
  public string Type { get; }
  public T Value { get; }
  #endregion
}
}