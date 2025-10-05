using UnityEngine;

namespace VoroSystem.Terrain {
public class VertexInfo {
    public readonly Color[] Colors;
    public readonly int[] Pieces;
    public readonly Vector3[] Vertices;

    public VertexInfo(ComputeResult data) {
        Vertices = new Vector3[data.Points.Length];
        Colors = new Color[data.Points.Length];
        Pieces = new int[data.Points.Length];
        for (var i = 0; i < data.Points.Length; ++i) {
            Vertices[i] = data.Points[i].Position;
            Colors[i] = data.Points[i].Color;
            Pieces[i] = data.Points[i].ID;
        }
    }
}
}