using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
class TileMap {
    Tile[,] _map;

    /// <summary>
    ///     creates 2D array of Tile
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void SetSize(int x, int z) {
        Debug.Log("creating a new Tile array");
        var sw = new Stopwatch();
        sw.Start();

        _map = new Tile[x, z]; // set the dimensions of the map
        for (var i = 0; i < x; i++) {
            for (var j = 0; j < z; j++) {
                GetTile(i, j);
            }
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to generate a [{x} , {z}] array");
    }

    /// <summary>
    ///     gets or creates a Tile at the coordinate
    /// </summary>
    /// <returns>the Tile at [x,z]</returns>
    Tile GetTile(int x, int z) {
        return _map[x, z] ??= new Tile((x, z));
    }

    public Vector3 GetTilePosition(Tile tile) {
        return new Vector3(tile.Coord.x, 0f, tile.Coord.z);
    }

    IEnumerable<Tile> AsEnumerable() {
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
    public IEnumerable<Vector3> AsPoints() {
        var instances =
            from tile in AsEnumerable()
            let pos = GetTilePosition(tile)
            select new { tile, pos };
        foreach (var item in instances) {
            foreach (var cell in item.tile.Chunk.Points) {
                var cellPosition = cell.Position;
                var pos = item.pos + cellPosition;
                yield return pos;
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
            let pos = GetTilePosition(tile)
            select new { tile, pos };

        // set visibility based on whether the camera can see the Tile
        foreach (var item in instances) {
            item.tile.Visible = true;
            // todo implement visibility check
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

    public class Tile {
        bool _visible;

        public Tile((int x, int z) coord) {
            Coord = coord;
        }

        public Chunk Chunk { get; set; }

        public (int x, int z) Coord { get; }

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
            return $"[{Coord.x} , {Coord.z}]";
        }
    }
}
}