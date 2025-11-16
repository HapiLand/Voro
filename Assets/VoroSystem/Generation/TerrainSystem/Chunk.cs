using System;
using UnityEngine;
using VoroSystem.Landscape.WorldMapSystem;

namespace VoroSystem.Generation.TerrainSystem {
/// <summary>
/// Mesh form of a Tile
/// </summary>
[Serializable]
public class Chunk {
    #region Serialized Fields

    /// <summary>
    /// location of the chunk
    /// </summary>
    public Tile tile;

    /// <summary>
    /// does the instance exist?
    /// </summary>
    public bool initialised;

    /// <summary>
    /// has the Chunk changed?
    /// </summary>
    public bool dirty;

    /// <summary>
    /// quad mesh
    /// </summary>
    public Quad quad;

    /// <summary>
    /// game object instance
    /// </summary>
    public GameObject instance;

    /// <summary>
    /// store the previous visibility value
    /// </summary>
    public bool lastVisibility;

    #endregion

    public Chunk(Tile tile) {
        this.tile = tile;
        quad = new Quad(this.tile);
        dirty = false;
        initialised = false;
        lastVisibility = false;
    }
}
}