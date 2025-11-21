using System;
using VoroSystem.Landscape.Tilemap;

namespace VoroSystem.Landscape.Generate {
/// <summary>
/// Texture to elevate a point cloud with
/// </summary>
public class HeightMapTexture {
    /// <summary>
    /// Cut out a region from a source Texture to create a cropped texture
    /// </summary>
    /// <param name="location">Location to cut out at</param>
    /// <param name="source">The source texture</param>
    /// <returns></returns>
    public static HeightMapTexture Sample(Tile location, HeightMapTexture source) {
        throw new NotImplementedException();
    }
}
}