namespace VoroSystem.Designer.GraphSystem.Core {
public interface IFieldDrawer<T> {
  void Draw(ref T value, string name);
}
}