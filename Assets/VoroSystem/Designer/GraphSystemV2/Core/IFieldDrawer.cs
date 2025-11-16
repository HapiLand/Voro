namespace VoroSystem.Designer.GraphSystemV2.UI.Drawers {
public interface IFieldDrawer<T> {
    void Draw(ref T value, string name);
}
}