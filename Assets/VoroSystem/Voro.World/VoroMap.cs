using System;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.World {
[ExecuteAlways]
public class VoroMap : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] Tilemap<Chunk> map;
  [SerializeField] [Range(0.1f, 1f)] float chunkSize = 1f;
  [SerializeField] public Vector2 cornerA = new(0, 0);
  [SerializeField] public Vector2 cornerB = new(10, 10);

  #endregion

  int MapSizeX => Mathf.RoundToInt(Mathf.Abs(cornerA.x - cornerB.x));
  int MapSizeZ => Mathf.RoundToInt(Mathf.Abs(cornerB.y - cornerA.y));

  #region Event Functions

  void Update() {
    ForEach(c => c.Update());
  }

  #endregion

  public void Init() {
    CreateMap();
  }

  public static event Action<Chunk> CreatedChunk;

  void CreateMap() {
    map = new Tilemap<Chunk>(chunkSize, MapSizeX, MapSizeZ, (index, pos) => {
      var chunk = new Chunk(index, pos, chunkSize);
      CreatedChunk?.Invoke(chunk);
      return chunk;
    });
  }

  public void ForEach(Action<Chunk> action) {
    if (map == null) {
      Debug.LogWarning("Tilemap does not exist");
      return;
    }

    map.ForEach(action);
  }
}
}