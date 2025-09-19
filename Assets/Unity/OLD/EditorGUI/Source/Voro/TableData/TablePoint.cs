using System;

namespace EditorGUI.Source.Voro.TableData {
/// <summary>
///     the data format for a point that matches the format of the point within Table.json
/// </summary>
[Serializable]
public class TablePoint {
    public float[] Col;
    public int Id;
    public float[] Pos;
}
}