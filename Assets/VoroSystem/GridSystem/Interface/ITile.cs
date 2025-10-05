namespace VoroSystem.GridSystem.Interface {
public interface ITile {
    bool IsDirty { get; }
    void OnBecameActive();
    void OnDisabled();
    void MarkDirty();
}
}