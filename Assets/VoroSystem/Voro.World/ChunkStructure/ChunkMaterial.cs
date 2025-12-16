using System;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class ChunkMaterial : IChunkMaterial {
  #region Serialized Fields
  [SerializeField] MeshRenderer renderer;
  #endregion

  public ChunkMaterial(GameObject instance) {
    renderer = instance.AddComponent<MeshRenderer>();
    SetMaterial(Resources.Load<Material>("ChunkMaterial"));
    SetTexture(Texture2D.whiteTexture);
  }

  #region IChunkMaterial Members
  public MeshRenderer Renderer => renderer;

  public void SetMaterial(Material mat) {
    renderer.sharedMaterial = new Material(mat);
  }

  public void SetTexture(Texture2D tex) {
    renderer.sharedMaterial.mainTexture = tex;
  }

  public Material GetMaterial() {
    return renderer.sharedMaterial;
  }
  #endregion
}
}