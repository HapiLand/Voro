using System.Collections.Generic;

namespace VoroSystem.UserInterface.Interface {
public interface IContainer<T> {
    /// <summary>
    ///     the contents of the container
    /// </summary>
    T[] Container { get; set; }

    IEnumerable<T> GetItems();

    /// <summary>
    ///     adds a new value to the container
    /// </summary>
    void AddToContainer(T data);

    /// <summary>
    ///     clears the collection
    /// </summary>
    void ClearContainer();

    /// <summary>
    ///     removes this item from the container
    /// </summary>
    /// <param name="item"></param>
    void RemoveFromContainer(T item);
}
}