using UnityEngine;

namespace VoroSystem {
public class MeshData {
    public (Mesh mesh, Vector3 pos, Color col)[] Data;

    public MeshData(VertexInfo vtxInfo) {
        Data = new (Mesh, Vector3, Color)[vtxInfo.Vertices.Length];
        // for every vertex, load the .fbx instance that matches its ID
        for (var i = 0; i < Data.Length; i++) {
            var mesh = AssetLoader.GetMeshPiece(vtxInfo.IDs[i]);
            var pos = vtxInfo.Vertices[i];
            var col = vtxInfo.Colors[i];
            Data[i] = (mesh, pos, col);
        }

        Debug.Log($"produced {Data.Length} mesh pieces");
    }
}
}