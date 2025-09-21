using UnityEngine;

namespace VoroWorld.Generation {
/// <summary>
///     point data that VoroCompute stores
/// </summary>
public struct CellPoint {
    public Vector3 Position;
    public int ID;
    public Color Color;
    public Vector3 Origin; // in order to convert between local and world space
}
}