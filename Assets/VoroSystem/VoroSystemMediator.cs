using UnityEngine;
using VoroSystem.GraphEditor;
using VoroSystem.Terrain.Overseer;
using VoroSystem.WorldGrid;

namespace VoroSystem {
[ExecuteAlways]
public class VoroSystemMediator : MonoBehaviour {
    IDesigner _graphDesigner;
    IWorldGenerationOverseer _terrainWorldGenerationOverseer;
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

    public void InitializeTerrainGenerator() {
        if (!GetComponent<WorldMap>() || !GetComponent<GraphDesigner>()) {
            Debug.LogError("WorldMap or GraphDesigner not found");
            return;
        }

        if (GetComponent<WorldGenerationOverseer>()) {
            Debug.Log("Generator component already exists");
            return;
        }

        _terrainWorldGenerationOverseer = gameObject.AddComponent<WorldGenerationOverseer>();
    }
}
}