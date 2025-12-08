using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Utilities;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.World.Components {
/// <summary>
/// VoroWorld represents all the objects that are instanced and exist in the landscape
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(VoroMap), typeof(VoroSpawner))]
public class VoroWorld : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroMap voroMap;
  [SerializeField] VoroSpawner voroSpawner;

  /// <summary>
  /// container that stores every Chunk that exists in the world
  /// </summary>
  [SerializeField] SerializableDictionary<int, Chunk> chunkDictionary = new();

  #endregion

  public static VoroWorld Instance { get; private set; }

  #region Event Functions

  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    name = "Voro World";
    voroMap = GetComponent<VoroMap>();
    voroSpawner = GetComponent<VoroSpawner>();
    VoroMap.CreatedChunk += HandleChunkCreated;
    voroMap.Init();
  }

  void OnDisable() {
    VoroMap.CreatedChunk -= HandleChunkCreated;
  }

  #endregion

  public void SetChunkTextures(Func<Chunk, Texture2D> textureFunc) {
    foreach (var chunk in chunkDictionary.Values) {
      chunk.SetTexture(textureFunc(chunk));
    }
  }

  void HandleChunkCreated(Chunk obj) {
    if (!chunkDictionary.TryAdd(obj.Index, obj)) {
      Debug.LogWarning($"Chunk [{obj.Index}] already exists");
    }
  }

  public IEnumerable<Chunk> GetAllChunks() {
    return chunkDictionary.Values;
  }
}
}