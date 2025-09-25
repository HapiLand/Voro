using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Voro.World {
public class TileMap {
    readonly Chunk[,] _map;
    int _pendingCount;

    public TileMap() {
        // set the initial VoroDiagram Map
        _map = new Chunk[_mapSize.width, _mapSize.length];
    }

    (int width, int length) _mapSize => (2, 2);

    public Chunk[,] GetDiagramMapArray() {
        return _map;
    }
    // public static event Action OnMapConstructed;

    /// <summary>
    ///     called once every VoroDiagram has been created
    /// </summary>
    public event Action DiagramMapConstructed;

    public void InstanceDiagramObjects(Transform parent) {
        // for each diagram, instance its GameObjects that it contains
        for (var x = 0; x < _mapSize.width; x++) {
            for (var z = 0; z < _mapSize.length; z++) {
                _map[x, z].WorldManagerOnOnCreatedAllDiagrams(parent);
            }
        }
    }

    public void CreateDiagramMap() {
        // Debug.Log("create diagram map");
        for (var x = 0; x < _mapSize.width; x++) {
            for (var z = 0; z < _mapSize.length; z++) {
                _map[x, z] = new Chunk();
                // create the tile in the diagram
                _map[x, z].CreateTile(x, z);
            }
        }

        // every diagram has been created
        // load the points and set them for all pending diagrams
        _pendingCount = _mapSize.width * _mapSize.length;

        for (var x = 0; x < _mapSize.width; x++) {
            for (var z = 0; z < _mapSize.length; z++) {
                LoadPointsIntoDiagram(_map[x, z]);
            }
        }

        return;

        void LoadPointsIntoDiagram(Chunk diagram) {
            AssetExtensions.LoadAssetPath<TextAsset>(
                "Assets/VoroWorld/PointDataLibrary/Table0.json",
                table => {
                    if (table != null) {
                        var tablePoints = JObject.Parse(table.text)["Points"].ToObject<TableArrPoint[]>();
                        diagram.SetCellPoints(tablePoints);
                    }

                    _pendingCount--;
                    if (_pendingCount == 0) {
                        // Debug.Log("All diagrams initialized");
                        // OnMapConstructed?.Invoke();
                        DiagramMapConstructed?.Invoke();
                    }
                }
            );
        }
    }
}
}