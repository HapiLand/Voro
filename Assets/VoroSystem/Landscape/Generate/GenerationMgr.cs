using System;
using VoroSystem.Landscape.World;

namespace VoroSystem.Landscape.Generate {
public static class GenerationMgr {
    /// <returns>Get the computed Heightmap Texture created from a Graph</returns>
    public static HeightMapTexture GetHeightmap() {
        throw new NotImplementedException();
    }

    /// <returns>Default Point Cloud filling world bounds</returns>
    static PointCloud GetPointCloud() {
        return PointCloud.CreateInstance()
            .SetVerticalOffset(true);
    }

    /// <returns>Displaced mesh for surface of Landscape</returns>
    static GroundMesh GetGroundMesh(PointCloud pointCloud) {
        // generate mesh grid,vertical raycast to
        // match point cloud ground-level elevation
        return new GroundMesh(1f)
            .RayVertical(pointCloud);
    }

    /// <returns>Gets the resulting Landscape from the generator</returns>
    public static Output<SmartObject, TileMesh, VoroPiece> GetLandscape() {
        // generate mesh grid,vertical raycast to
        // match point cloud ground-level elevation
        var tileMeshes = GetGroundMesh(GetPointCloud())
            .Fabricate(false);

        VoroWorld.GetSmartObjects(out var smartObjects);
        VoroWorld.GetVoroPieces("Pieces", out var voroPieces);
        return new Output<SmartObject, TileMesh, VoroPiece>(smartObjects, tileMeshes, voroPieces);
    }
}
}