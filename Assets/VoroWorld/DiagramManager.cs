using System;
using Newtonsoft.Json.Linq;
using Source.Utility;
using UnityEngine;
using VoroWorld.PointDataLibrary;

namespace VoroWorld {
public class DiagramManager {
    readonly VoroDiagram[,] _map;
    int _pendingCount;

    public DiagramManager() {
        // set the initial VoroDiagram Map
        _map = new VoroDiagram[_mapSize.width, _mapSize.length];

        // call when the WorldManager is ready for the diagrams to be constructed
        WorldManager.OnWorldManagerAwake += CreateDiagramMap;
    }

    (int width, int length) _mapSize => (2, 2);
    public static event Action OnMapConstructed;


    void CreateDiagramMap() {
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
                        OnMapConstructed?.Invoke();
                    }
                }
            );
        }
    }
}
}