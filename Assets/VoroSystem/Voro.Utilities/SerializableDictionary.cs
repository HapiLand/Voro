using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Util {
/// <summary>
/// Generic Serializable Dictionary for Unity 2020.1 and above.
/// Simply declare your key/value types and you're good to go - zero boilerplate.
/// </summary>
[Serializable]
public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver {
  #region Serialized Fields

  // Internal
  [SerializeField] List<KeyValuePair> list = new();

#pragma warning disable 0414
  [SerializeField] [HideInInspector] bool keyCollision;
#pragma warning restore 0414

  #endregion

  Dictionary<TKey, TValue> _dict = new();

  Dictionary<TKey, int> _indexByKey = new();

  #region IDictionary<TKey,TValue> Members

  // IDictionary
  public TValue this[TKey key] {
    get => _dict[key];
    set
    {
      _dict[key] = value;
      if (_indexByKey.ContainsKey(key)) {
        var index = _indexByKey[key];
        list[index] = new KeyValuePair(key, value);
      }
      else {
        list.Add(new KeyValuePair(key, value));
        _indexByKey.Add(key, list.Count - 1);
      }
    }
  }

  public ICollection<TKey> Keys => _dict.Keys;
  public ICollection<TValue> Values => _dict.Values;

  public void Add(TKey key, TValue value) {
    _dict.Add(key, value);
    list.Add(new KeyValuePair(key, value));
    _indexByKey.Add(key, list.Count - 1);
  }

  public bool ContainsKey(TKey key) {
    return _dict.ContainsKey(key);
  }

  public bool Remove(TKey key) {
    if (!_dict.Remove(key)) {
      return false;
    }

    var index = _indexByKey[key];
    list.RemoveAt(index);
    UpdateIndexLookup(index);
    _indexByKey.Remove(key);
    return true;
  }

  public bool TryGetValue(TKey key, out TValue value) {
    return _dict.TryGetValue(key, out value);
  }

  // ICollection
  public int Count => _dict.Count;
  public bool IsReadOnly { get; set; }

  public void Add(KeyValuePair<TKey, TValue> pair) {
    Add(pair.Key, pair.Value);
  }

  public void Clear() {
    _dict.Clear();
    list.Clear();
    _indexByKey.Clear();
  }

  public bool Contains(KeyValuePair<TKey, TValue> pair) {
    return _dict.TryGetValue(pair.Key, out var value) && EqualityComparer<TValue>.Default.Equals(value, pair.Value);
  }

  public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
    if (array == null) {
      throw new ArgumentException("The array cannot be null.");
    }

    if (arrayIndex < 0) {
      // ReSharper disable NotResolvedInText
      throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
      // ReSharper restore NotResolvedInText
    }

    if (array.Length - arrayIndex < _dict.Count) {
      throw new ArgumentException("The destination array has fewer elements than the collection.");
    }

    foreach (var pair in _dict) {
      array[arrayIndex] = pair;
      arrayIndex++;
    }
  }

  public bool Remove(KeyValuePair<TKey, TValue> pair) {
    if (!_dict.TryGetValue(pair.Key, out var value)) {
      return false;
    }

    var valueMatch = EqualityComparer<TValue>.Default.Equals(value, pair.Value);
    return valueMatch && Remove(pair.Key);
  }

  // IEnumerable
  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
    return _dict.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() {
    return _dict.GetEnumerator();
  }

  #endregion

  #region ISerializationCallbackReceiver Members

  // Lists are serialized natively by Unity, no custom implementation needed.
  public void OnBeforeSerialize() { }

  // Populate dictionary with pairs from list and flag key-collisions.
  public void OnAfterDeserialize() {
    _dict.Clear();
    _indexByKey.Clear();
    keyCollision = false;
    for (var i = 0; i < list.Count; i++) {
      var key = list[i].key;
      if (key != null && !ContainsKey(key)) {
        _dict.Add(key, list[i].value);
        _indexByKey.Add(key, i);
      }
      else {
        keyCollision = true;
      }
    }
  }

  #endregion

  void UpdateIndexLookup(int removedIndex) {
    for (var i = removedIndex; i < list.Count; i++) {
      var key = list[i].key;
      _indexByKey[key]--;
    }
  }

  #region Nested type: ${0}

  [Serializable]
  struct KeyValuePair {
    public TKey key;
    public TValue value;

    public KeyValuePair(TKey key, TValue value) {
      this.key = key;
      this.value = value;
    }
  }

  #endregion
}
}