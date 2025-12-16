namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// TerrainRegion samples stored height to produce float[]
/// =====
/// handles mapping between a tiles local mesh vertices and world-space height samples.
/// Bridges tile mesh layout and terrain height queries.
/// ---
/// stored height could be represented as a volume
/// sampling isolates a region of it
/// then using the spacing between each vertex, the actual height value is sampled
/// a height float array is returned
/// </summary>
public class TileHeightSampler { }
}