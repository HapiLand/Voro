namespace VoroSystem.GraphEditor.UserInterface.Elements {
public interface IContainerMutable<T> : IContainer<T> where T : IItem {
    void Add(T item);
    void Remove(T item);
    void MoveUp(T item);
    void MoveDown(T item);
}
}