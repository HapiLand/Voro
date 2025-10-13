using UnityEngine;

namespace VoroSystem.WorldGrid {
public interface ICell : IPoint {
    int ID { get; }
    Color Color { get; }
    Mesh Piece { get; }
    void InstantiateCell(Transform parent);
}

public interface IPoint {
    Vector3 WorldPosition { get; }
}

/// <summary>
///     represents a mesh piece for a point inside a Chunk
/// </summary>
public readonly struct Cell : ICell {
    public Cell(float[] position, int id, float[] color) {
        WorldPosition = new Vector3(position[0], 0, position[1]);
        ID = id;
        Color = new Color(color[0], color[1], color[2], 1.0f);
        Piece = AssetLoader.LoadMeshPiece(ID);
    }

    public Vector3 WorldPosition { get; }
    public int ID { get; }
    public Color Color { get; }
    public Mesh Piece { get; }

    public void InstantiateCell(Transform parent) {
        Debug.Log($"Instantiating Cell.Piece '{ID}' at '{WorldPosition.x:F2} x {WorldPosition.y:F2}'");

        var instance = new GameObject($"Cell {ID}")
        {
            transform =
            {
                position = WorldPosition
            }
        };
        instance.transform.SetParent(parent, false);

        // set mesh in instance
        var meshFilter = instance.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = Piece;

        // set material in instance
        var meshRenderer = instance.AddComponent<MeshRenderer>();
        var originalMat = Resources.Load<Material>("FbxMat");
        var mat = new Material(originalMat)
        {
            color = Color
        };
        meshRenderer.material = mat;
    }
}
}