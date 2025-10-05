namespace VoroSystem.Interface {
public interface IElement<T> {
    bool Active { get; set; }
    string Name { get; }
    int Index { get; }
    void SetIndex(int index);
    T Content { get; set; }    
    void SetContent(T data);
}
}