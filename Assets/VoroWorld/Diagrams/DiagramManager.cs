using System;
using Newtonsoft.Json.Linq;
using Source.Utility;
using UnityEngine;
using VoroWorld.Generation;

namespace VoroWorld.Diagrams {
public class DiagramManager {
    readonly VoroDiagram[,] _map;
    int _pendingCount;

    public DiagramManager() {
        // set the initial VoroDiagram Map
        _map = new VoroDiagram[_mapSize.width, _mapSize.length];
    }

    (int width, int length) _mapSize => (2, 2);

    public void AppendResultsToMap(VoroResult[,] results) {
        Debug.Log("Applying Computed Result to the Diagram Map");
        // the output from VoroCompute is used to generate these results
        // this holds the new value to be appended, this will show the new terrain

        var resultWidth = results.GetLength(0);
        var resultLength = results.GetLength(1);

        if (resultWidth != _mapSize.width || resultLength != _mapSize.length) {
            Debug.LogError("Result width and length do not match");
            Debug.LogError($"Width Result:{resultWidth} Map:{_mapSize.width}");
            Debug.LogError($"Length Result:{resultLength} Map:{_mapSize.length}");
            return;
        }

        // append the map with the result
        for (var x = 0; x < resultWidth; x++) {
            for (var z = 0; z < resultLength; z++) {
                // get the Map VoroDiagram 
                var diagram = _map[x, z];
                AppendResultHeightToDiagram(x, z, diagram);

                // update the diagram game objects so the mesh assets are at the correct location
                for (var i = 0; i < diagram.Tile.Container.transform.childCount; i++) {
                    var cellObject = diagram.Tile.Container.transform.GetChild(i);

                    // the computed position
                    var pos = results[x, z].Points[i].Position;
                    // debug to get the position, it must be in local space
                    // Debug.Log($"From Result: {results[x, z].Points[i].Position} (converted to local) ({results[x, z].Points[i].Position.x - results[x, z].Points[i].Origin.x} {results[x, z].Points[i].Position.z - results[x, z].Points[i].Origin.z})");

                    // var tileOrigin = diagram.Tile.Position;
                    // cellObject.position = new Vector3(pos.x + tileOrigin.x, pos.y, pos.z + tileOrigin.z);
                    cellObject.position = new Vector3(pos.x, pos.y, pos.z);
                }
            }
        }


        void AppendResultHeightToDiagram(int x, int z, VoroDiagram diagram) {
            // get the Result VoroDiagram
            var result = results[x, z];

            // copy the computed height in each result point
            // to the current height in each diagram point
            for (var i = 0; i < diagram.CellPoints.Length; i++) {
                var current = diagram.CellPoints[i];
                var computed = result.Points[i];

                // write the computed height to the point
                var newPosition = current.Position;
                newPosition.y = computed.Position.y;

                // append the computed result to the point
                diagram.CellPoints[i].Position = newPosition;
            }
        }
    }

    public VoroDiagram[,] GetDiagramMapArray() {
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
                _map[x, z] = new VoroDiagram();
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

        void LoadPointsIntoDiagram(VoroDiagram diagram) {
            AssetUtil.LoadAssetPath<TextAsset>(
                "Assets/VoroWorld/PointDataLibrary/Table0.json",
                table => {
                    if (table != null) {
                        var tablePoints = JObject.Parse(table.text)["Points"].ToObject<TablePoint[]>();
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