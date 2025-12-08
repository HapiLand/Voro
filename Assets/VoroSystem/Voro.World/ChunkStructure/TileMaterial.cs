using System;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class TileMaterial : ITileMaterial {
  #region Serialized Fields

  [SerializeField] MeshRenderer renderer;

  #endregion

  public TileMaterial(GameObject instance) {
    renderer = instance.AddComponent<MeshRenderer>();
    SetMaterial(Resources.Load<Material>("ChunkMaterial"));
    SetTexture(Texture2D.redTexture);
  }

  #region ITileMaterial Members

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