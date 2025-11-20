namespace VoroSystem.Voro.Designer.Graph.Core {
public interface IFieldDrawer<T> {
  void Draw(ref T value, string name);
}
}