using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public static class WorldBuilder {
    /// <summary>
    ///     instantiates the mesh data into the unity scene
    ///     the mesh data is used to construct game objects in order for the geometry to appear in the scene
    /// </summary>
    /// <param name="meshData">3d geometry to produce game objects</param>
    public static void GenerateWorldMap(Transform parent, MeshData meshData) {
        Debug.Log("Generating World map");
        var sw = new Stopwatch();
        sw.Start();

        for (var i = 0; i < meshData.Data.Length; i++) {
            var item = meshData.Data[i];

            var instance = new GameObject("MeshPiece");
            instance.transform.position = item.pos;
            instance.transform.SetParent(parent);
            var meshFilter = instance.AddComponent<MeshFilter>();
            var meshRenderer = instance.AddComponent<MeshRenderer>();

            // set mesh in object
            meshFilter.sharedMesh = item.mesh;

            // set material
            var originalMat = Resources.Load<Material>("FbxMat");
            var mat = new Material(originalMat)
            {
                color = item.col
            };
            meshRenderer.material = mat;
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to generate the world map");
    }
}
}