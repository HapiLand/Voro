using System;
using UnityEngine;

namespace Usage {
public sealed class VoroSceneEvents {
    public event Action<Vector3> WorldGenerated;
    internal void RaiseWorldGenerated(Vector3 position) => WorldGenerated?.Invoke(position);
    
    public event Action WorldSetupComplete;
    internal void RaiseToCompleteSetup() => WorldSetupComplete?.Invoke();
}

public sealed class VoroConfigManager {
    /// <summary>Provides system with a world generation template.</summary>
    /// <param name="preset">Custom template produced by the Editor GUI.</param>
    public void Apply(VoroConfig preset) {
        Debug.Log($"Setting Configuration: '{preset.configName}'");
    }
}

public sealed class VoroWorldSpawn {
    /// <summary>Set origin point for world generation.</summary>
    /// <param name="position">Initial location to spawn the player.</param>
    public void SetSpawn(Vector3 position) {
        Debug.Log($"Setting World Origin: '{position}'");
    }
}

public sealed class VoroSceneBehavior {
    readonly VoroConfigManager _config;
    readonly VoroWorldSpawn _spawner;

    /// <summary>Initializes a new default Voro.</summary>
    /// <param name="debugMode">Enable gizmos & profiling.</param>
    public VoroSceneBehavior(bool debugMode) {
        Debug.Log($"Create Voro instance - Initializing Core (Debug Mode: {debugMode})");
        Events = new VoroSceneEvents();
        _config = new VoroConfigManager();
        _spawner = new VoroWorldSpawn();
    }
    public VoroSceneEvents Events { get; }

    public void SetConfiguration(VoroConfig preset) => _config.Apply(preset);
    public void SetInitialSpawn(Vector3 position) => _spawner.SetSpawn(position);
}
}