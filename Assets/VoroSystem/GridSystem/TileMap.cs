using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.GridSystem.Interface;
using Debug = UnityEngine.Debug;

namespace VoroSystem.GridSystem {
public class TileMap : IGrid<Tile> {
    IGridSystemMediator _mediator;

    /// <summary>
    ///     direct tile access via index
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Tile this[int x, int y] {
        get { return Map[x, y] ??= new Tile(); }
        set
        {
            Debug.Log($"SetTile {x},{y}");
            Map[x, y] = value;
            TileLookup[new Vector2Int(x, y)] = value;
            MarkDirty();
        }
    }

    public void SetSize(Vector2Int size) {
        Size = size;
        Map = new Tile[size.x, size.y]; // set the dimensions of the map
        TileLookup = new Dictionary<Vector2Int, Tile>();
        for (var x = 0; x < size.x; x++) {
            for (var y = 0; y < size.y; y++) {
                this[x, y] = new Tile();
            }
        }

        IsInitialized = true;
    }

    /// <summary>
    ///     dimensions of the Tile map
    /// </summary>
    public Vector2Int Size { get; set; }

    /// <summary>
    ///     indicates whether the Map has been initialized (it contains Tiles)
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <summary>
    ///     indicate if any new Tiles have been added
    /// </summary>
    public bool IsDirty { get; private set; }

    /// <summary>
    ///     2d array containing all Tiles
    /// </summary>
    public Tile[,] Map { get; private set; }

    /// <summary>
    ///     fast tile access by coordinate
    /// </summary>
    public Dictionary<Vector2Int, Tile> TileLookup { get; private set; }

    public void MarkDirty() {
        IsDirty = true;
    }

    public IEnumerable<Tile> GetTiles() {
        for (var x = 0; x < Size.x; x++) {
            for (var y = 0; y < Size.y; y++) {
                yield return this[x, y];
            }
        }
    }

    public void SetMediator(IGridSystemMediator gridSystemMediator) {
        _mediator = gridSystemMediator;
    }

    public Vector3 GetTilePosition(Tile tile) {
        foreach (var kvp in TileLookup.Where(kvp => kvp.Value == tile)) {
            return new Vector3(kvp.Key.x, 0, kvp.Key.y);
        }

        throw new InvalidOperationException("Tile not found");
    }

    /// <summary>
    ///     gets every Cell in the Map
    /// </summary>
    public IEnumerable<(Vector3 Position, int Id, Vector3 Color)> GetPoints() {
        var points = new List<(Vector3 Position, int Id, Vector3 Color)>();

        _mediator.ForEachCell(cell => {
            points.Add((
                cell.Position,
                cell.ID,
                new Vector3(cell.Color.r, cell.Color.g, cell.Color.b)
            ));
        });

        foreach (var result in points) {
            yield return result;
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

        // todo set from camera visibility
        foreach (var tile in GetTiles()) {
            tile.Visible = true;
        }
    }
}
}