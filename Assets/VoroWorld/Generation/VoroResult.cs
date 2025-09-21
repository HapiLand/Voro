using UnityEngine;

namespace VoroWorld.Generation {
// todo choo choo, the voro result is heading to the mesh factory
/// <summary>
///     the output computed data
///     this will now be turned into the Unity types to
///     be used by the Unity Engine
/// </summary>
public struct VoroResult {
    public CellPoint[] Points;
}

public struct CellPoint {
    public Vector3 Position;
    public int ID;
    public Color Color;
    public Vector3 Origin; // in order to convert between local and world space
}
}