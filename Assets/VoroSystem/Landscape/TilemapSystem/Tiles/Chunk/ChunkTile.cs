using UnityEngine;
using VoroSystem.Cameras;
using VoroSystem.Extensions;
using VoroSystem.Generation.MesherSystem;

namespace VoroSystem.Landscape.TilemapSystem.Tiles.Chunk {
public class ChunkTile : IChunkTile {
    BaseResult baseResult;
    bool visible;

    public ChunkTile(int index, Vector2 position, float size) {
        this.Index = index;
        this.Position = position;
        Visible = false;
        Dirty = false;
        State = StateType.None;
        this.Size = size;
    }

    public ChunkTile(bool dirty) {
        Dirty = dirty;
    }

    public int Index { get; }

    public Vector2 Position { get; }

    public bool Visible {
        get => visible;
        private set
        {
            /*
             *  Visible = true  & Dirty = true  --> Build this Tile so it exists
             *  Visible = true  & Dirty = false --> This Tile instance now exists
             *  Visible = false & Dirty = true  --> This Tile must be destroyed
             *  Visible = false & Dirty = false --> This Tile no longer exists
             */

            // make dirty when the visibility has changed
            SetDirty(visible != value);
            visible = value;

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

    public float Size { get; }

    public BaseResult Result {
        get
        {
            baseResult ??= new BaseResult(this);
            return baseResult;
        }
    }

    public void Update() {
        UpdateVisibility();
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

    void SetState(StateType stateType) {
        State = stateType;
    }

    void UpdateVisibility() {
        var cam = CameraManager.Camera;
        var tileWorldPos = Position.ToVector3();
        var viewportPos = cam.WorldToViewportPoint(tileWorldPos);
        var isVisible = viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
        Visible = isVisible;
    }
}
}