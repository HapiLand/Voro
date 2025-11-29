using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure {
/// <summary>
/// implementation for the heightmap of a Tile
/// </summary>
public interface ITileMaterial {
    MeshRenderer Renderer { get; }
    Material GetMaterial();
    void SetMaterial(Material mat);
    Texture2D GetTexture();
    void SetTexture(Texture2D tex);
}
}