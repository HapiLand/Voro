namespace VoroSystem.UserInterface.Interface {
public interface IOrderedItem {
    /// <summary>
    ///     the position of the item in its collection
    /// </summary>
    int Index { get; }

    /// <summary>
    ///     moves the position of the item in the collection
    /// </summary>
    void SetIndex(int index);
}
}