using UnityEngine;

namespace Voro.Generation {
public struct WorldPoint {
    public static void CreateInstance(ParsedPoint parsedPoint, out WorldPoint worldPoint) {
        worldPoint = new WorldPoint(
            new Vector3(parsedPoint.Pos[0], 0, parsedPoint.Pos[1]),
            parsedPoint.Id,
            new Color(parsedPoint.Col[0], parsedPoint.Col[1], parsedPoint.Col[2], 1.0f));
    }

    public Vector3 Position;
    public int ID;
    public Color Color;

    WorldPoint(Vector3 position, int id, Color color) {
        Position = position;
        ID = id;
        Color = color;
    }

    public GameObject GetMeshObject() {
        var instance = new GameObject($"{ID}");
        instance.transform.position = Position;
        var meshFilter = instance.AddComponent<MeshFilter>();
        var meshRenderer = instance.AddComponent<MeshRenderer>();

        // set mesh in object
        var variant = 0;
        meshFilter.sharedMesh = Resources.Load<Mesh>($"Mesh/{ID}_{variant}");

        // set material
        var originalMat = Resources.Load<Material>("FbxMat");
        var mat = new Material(originalMat);
        mat.color = Color;
        meshRenderer.material = mat;

        return instance;
    }
}
}