using System;
using VoroSystem.GridSystem.Interface;

namespace VoroSystem.GridSystem {
public class Tile : ITile {
    bool _visible;

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

    public bool IsDirty { get; private set; }

    public void OnBecameActive() {
        MarkDirty();
    }

    public void OnDisabled() {
        MarkDirty();
    }

    public void MarkDirty() {
        IsDirty = true;
    }

    void OnBecameVisible() {
        OnVisible?.Invoke(this);
    }

    public event Action<Tile> OnVisible;

    void NoLongerVisible() {
        NotVisible?.Invoke(this);
    }

    public event Action<Tile> NotVisible;
}
}