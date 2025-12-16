namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// stores height in its format (volume?)
/// =====
/// stores height data indexed in world-space sample coordinates.
/// responsible only for data storage and retrieval, not generation or rendering.
/// </summary>
public class TerrainHeightStorage {
  /*readonly float[,] _heightMap;
  readonly int _worldSize;
  public readonly float StepSize;

  public TerrainHeightStorage(int worldSize, float stepSize) {
    _worldSize = worldSize;
    StepSize = stepSize;
    var size = Mathf.CeilToInt(_worldSize / stepSize) + 1;
    _heightMap = new float[size, size];
    for (var x = 0; x < _heightMap.GetLength(0); x++) {
      for (var z = 0; z < _heightMap.GetLength(1); z++) {
        _heightMap[x, z] = Random.Range(-1f, 3f);
      }
    }
  }*/

  /*public (int minX, int minZ, int maxX, int maxZ) GetSampleBounds(TerrainRegion region) {
    var sizeX = _heightMap.GetLength(0);
    var sizeZ = _heightMap.GetLength(1);

    var minX = Mathf.Max(0, region.Position.x) * region.Resolution;
    var minZ = Mathf.Max(0, region.Position.y) * region.Resolution;
    var maxX = Mathf.Min(sizeX - 1, region.Position.x + region.Size) * region.Resolution;
    var maxZ = Mathf.Min(sizeZ - 1, region.Position.y + region.Size) * region.Resolution;

    return (minX, minZ, Mathf.CeilToInt(maxX), Mathf.CeilToInt(maxZ));
  }

  public float SampleHeightBilinear(
    int dx,
    int dz,
    int resolution) {
    var sizeX = _heightMap.GetLength(0);
    var sizeZ = _heightMap.GetLength(1);

    var sampleX = dx * (1f / resolution);
    var sampleZ = dz * (1f / resolution);

    var x0 = Mathf.FloorToInt(sampleX);
    var z0 = Mathf.FloorToInt(sampleZ);
    var x1 = Mathf.Min(x0 + 1, sizeX - 1);
    var z1 = Mathf.Min(z0 + 1, sizeZ - 1);

    var tx = sampleX - x0;
    var tz = sampleZ - z0;

    var h00 = _heightMap[x0, z0];
    var h10 = _heightMap[x1, z0];
    var h01 = _heightMap[x0, z1];
    var h11 = _heightMap[x1, z1];

    var hx0 = Mathf.Lerp(h00, h10, tx);
    var hx1 = Mathf.Lerp(h01, h11, tx);
    return Mathf.Lerp(hx0, hx1, tz);
  }*/
}
}