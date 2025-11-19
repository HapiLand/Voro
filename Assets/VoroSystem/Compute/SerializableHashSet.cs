using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Compute {
[Serializable]
public class SerializableHashSet<T> {
  #region Serialized Fields

  [SerializeField] List<T> items = new();

  #endregion

  HashSet<T> _set;

  public SerializableHashSet() {
    _set = new HashSet<T>(items);
  }

  public bool Add(T item) {
    if (!_set.Add(item)) {
      return false;
    }

    items.Add(item);
    return true;
  }

  public bool Remove(T item) {
    if (!_set.Remove(item)) {
      return false;
    }

    items.Remove(item);
    return true;
  }

  public bool Contains(T item) {
    return _set.Contains(item);
  }

  public void Clear() {
    _set.Clear();
    items.Clear();
  }

  public HashSet<T> AsHashSet() {
    return _set;
  }

  public IEnumerator<T> GetEnumerator() {
    return _set.GetEnumerator();
  }
}
}