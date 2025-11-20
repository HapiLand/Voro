using UnityEngine;
using VoroSystem.Voro.World.Voro.Terrain.Ground.Chunks.Geometry;

namespace VoroSystem.Voro.World.Voro.Terrain {
public static class MeshExtensions {
  public static Vector3[] ToVector3Array(this QuadVertex[] vtx) {
    var array = new Vector3[vtx.Length];
    for (var i = 0; i < vtx.Length; i++) {
      array[i] = vtx[i].position;
    }

    return array;
  }
}
}