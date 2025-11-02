using UnityEngine;

namespace Usage {
/// <summary>
/// Demonstration of how to use Voro
/// </summary>
public class Demo : MonoBehaviour {
    [SerializeField] VoroConfig config;

    /// <summary>Allows lifecycle/game events to talk to Voro.</summary>
    VoroSceneBehavior _voroCommander;

    /// <summary>Initialize the core system, default state.</summary>
    void Awake() {
        _voroCommander = new VoroSceneBehavior(false);
        _voroCommander.Events.WorldGenerated += OnGenerationCompleted;
    }

    /// <summary>Design the actual world.</summary>
    void Start() {
        _voroCommander.SetConfiguration(config);
        _voroCommander.SetInitialSpawn(Vector3.zero);
        _voroCommander.Events.RaiseToCompleteSetup();
        
        
    }

    void OnGenerationCompleted(Vector3 position) {
        Debug.Log($"Spawn Player character at: '{position}'");
    }
}
}