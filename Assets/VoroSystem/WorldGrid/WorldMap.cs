using System;
using UnityEngine;

namespace VoroSystem.WorldGrid {
public interface IMap<T> where T : ITile {
    T this[int x, int y] { get; set; }
    T this[int index] { get; }
    Vector2Int Size { get; }
    void ForEach(Action<T> action);
}

public interface IWorld : IMap<ITile> {
    bool HasMap { get; set; }
    void GenerateMapArray();
    void SetMapSize(int x, int y);
    void InstantiateMap();
}

[ExecuteAlways]
public class WorldMap : MonoBehaviour, IWorld {
    #region Map Size

    [SerializeField] int _sizeX = 1;
    [SerializeField] int _sizeY = 1;

    int WorldMapSizeX {
        get => _sizeX;
        set => _sizeX = Mathf.Max(1, value);
    }

    int WorldMapSizeY {
        get => _sizeY;
        set => _sizeY = Mathf.Max(1, value);
    }

    public Vector2Int Size => new(WorldMapSizeX, WorldMapSizeY);

    #endregion


    #region Tiles

    ITile[,] _tiles;

    public ITile this[int x, int y] {
        get => _tiles[x, y];
        set => _tiles[x, y] = value;
    }

    public ITile this[int index] {
        get
        {
            var x = index % Size.x;
            var y = index / Size.x;
            return _tiles[x, y];
        }
    }

    public void ForEach(Action<ITile> action) {
        for (var y = 0; y < Size.y; y++) {
            for (var x = 0; x < Size.x; x++) {
                action(_tiles[x, y]);
            }
        }
    }

    public void GenerateMapArray() {
        if (_tiles == null) {
            Debug.LogError("WorldMap.Tiles does not exist - Cannot generate Tiles");
            return;
        }

        for (var y = 0; y < Size.y; y++) {
            for (var x = 0; x < Size.x; x++) {
                if (this[x, y] != null) {
                    Debug.LogWarning($"WorldMap.Tile already exists at '{x} x {y}'");
                    continue;
                }

                var worldPosition = new Vector3(x, 0, y);
                Debug.LogWarning($"Creating new WorldMap.Tile at '{worldPosition.x:F2} x {worldPosition.y:F2}'");
                this[x, y] = new Tile(worldPosition);
                HasMap = true;
            }
        }
    }

    public void SetMapSize(int x, int y) {
        if (_tiles == null) {
            Debug.LogWarning($"WorldMap.Tiles not set - Creating new array '{x} x {y}'");

            WorldMapSizeX = x;
            WorldMapSizeY = y;
            _tiles = new ITile[WorldMapSizeX, WorldMapSizeY];
            return;
        }

        if (WorldMapSizeX == x && WorldMapSizeY == y) {
            // unchanged
            return;
        }

        Debug.Log($"Setting new WorldMap.Tiles: OLD Size = '{WorldMapSizeX} x {WorldMapSizeY}' NEW Size = '{x} x {y}'");
        WorldMapSizeX = x;
        WorldMapSizeY = y;
        _tiles = new ITile[WorldMapSizeX, WorldMapSizeY];
    }

    public void InstantiateMap() {
        Debug.Log($"Instantiating WorldMap '{WorldMapSizeX} x {WorldMapSizeY}'");

        // set the parent object that the Tile is instantiated into
        var parent = transform;

        ForEach(tile => { tile.InstantiateTile(parent); });
    }

    public bool HasMap { get; set; }

    #endregion
}
}