using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Interface {
public interface ITile<T> {
    bool Active { get; }
    bool Dirty { get; }
    void OnBecameActive();
    void OnDisabled();
    void MarkDirty();
    IEnumerable<T> AsEnumerable();
    void Instantiate();
}
}