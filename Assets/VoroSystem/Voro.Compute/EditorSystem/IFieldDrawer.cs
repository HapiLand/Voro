namespace VoroSystem.Voro.Compute.EditorSystem {
public interface IFieldDrawer<T> {
    void Draw(ref T value, string name);
}
}