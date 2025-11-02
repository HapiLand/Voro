using UnityEngine;
using VoroSystem.Landscape.World;

namespace VoroSystem {
class VoroBoundingBox {
    readonly Voro _voro;

    public VoroBoundingBox(Voro voro) {
        _voro = voro;
    }

    /// <summary>
    /// Set bounding box
    /// </summary>
    public void InitWorld() {
        SetBounds(_voro.VoroInputValue.InputValues.WidthMeters, _voro.VoroInputValue.InputValues.LengthMeters);
    }

    /// <summary>
    /// Set the Bounding Box of Landscape
    /// </summary>
    /// <param name="width">in meters X axis</param>
    /// <param name="length">in meters Y axis</param>
    void SetBounds(int width, int length) {
        VoroWorld.CreateBounds(Vector2.zero, width, length);
    }
}
}