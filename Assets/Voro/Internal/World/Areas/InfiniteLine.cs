using UnityEngine;

namespace Voro.Internal.World.Areas {
/// <summary>
/// horizontal line of infinite length, along the line is a basic area
/// infinitely horizontal line within the world, GridTiles are created around this
/// TiledLine acts as a set of points at a world position
/// tiles exist around a radius of each point
/// </summary>
public class InfiniteLine {
    /* todo 
     * provide player position, determine if position is near the line
     * if nearby, find the BasicArea
     * this is to allow GridTiles to exist at the players location
     * wherever they are along the line
     */
    public LineDirection Direction;
    public InfiniteLine(LineDirection direction) {
        Direction = direction;
    }
    public enum LineDirection {
        XAxis,
        ZAxis
    }
}
}