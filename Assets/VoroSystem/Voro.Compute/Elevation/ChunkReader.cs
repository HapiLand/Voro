using System;
using UnityEngine;
using VoroSystem.Util;
using VoroSystem.Voro.Designer.Graph;
using VoroSystem.Voro.World.Voro.Terrain.Ground.Chunks;

namespace VoroSystem.Voro.Compute.Elevation {
/// <summary>
/// Applies generated heightmaps to newly created chunks
/// </summary>
[Serializable]
public class ChunkReader {
  #region Serialized Fields

  [SerializeField] bool chunkCreationSubscription;
  [SerializeField] bool graphComputeSubscription;

  [SerializeField] SerializableHashSet<ChunkInstance> chunkInstances = new();
  [SerializeField] HeightmapGenerator heightmapGenerator = new();

  #endregion

  Material MaterialResource => Resources.Load<Material>("ChunkMaterial");

  public void Subscribe() {
    SubscribeToChunkCreation();
    SubscribeToGraphCompute();
    return;

    void SubscribeToChunkCreation() {
      if (chunkCreationSubscription) {
        return;
      }

      ChunkInstance.OnChunkCreated += HandleChunkCreated;
      chunkCreationSubscription = true;
    }

    void SubscribeToGraphCompute() {
      if (graphComputeSubscription) {
        return;
      }

      VoroCompute.OnDoCompute += HandleDoCompute;
      graphComputeSubscription = true;
    }
  }

  public void Unsubscribe() {
    UnsubscribeFromChunkCreation();
    SubscribeFromGraphCompute();
    return;

    void UnsubscribeFromChunkCreation() {
      if (!chunkCreationSubscription) {
        return;
      }

      ChunkInstance.OnChunkCreated -= HandleChunkCreated;
      chunkCreationSubscription = false;
    }

    void SubscribeFromGraphCompute() {
      if (!graphComputeSubscription) {
        return;
      }

      VoroCompute.OnDoCompute -= HandleDoCompute;
      graphComputeSubscription = false;
    }
  }

  /// <summary>
  /// Cache every new chunk that gets created
  /// </summary>
  /// <param name="instance"> new chunk </param>
  void HandleChunkCreated(ChunkInstance instance) {
    chunkInstances.Add(instance);
  }

  /// <summary>
  /// Computes the heightfield texture
  /// </summary>
  void HandleDoCompute(Graph graph) {
    // iterate through every chunk so the texture can be computed for the instance
    foreach (var ci in chunkInstances) {
      // var texture2D = heightmapGenerator.HandleDoCompute(graph, ci);
      var mr = ci.gameObject.GetComponent<MeshRenderer>();
      var materialInstance = new Material(MaterialResource)
      {
        mainTexture = heightmapGenerator.HandleDoCompute(graph, ci)
      };
      mr.sharedMaterial = materialInstance;
      ci.hasTexture = true;
    }
    // SetChunkMaterial();
  }
}
}