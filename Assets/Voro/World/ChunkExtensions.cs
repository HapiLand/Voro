using Voro.Grids;
using Voro.World.Internal;

namespace Voro.World {
public static class ChunkExtensions {
    /// <summary>
    ///     gets the chunk point positions in the world
    /// </summary>
    /// <param name="chunk">this chunk</param>
    /// <param name="coord">the coordinate of the Tile</param>
    /// <returns></returns>
    public static ChunkPoint[] ToWorldCoordinate(this Chunk chunk, Coordinate coord) {
        var array = new ChunkPoint[chunk.Points.Length];
        for (var i = 0; i < chunk.Points.Length; i++) {
            array[i] = new ChunkPoint
            {
                Position = chunk.Points[i].Position + coord.WorldPosition(),
                ID = chunk.Points[i].ID,
                Color = chunk.Points[i].Color
            };
        }

        return array;
    }
}
}