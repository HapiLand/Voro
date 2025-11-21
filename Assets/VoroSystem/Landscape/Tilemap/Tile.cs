using VoroSystem.Landscape.Generate;

namespace VoroSystem.Landscape.Tilemap {
public struct Tile : ITile {
    /// <summary>
    /// Texture which holds a value for the elevation located at this <see cref="Tile" />
    /// </summary>
    HeightMapTexture _height;

    public void ReadTexture(HeightMapTexture tex) {
        _height = HeightMapTexture.Sample(this, tex);
    }

    public int Index { get; set; }

    public Tile(int index) {
        Index = index;
        _height = GenerationMgr.GetHeightmap();
    }
}
}