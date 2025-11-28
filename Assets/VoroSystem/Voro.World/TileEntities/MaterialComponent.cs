using System;
using UnityEngine;

namespace VoroSystem.Voro.World.TileEntities {
[Serializable]
public class MaterialComponent : MonoBehaviour {
  MeshRenderer _renderer;
  Material BaseMaterial => Resources.Load<Material>("ChunkMaterial");

  public void Initialize() {
    _renderer = gameObject.GetComponent<MeshRenderer>();
    ApplyMaterial();
  }

  void ApplyMaterial() {
    var instance = new Material(BaseMaterial);
    _renderer.sharedMaterial = instance;
    _renderer.sharedMaterial.mainTexture = Texture2D.blackTexture;
  }
}
}