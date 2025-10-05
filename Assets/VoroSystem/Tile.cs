using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Extensions;
using VoroSystem.Interface;

namespace VoroSystem {
public class Tile : ITile<Cell> {
    bool _visible;

    public Tile() { }

    public Chunk Chunk { get; set; }

    // public (int x, int z) Coord { get; }

    public bool Visible {
        get => _visible;
        set
        {
            if (value) {
                if (!_visible) {
                    // this Tile was not previously visible
                    OnBecameVisible();
                }

                _visible = true;
            }
            else {
                if (_visible) {
                    // this Tile was previously visible
                    NoLongerVisible();
                }

                _visible = false;
            }
        }
    }

    void OnBecameVisible() {
        Debug.Log($"Tile {ToString()} became visible");
        OnVisible?.Invoke(this);
    }

    public event Action<Tile> OnVisible;

    void NoLongerVisible() {
        NotVisible?.Invoke(this);
    }

    public event Action<Tile> NotVisible;

    public override string ToString() {
        return $"[{this.WorldPosition().x:F1} , {this.WorldPosition().z:F1}]";
    }

    public bool Active { get; }
    public bool Dirty { get; }
    public Chunk Content { get; }
    public void OnBecameActive() {
        MarkDirty();
    }
    public void OnDisabled() {
        MarkDirty();
    }
    public void MarkDirty() {
        throw new NotImplementedException();
    }

    public IEnumerable<Cell> AsEnumerable() {
        var length = Chunk.Content.Length;
        for (var i = 0; i < length; i++) {
            yield return Chunk.Content[i];
        }
    }

    public void Instantiate() {
        throw new NotImplementedException();
    }
}
}