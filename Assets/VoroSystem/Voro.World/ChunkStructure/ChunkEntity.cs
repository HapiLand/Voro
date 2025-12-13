using System;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class ChunkEntity : IChunkEntity {
  #region Serialized Fields

  [SerializeField] Vector3 position;
  [SerializeField] GameObject entity;
  [SerializeField] ChunkMaterial chunkMaterial;
  [SerializeField] ChunkMesh chunkMesh;

  #endregion

  public ChunkEntity(Vector3 position, float meshSize, Transform parent, VoroMap map) {
    this.position = position;
    CreateInstance(parent, meshSize, map);
  }

  #region IChunkEntity Members

  public Vector3 Position => position;
  public GameObject Entity => entity;
  public ChunkMaterial ChunkMaterial => chunkMaterial;
  public ChunkMesh ChunkMesh => chunkMesh;

  #endregion

  void CreateInstance(Transform parent, float size, VoroMap map) {
    entity = new GameObject($"({Position.x:F0},{Position.z:F0})");
    entity.transform.SetParent(parent);
    entity.transform.position = Position;

    chunkMaterial = new ChunkMaterial(entity);
    chunkMesh = new ChunkMesh(entity, size, map);
  }

  public void SetTexture(Texture2D tex) {
    chunkMaterial.SetTexture(tex);
  }

  public Texture2D GetTexture() {
    return chunkMaterial.GetMaterial().mainTexture as Texture2D;
  }
}
}