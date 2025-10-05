using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Interface {
public interface IGrid<T> {
    Vector2Int Origin { get; }
    Vector2Int Size { get; set; }
    int ID { get; }
    bool Active { get; }
    bool Dirty { get; }
    T[,] Map { get; }
    void AddItem(int x, int y, T item);
    Dictionary<Vector2Int, T> Lookup { get; }
    void SetSize(Vector2Int size);
    IEnumerable<T> AsEnumerable();
    void MarkDirty();
    void Instantiate();
}
}