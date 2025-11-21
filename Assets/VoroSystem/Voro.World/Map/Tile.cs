using System;
using UnityEngine;
using VoroSystem.Util.Cameras;
using VoroSystem.Util.Extensions;
using VoroSystem.Voro.World.TileEntities;

namespace VoroSystem.Voro.World.Map {
[Serializable]
public class Tile : ITile {
    bool _visible;
    internal TileState State;

    public Tile(int index, Vector2 position, float size) {
        Index = index;
        Position = position;
        Size = size;
        Visible = false;
        Dirty = false;
        State = TileState.None;
        TileEvents.RaiseTileCreated(this);
    }

    public bool Dirty { get; private set; }

    public void Update() {
        UpdateVisibility();
    }

    void SetState(TileState tileState) {
        State = tileState;
    }

    void UpdateVisibility() {
        var cam = CameraManager.Camera;
        var tileWorldPos = Position.ToVector3();
        var viewportPos = cam.WorldToViewportPoint(tileWorldPos);
        var isVisible = viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
        Visible = isVisible;
    }

    void SetDirty(bool value) {
        if (value == Dirty) {
            return;
        }

        if (value) {
            MakeDirty();
        }
        else {
            MakeClean();
        }

        return;

        void MakeDirty() {
            Dirty = true;
        }

        void MakeClean() {
            Dirty = false;
        }
    }

    #region Nested type: ${0}
    internal enum TileState {
        None,
        Build,
        Lifecycle,
        Remove
    }
    #endregion

    #region ITile Members
    public int Index { get; }
    public Vector2 Position { get; }
    public float Size { get; }

    public bool Visible {
        get => _visible;
        private set
        {
            /*
             *  Visible = true  & Dirty = true  --> Build this Tile so it exists
             *  Visible = true  & Dirty = false --> This Tile instance now exists
             *  Visible = false & Dirty = true  --> This Tile must be destroyed
             *  Visible = false & Dirty = false --> This Tile no longer exists
             */

            // make dirty when the visibility has changed
            SetDirty(_visible != value);
            _visible = value;

            if (value) {
                SetState(Dirty ? TileState.Build : TileState.Lifecycle);
            }
            else {
                SetState(Dirty ? TileState.Remove : TileState.None);
            }
        }
    }
    #endregion
}
}