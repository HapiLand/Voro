using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.GridSystem.Interface {
public interface IGrid<T> {
    Vector2Int Size { get; set; }
    bool IsInitialized { get; }
    bool IsDirty { get; }
    T[,] Map { get; }
    Dictionary<Vector2Int, T> TileLookup { get; }
    void SetSize(Vector2Int size);
    IEnumerable<T> GetTiles();
    void MarkDirty();
}
}