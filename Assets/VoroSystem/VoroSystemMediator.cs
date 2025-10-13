using UnityEngine;
using VoroSystem.GraphEditor;
using VoroSystem.GraphEditor.UserInterface;
using VoroSystem.WorldGrid;

namespace VoroSystem {
[ExecuteAlways]
public class VoroSystemMediator : MonoBehaviour {
    IDesigner _graphDesigner;

    // ITerrainGenSystem _terrainDesigner;
    IWorld _worldMap;

    public void InitializeMapDesigner() {
        if (GetComponent<WorldMap>()) {
            Debug.Log("WorldMap component already exists");
            return;
        }

        _worldMap = gameObject.AddComponent<WorldMap>();
    }

    public void InitializeGraphDesigner() {
        if (GetComponent<GraphDesigner>()) {
            Debug.Log("GraphDesigner component already exists");
            return;
        }

        _graphDesigner = gameObject.AddComponent<GraphDesigner>();
    }

    /*
    public void InitializeTerrainDesigner() {
        if (!GetComponent<WorldMap>() || !GetComponent<GraphDesigner>()) {
            Debug.LogError("MapDesigner or GraphDesigner not found");
            return;
        }

        if (GetComponent<TerrainDesigner>()) {
            Debug.Log("TerrainDesigner component already exists");
            return;
        }

        _terrainDesigner = gameObject.AddComponent<TerrainDesigner>();
    }*/
}
}