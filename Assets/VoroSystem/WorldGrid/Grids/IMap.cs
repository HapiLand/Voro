using System;
using UnityEngine;

namespace VoroSystem.WorldGrid.Grids {
public interface IMap<T> where T : ITile {
    T this[int x, int y] { get; set; }
    T this[int index] { get; }
    Vector2Int Size { get; }
    void ForEach(Action<T> action);
}
}