using System;
using UnityEngine;
using VoroSystem.Cameras;
using VoroSystem.Extensions;
using VoroSystem.Landscape.TilemapSystem.Tiles;

namespace VoroSystem.Landscape.WorldMapSystem {
[Serializable]
public class Tile {
    #region Serialized Fields

    public int index;
    public Vector2 position;
    public float size;

    #endregion


    bool _visible;

    public Tile(int index, Vector2 position, float size) {
        this.index = index;
        this.position = position;
        this.size = size;
        Visible = false;
        Dirty = false;
        State = StateType.None;
    }

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
                SetState(Dirty ? StateType.Build : StateType.Lifecycle);
            }
            else {
                SetState(Dirty ? StateType.Remove : StateType.None);
            }
        }
    }

    public bool Dirty { get; private set; }
    public StateType State { get; private set; }

    public void Update() {
        UpdateVisibility();
    }

    void SetState(StateType stateType) {
        State = stateType;
    }

    void UpdateVisibility() {
        var cam = CameraManager.Camera;
        var tileWorldPos = position.ToVector3();
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
}
}