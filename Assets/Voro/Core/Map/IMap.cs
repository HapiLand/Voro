using System;

namespace Voro.Core.Map {
interface IMap<T> where T : ITile {
    T this[int x, int y] { get; set; }
    T this[int index] { get; }
    (int x, int y) Size { get; }
    void ForEach(Action<T> getTile);
}
}