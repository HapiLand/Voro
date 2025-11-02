using System;

namespace VoroSystem.Core {
[Serializable]
public struct VoroInput {
    public int mapSizeX;
    public int mapSizeZ;
    public float slopeAmount;

    public VoroInput(int mapSizeX, int mapSizeZ, float slopeAmount) {
        this.mapSizeX = mapSizeX;
        this.mapSizeZ = mapSizeZ;
        this.slopeAmount = slopeAmount;
    }
}
}