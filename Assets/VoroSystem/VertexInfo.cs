using UnityEngine;

namespace VoroSystem {
public class VertexInfo {
    public readonly Color[] Colors;
    public readonly int[] IDs;
    public readonly Vector3[] Vertices;

    public VertexInfo(ComputeResult data) {
        Vertices = new Vector3[data.Points.Length];
        Colors = new Color[data.Points.Length];
        IDs = new int[data.Points.Length];
        for (var i = 0; i < data.Points.Length; ++i) {
            Vertices[i] = data.Points[i].Position;
            Colors[i] = data.Points[i].Color;
            IDs[i] = data.Points[i].ID;
        }
    }
}
}