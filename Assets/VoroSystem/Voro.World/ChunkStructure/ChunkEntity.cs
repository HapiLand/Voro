using System;
using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class ChunkEntity : IChunkEntity {
  #region Serialized Fields

  [SerializeField] Vector2 position;
  [SerializeField] GameObject entity;
  [SerializeField] ChunkMaterial chunkMaterial;
  [SerializeField] ChunkMesh chunkMesh;

  #endregion

  public ChunkEntity(Vector2 position, float meshSize, Transform parent, VoroMap map) {
    this.position = position;
    CreateInstance(parent, meshSize, map);
  }

  #region IChunkEntity Members

  public Vector2 Position => position;
  public GameObject Entity => entity;
  public ChunkMaterial ChunkMaterial => chunkMaterial;
  public ChunkMesh ChunkMesh => chunkMesh;
  public ComputeBuffer PointBuffer => chunkMesh.PointBuffer;


  public void UpdateHeight() {
    chunkMesh.UpdateHeight();
  }

  #endregion

  void CreateInstance(Transform parent, float size, VoroMap map) {
    entity = new GameObject($"({Position.x:F0},{Position.y:F0})");
    entity.transform.SetParent(parent);
    entity.transform.position = Position.ToVector3();

    chunkMaterial = new ChunkMaterial(entity);
    chunkMesh = new ChunkMesh(entity, size, map);
  }

  public void SetTexture(Texture2D tex) {
    chunkMaterial.SetTexture(tex);
  }

  public Texture2D GetTexture() {
    return chunkMaterial.GetMaterial().mainTexture as Texture2D;
  }

  /// <summary>
  /// applies the computed data to the points
  /// </summary>
  public void ReadHeightFromPointBuffer() {
    // get data from buffer
    var data = new MeshVertex.PointData[chunkMesh.PointBuffer.count];
    chunkMesh.PointBuffer.GetData(data);

    // write values to vertices
    chunkMesh.Apply(data);
  }

  public void ReleasePointBuffer() {
    chunkMesh.ReleasePointBuffer();
  }
}
}