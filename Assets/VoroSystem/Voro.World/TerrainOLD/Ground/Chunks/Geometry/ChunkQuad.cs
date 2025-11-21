using System;
using UnityEngine;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TerrainOLD.Ground.Chunks.Geometry {
[Serializable]
public class ChunkQuad : QuadBase {
  #region Serialized Fields

  public Mesh quadMesh;
  public QuadVertex[] vertices;
  [SerializeField] MeshCreation meshCreation;
  [SerializeField] QuadDisplacement quadDisplacement;

  #endregion

  public ChunkQuad(ITile tile) {
    meshCreation = new MeshCreation(this);
    quadMesh = meshCreation.CreateMesh(tile.Size, tile.Position);
    quadDisplacement = new QuadDisplacement(this);
  }

  /// <summary>
  /// heightmap texture displaces the vertices
  /// </summary>
  public void UpdateHeight(Texture2D tex) {
    quadDisplacement.DisplaceVertices(v => {
      var index = Array.IndexOf(vertices, v);
      if (index < 0 || tex == null) {
        return 0f;
      }

      var uv = quadMesh.uv[index];
      var sample = tex.GetPixelBilinear(uv.x, uv.y);
      var height = sample.r;
      return height;
    });
  }
}
}