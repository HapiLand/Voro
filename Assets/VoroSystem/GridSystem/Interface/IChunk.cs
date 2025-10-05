using System.Collections.Generic;

namespace VoroSystem.GridSystem.Interface {
public interface IChunk<T> {
    int ID { get; }
    T[] Content { get; }
    void SetContent(T[] data);
    IEnumerable<T> GetCells();
}
}