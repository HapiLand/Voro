using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem.Terrain {
public static class WorldBuilder {
    /// <summary>
    ///     instantiates the mesh data into the unity scene
    ///     the mesh data is used to construct game objects in order for the geometry to appear in the scene
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="meshData">3d geometry to produce game objects</param>
    public static void GenerateWorldMap(this Transform parent, MeshData meshData) {
        Debug.Log("Generating World map");
        var sw = new Stopwatch();
        sw.Start();

        foreach (var item in meshData.Data) {
            var instance = new GameObject("MeshPiece")
            {
                transform =
                {
                    position = item.pos
                }
            };
            instance.transform.SetParent(parent);
            if (instance is not null) {
                var meshFilter = instance.AddComponent<MeshFilter>();
                var meshRenderer = instance.AddComponent<MeshRenderer>();

                // set mesh in object
                meshFilter.sharedMesh = item.mesh;

                // set material
                var originalMat = Resources.Load<Material>("FbxMat");
                var mat = new Material(originalMat)
                {
                    color = item.col,
                };
                meshRenderer.material = mat;
            }
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to generate the world map");
    }
}
}