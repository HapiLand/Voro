using System;
using System.Collections.Generic;

namespace VoroSystem.GraphEditor.UserInterface.Elements {
public interface IContainer<T> where T : IItem {
    List<T> Items { get; }
    T this[int index] { get; }
    void ForEach(Action<T> action);
}
}