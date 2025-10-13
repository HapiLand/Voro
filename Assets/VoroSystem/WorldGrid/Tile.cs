using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace VoroSystem.WorldGrid {
public interface ITile {
    Vector3 WorldPosition { get; }
    TileChunk Chunk { get; }
    void InstantiateTile(Transform parent);

    interface IChunk {
        /// <summary>
        ///     Cells are Read Only in order to preserve the default values
        /// </summary>
        IReadOnlyList<Cell> Cells { get; }

        /// <summary>
        ///     the Compute Result sets the Elevation for Cells
        /// </summary>
        List<float> CellElevations { get; set; }

        Cell this[int index] { get; set; }
        int CellCount { get; }
        void AddCell(Cell item);
        void ParseCells(int preset);
        void ForEach(Action<Cell> action);
    }

    /// <summary>
    ///     chunk is mutable
    /// </summary>
    class TileChunk : IChunk {
        readonly List<Cell> _cells = new();

        public TileChunk(int preset) {
            Debug.Log($"New TileChunk '{preset}'");
            ParseCells(preset);
        }

        public IReadOnlyList<Cell> Cells => _cells;
        public List<float> CellElevations { get; set; } = new();

        public Cell this[int index] {
            get => _cells[index];
            set => _cells[index] = value;
        }

        public int CellCount => _cells.Count;

        public void AddCell(Cell item) {
            _cells.Add(item);
        }

        public void ParseCells(int preset) {
            AssetLoader.LoadTable(0, out var assetText);
            if (string.IsNullOrEmpty(assetText)) {
                Debug.LogError("ParseCells input is null or empty");
                return;
            }

            var jObject = JObject.Parse(assetText);
            var jArray = jObject["Points"] as JArray;

            var array = JsonParseUtil.ParseArray(jArray, token => {
                var position = JsonParseUtil.GetValue(token, "Pos", Array.Empty<float>());
                var id = JsonParseUtil.GetValue(token, "Id", 0);
                var color = JsonParseUtil.GetValue(token, "Col", Array.Empty<float>());
                // parse text object ot Cell
                var cell = new Cell(position, id, color);
                return cell;
            });

            foreach (var cell in array) {
                // set the Cell
                _cells.Add(cell);
                // create the elevation of the Cell
                CellElevations.Add(cell.WorldPosition.y);
            }
        }

        public void ForEach(Action<Cell> action) {
            _cells.ForEach(action);
        }

        /// <summary>
        ///     get the position of the cell, offset vertically by its elevation
        /// </summary>
        /// <param name="cell"> </param>
        /// <returns> </returns>
        public Vector3 CellPosition(Cell cell) {
            var cellPos = new Vector3(
                cell.WorldPosition.x,
                cell.WorldPosition.y + CellElevations[0],
                cell.WorldPosition.z);
            return cellPos;
        }
    }
}

/// <summary>
///     represents a value inside a map which stores a chunk value
/// </summary>
public readonly struct Tile : ITile {
    public Tile(Vector3 worldPosition) {
        Debug.Log($"new Tile at '{worldPosition.x:F2} x {worldPosition.y:F2}'");
        WorldPosition = worldPosition;
        Chunk = new ITile.TileChunk(0);
    }

    public Vector3 WorldPosition { get; }

    /// <summary>
    ///     When computing the Tile, the Chunk is altered as the chunk is mutalb
    /// </summary>
    public ITile.TileChunk Chunk { get; }

    public void InstantiateTile(Transform parent) {
        Debug.Log($"Instantiating Tile '{WorldPosition.x:F2} x {WorldPosition.y:F2}'");

        // create the object that stores the contents of the Tile
        var instance = new GameObject("Tile")
        {
            transform =
            {
                position = WorldPosition
            }
        };
        instance.transform.SetParent(parent);

        Chunk.ForEach(cell => { cell.InstantiateCell(instance.transform); });
    }
}
}