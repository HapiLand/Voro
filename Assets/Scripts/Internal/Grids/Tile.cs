using JetBrains.Annotations;
using UnityEngine;

namespace Internal.Grids {
public struct Tile {
    /// <summary>
    ///     the corner position of this tile, the origin of it
    /// </summary>
    readonly Vector2 _corner;

    public Vector3 CornerPosition => new(_corner.x, 0f, _corner.y);
    public bool IsVisible;

    /// <summary>
    ///     the Voro that belongs to this tile
    /// </summary>
    public Voro VoroInstance;

    /// <summary>
    ///     the GameObject to represent the Tile, so that any GameObjects that
    ///     are from the Voro can be stored in this
    /// </summary>
    public GameObject VoroContainer;


    [CanBeNull]
    public Voro UpdateVisibility(Vector3 playerPosition, float drawDistance) {
        // tile is visible when within a radius of the player
        
        // zero the Y value for both positions, as the visibility
        // is only calculated for a 2D plane
        playerPosition.y = 0f;
        var tilePos = CornerPosition;
        tilePos.y = 0f;
        
        var distance = Vector3.Distance(tilePos, playerPosition);
        var visible = distance < drawDistance;


        // the first time the tile becomes visible, it is initialised
        // the Voro in the tile is constructed
        Voro? voroInstance = null;
        if (!HasInitialised && visible) {
            Init();
            // the voro has been constructed, it shall be returned
            voroInstance = VoroInstance;
        }

        IsVisible = visible;

        return voroInstance;
    }

    public bool HasInitialised;

    /// <summary>
    ///     called upon the tile becoming visible to the player for the first time
    /// </summary>
    void Init() {
        HasInitialised = true;
        // construct this tiles Voro
        VoroInstance = new Voro(_corner);

        // construct the container for this voro
        VoroContainer = new GameObject($"Voro [{_corner.x:F1},{_corner.y:F1}]");
    }


    public Tile(Vector2 corner) : this() {
        _corner = corner;
        IsVisible = false;
        HasInitialised = false;
    }
}
}