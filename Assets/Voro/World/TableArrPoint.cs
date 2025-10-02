using System;

namespace Voro.World {
/// <summary>
///     the data format for a point that matches the format of the point within Table.json
/// </summary>
[Serializable]
public class TableArrPoint {
    public float[] Col;
    public int Id;
    public float[] Pos;
}
}