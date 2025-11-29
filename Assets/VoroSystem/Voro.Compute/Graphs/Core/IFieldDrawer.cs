namespace VoroSystem.Voro.Compute.Graphs.Core {
public interface IFieldDrawer<T> {
  void Draw(ref T value, string name);
}
}