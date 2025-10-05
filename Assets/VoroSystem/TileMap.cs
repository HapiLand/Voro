using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using VoroSystem.Extensions;
using VoroSystem.Interface;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public class TileMap : IGrid<Tile> {
    Tile[,] _map;

    /// <summary>
    ///     creates 2D array of Tile
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void SetSize(Vector2Int size) {
        Size = size;
        Debug.Log("creating a new Tile array");
        var sw = new Stopwatch();
        sw.Start();

        _map = new Tile[size.x, size.y]; // set the dimensions of the map
        Lookup = new Dictionary<Vector2Int, Tile>();
        for (var x = 0; x < size.x; x++) {
            for (var y = 0; y < size.y; y++) {
                AddItem(x,y,new Tile());
            }
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to generate a [{size.x} , {size.y}] array");
    }
    
    /// <summary>
    /// adds the new tile to the map
    /// </summary>
    public void AddItem(int x, int y, Tile item) {
        _map[x, y] = item;
        Lookup[new Vector2Int(x, y)] = item;
        MarkDirty();
    }


    /// <summary>
    ///     gets or creates a Tile at the coordinate
    /// </summary>
    /// <returns>the Tile at [x,z]</returns>
    Tile GetTile(int x, int z) {
        return _map[x, z] ??= new Tile();
    }

    public Vector2Int Origin { get; }
    public Vector2Int Size { get; set; }
    public int ID { get; }
    public bool Active { get; }
    public bool Dirty { get; }

    public Tile[,] Map => _map;

    public Dictionary<Vector2Int, Tile> Lookup { get; private set; }

    public void MarkDirty() {
        throw new NotImplementedException();
    }
    public void Instantiate() {
        throw new NotImplementedException();
    }


    public IEnumerable<Tile> AsEnumerable() {
        var xLength = _map.GetLength(0);
        var zLength = _map.GetLength(1);

        for (var x = 0; x < xLength; x++) {
            for (var z = 0; z < zLength; z++) {
                yield return GetTile(x, z);
            }
        }
    }

    /// <summary>
    ///     get every Chunk.Cell position for the entire TileMap
    /// </summary>
    /// <returns></returns>
    public IEnumerable<(Vector3 Position, int Id, Vector3 Color)> AsPoints() {
        // todo move elsewhere, TileMap shouldnt deal with point information
        foreach (var tile in AsEnumerable()) {
            if (!tile.Visible) {
                // dont use the tile if it isnt visible
                continue;
            }

            foreach (var cell in tile.Chunk.Content) {
                yield return (
                    tile.WorldPosition() + cell.Position,
                    cell.ID,
                    new Vector3(cell.Color.r, cell.Color.g, cell.Color.b)
                );
            }
        }
    }


    /// <summary>
    ///     set visibility for tiles in view of the camera
    /// </summary>
    public void UpdateVisibility() {
        // ensure the camera exists
        var cam = Camera.main;
        if (!cam) {
            Debug.LogError("no Camera found");
            return;
        }

        var sw = new Stopwatch();
        sw.Start();

        // select each Tile and its position
        var instances =
            from tile in AsEnumerable()
            let pos = tile.WorldPosition()
            select new { tile, pos };

        // set visibility based on whether the camera can see the Tile
        foreach (var item in instances) {
            var viewportPos = cam.WorldToViewportPoint(item.pos);
            var visible = viewportPos.z > 0f
                          && viewportPos.x >= 0f && viewportPos.x <= 1f
                          && viewportPos.y >= 0f && viewportPos.y <= 1f;
            item.tile.Visible = visible;
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to update visibility");
    }

    /// <summary>
    ///     copy the Chunk to each Tile
    /// </summary>
    /// <param name="chunk">the reference chunk</param>
    public void Blit(Chunk chunk) {
        var sw = new Stopwatch();
        sw.Start();

        // write the chunk into each Tile so they hold a copy of it
        foreach (var item in AsEnumerable()) {
            item.Chunk = chunk;
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to blit all Chunks");
    }

    
}
}