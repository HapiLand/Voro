using UnityEngine;

namespace VoroSystem.TilemapSystem {
/// <summary> Does the Runtime behaviour for the Tilemap system </summary>
[ExecuteInEditMode]
public class BasicTilemapComponent : MonoBehaviour {
    public BasicTilemapComponent() {
        MapEffectSystem = new MapEffectSystem(this);
    }

    public TilemapParameters TilemapParameters { get; } = new();
    public CompMap CompMap { get; } = new();
    public MapEffectSystem MapEffectSystem { get; }

    void Update() {
        /* change tile mesh color indicating visibility
           this also gives the opportunity to regen the mesh each frame
           as this will be useful for altering effects live
           apply the new color when building the mesh
        */
        CompMap.Tilemap.ForEach(tile => { tile.Update(); });
    }

    /// <summary> Observe changes in the Tilemap dimensions, generate the correct size map </summary>
    void OnValidate() {
        Debug.Log("Validating Tilemap");
    }
}
}