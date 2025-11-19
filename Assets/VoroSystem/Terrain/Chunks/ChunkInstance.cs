using System;
using UnityEngine;
using VoroSystem.Terrain.Chunks.Geometry;

namespace VoroSystem.Terrain.Chunks {
/// <summary>
/// game object instance
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkInstance : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] MeshFilter mf;
  [SerializeField] MeshRenderer mr;

  /// <summary>
  /// quad mesh
  /// </summary>
  public ChunkQuad chunkQuad;

  public bool hasTexture;

  #endregion

  #region Event Functions

  void Awake() {
    mf ??= GetComponent<MeshFilter>();
    mr ??= GetComponent<MeshRenderer>();
  }

  void Start() {
    OnChunkCreated?.Invoke(this);
  }

  void Update() {
    if (!hasTexture) {
      // texture required for the chunk to update its height
      return;
    }

    var tex = mr.sharedMaterial.mainTexture as Texture2D;
    chunkQuad.UpdateHeight(tex);
    // todo fix seams
  }

  #endregion

  public static event Action<ChunkInstance> OnChunkCreated;

  public void Remove() {
    if (Application.isPlaying) {
      Destroy(gameObject);
    }
    else {
      DestroyImmediate(gameObject);
    }
  }
}
}