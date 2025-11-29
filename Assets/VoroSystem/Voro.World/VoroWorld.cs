using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Util;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.World {
/// <summary>
/// VoroWorld represents all the objects that are instanced and exist in the landscape
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(VoroMap), typeof(VoroSpawner))]
public class VoroWorld : MonoBehaviour {
    void HandleChunkCreated(Chunk obj) {
        if (!chunks.TryAdd(obj.Index, obj)) {
            Debug.LogWarning($"Chunk [{obj.Index}] already exists");
        }
    }

    public IEnumerable<Chunk> GetAllChunks() {
        return chunks.Values;
    }

    #region Serialized Fields
    [SerializeField] VoroMap voroMap;
    [SerializeField] VoroSpawner voroSpawner;

    /// <summary>
    /// container that stores every Chunk that exists in the world
    /// </summary>
    [SerializeField] SerializableDictionary<int, Chunk> chunks = new();
    #endregion

    #region Event Functions
    void Awake() {
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
}
}