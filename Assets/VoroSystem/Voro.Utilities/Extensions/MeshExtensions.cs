using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Util.Extensions {
public static class MeshExtensions {
  public static Vector3[] ToVector3Array(this CVertex[] vtx) {
    var array = new Vector3[vtx.Length];
    for (var i = 0; i < vtx.Length; i++) {
      array[i] = vtx[i].position;
    }

    return array;
  }
}
}