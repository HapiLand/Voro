using System;

namespace VoroSystem.UserInterface.Interface {
public interface IUserInterfaceMediator {
    void Initialize(int preset);
    void OpenWindow();

    /// <summary> move position of Item up the collection </summary>
    void MoveUp<T>(T item) where T : IOrderedItem;

    /// <summary> move position of Item down the collection </summary>
    void MoveDown<T>(T item) where T : IOrderedItem;

    /// <summary> get the index of this Item in its collection </summary>
    int GetIndex<T>(T item) where T : IOrderedItem;

    /// <summary> get every Item from its collection </summary>
    void ForEach<T>(Action<T> action) where T : IOrderedItem;

    /// <summary> get the Item that is active </summary>
    T GetActiveItem<T>(Func<T, bool> action) where T : ISelectable;

    /// <summary> creates a new Item and adds it to the collection </summary>
    void CreateItem<T>(T item) where T : IOrderedItem;

    /// <summary> removes this Item from the collection </summary>
    void RemoveItem<T>(T item) where T : IOrderedItem;
}
}