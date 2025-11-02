using UnityEngine;
using VoroSystem.Cameras;
using VoroSystem.Extensions;

namespace VoroSystem.Grids.Tiles {
public class TileState {
    bool _visible;

    public TileState(bool visible, bool dirty) {
        Visible = visible;
        Dirty = dirty;
        SetState(StateType.None);
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
    public StateType StateType { get; private set; }

    void SetState(StateType stateType) {
        StateType = stateType;
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

    public void UpdateVisibility(Vector2 position) {
        var cam = CameraManager.Camera;
        var tileWorldPos = position.ToVector3();
        var viewportPos = cam.WorldToViewportPoint(tileWorldPos);
        var isVisible = viewportPos.z > 0 &&
                        viewportPos.x >= 0 && viewportPos.x <= 1 &&
                        viewportPos.y >= 0 && viewportPos.y <= 1;
        Visible = isVisible;
    }
}
}