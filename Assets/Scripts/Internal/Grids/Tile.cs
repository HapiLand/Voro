using JetBrains.Annotations;
using Terrain;
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
    ///     the GameObject to represent the Tile, so that any GameObjects that
    ///     are from the Voro can be stored in this
    /// </summary>
    public GameObject VoroContainer;

    /// <summary>
    ///     the diagram that belongs to the tile
    ///     this is to be used for EditorCompute
    /// </summary>
    public VoroDiagram Diagram;

    [CanBeNull]
    public VoroDiagram UpdateVisibility(Vector3 playerPosition, float drawDistance) {
        // tile is visible when within a radius of the player

        // zero the Y value for both positions, as the visibility
        // is only calculated for a 2D plane
        playerPosition.y = 0f;
        var tilePos = CornerPosition;
        tilePos.y = 0f;

        var distance = Vector3.Distance(tilePos, playerPosition);
        var visible = distance < drawDistance;


        // the first time the tile becomes visible, it is initialised
        // the voro diagram in the tile is constructed
        VoroDiagram diagramInstance = null;
        if (!HasInitialised && visible) {
            Init();
            // the diagram has been constructed, it shall be returned
            diagramInstance = Diagram;
        }

        IsVisible = visible;

        return diagramInstance;
    }

    public bool HasInitialised;

    /// <summary>
    ///     called upon the tile becoming visible to the player for the first time
    /// </summary>
    void Init() {
        HasInitialised = true;
        // construct this tiles diagram
        var factory = new DiagramFactory();
        Diagram = factory.Create(_corner);

        // construct the container for the objects within the diagram
        VoroContainer = new GameObject($"Voro [{_corner.x:F1},{_corner.y:F1}]");
    }

    public Tile(Vector2 corner) : this() {
        _corner = corner;
        IsVisible = false;
        HasInitialised = false;
    }
}
}