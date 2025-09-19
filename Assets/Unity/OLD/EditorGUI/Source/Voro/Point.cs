using UnityEngine;

namespace EditorGUI.Source.Voro {
public struct Point {
    public Vector3 Position;
    public int ID;

    public Point(Vector3 position, int id) {
        Position = position;
        ID = id;
    }
}
}