using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using VoroSystem.GridSystem.Interface;
using VoroSystem.Terrain;
using Debug = UnityEngine.Debug;

namespace VoroSystem.GridSystem {
/// <summary>
///     <see cref="VoroCompute" />
///     <see cref="WorldController" />
/// </summary>
public class GridSystemMediator : IGridSystemMediator {
    readonly Chunk _chunk;
    readonly TileMap _map;

    public GridSystemMediator(TileMap tileMap) {
        _map = tileMap;
        _map.SetMediator(this);

        _chunk = new Chunk(0);
        _chunk.SetMediator(this);
    }

    public void ForEachCell(Action<Cell> action) {
        // get every Tile
        var items = new List<Tile>();

        ForEachTile(t => { items.Add(t); });

        foreach (var tile in items) {
            // ensure Tile is visible
            var visible = tile.Visible;
            if (!visible) {
                continue;
            }

            // todo return the Cell without producing a copy
            //  not possible right now due to the way VoroCompute works

            // copy the Cell and offset its position to find where it is in the world
            foreach (var cell in _chunk.GetCells()) {
                var tilePos = _map.GetTilePosition(tile);
                var position = tilePos + cell.Position;
                action(new Cell(position, cell.ID, cell.Color));
            }
        }
    }

    public void ForEachTile(Action<Tile> action) {
        foreach (var tile in _map.GetTiles()) {
            action(tile);
        }
    }

    public void Initialize(Vector2Int size) {
        Debug.Log("Initialize GridSystem Mediator");
        var sw = new Stopwatch();
        sw.Start();

        Debug.Log($"Initializing {size.x},{size.y} TileMap");
        _map.SetSize(size);
        _map.UpdateVisibility();
        Debug.Log($"TileMap contains {_map.GetTiles().Count()} visible Tiles");

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to initialize");
    }
}
}