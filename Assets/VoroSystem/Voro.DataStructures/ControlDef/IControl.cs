namespace VoroSystem.Voro.DataStructures.ControlDef {
public interface IControl<out T> where T : ControlDataBase {
  string Name { get; }
  string Type { get; }
  T Value { get; }
}
}