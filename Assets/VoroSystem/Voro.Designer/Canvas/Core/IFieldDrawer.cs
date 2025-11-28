namespace VoroSystem.Voro.Designer.Canvas.Core {
public interface IFieldDrawer<T> {
  void Draw(ref T value, string name);
}
}