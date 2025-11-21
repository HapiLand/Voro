using System;
using UnityEngine;
using VoroSystem;
using VoroSystem.Landscape.Generate;
using VoroSystem.Landscape.World;

namespace VoroInternal {
/// <summary>
/// Runtime.
/// </summary>
public class Program : MonoBehaviour {
    bool _dirty;

    /// <summary>
    /// The world.
    /// </summary>
    Output<SmartObject, TileMesh, VoroPiece> _landscape;

    /// <summary>
    /// World engine.
    /// </summary>
    Voro _voro;

    /// <summary>
    /// Prepare the world.
    /// Initialize variables and states before running.
    /// </summary>
    void Awake() {
        var input = InputToSystem.Default();
        _voro = Voro.CreateInstance(new VoroFlags());
        _voro.VoroInitializer.Initialize(input);
        _voro.VoroBoundingBox.InitWorld();
        _voro.VoroMap.InitTilemap();
        _voro.VoroGraph.InitGraphs();
    }

    /// <summary>
    /// Revert all derived input values to defaults.
    /// </summary>
    void Reset() {
        _voro.VoroInputValue.RevertToDefaults();
    }

    /// <summary>
    /// Called first time program starts.
    /// All Awake methods must be called first.
    /// Ready up the world, to await generation cycle.
    /// </summary>
    void Start() {
        _voro.Begin(false);
        BeginGenerate();
        BeginTilemap();
        BeginWorld();
        BeginGraphs();
        return;

        void BeginGenerate() {
            // Clear texture
            // New point cloud
            // New ground mesh
        }

        void BeginTilemap() {
            // New tile map
        }

        void BeginWorld() {
            // Load voro piece assets
        }

        void BeginGraphs() {
            // Open designer
        }
    }

    /// <summary>
    /// Fabricate the world.
    /// </summary>
    void Update() {
        if (!_dirty) {
            return;
        }

        _landscape = _voro.CreateLandscape();
        _landscape.Instantiate();

        UpdateGenerate();
        UpdateTilemap();
        return;

        void UpdateGenerate() {
            // Compute texture
            // Construct output
            // Displace ground mesh
        }

        void UpdateTilemap() {
            // Set visibility
        }
    }

    /// <summary>
    /// Validate fabricated world.
    /// Smart elevation.
    /// </summary>
    void LateUpdate() {
        _dirty = false;
    }

    /// <summary>
    /// World settings.
    /// </summary>
    void OnGUI() {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Global debug visualisers.
    /// </summary>
    void OnDrawGizmos() {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Refresh the world.
    /// </summary>
    void OnValidate() {
        _dirty = true;
    }
}
}