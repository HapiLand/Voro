using UnityEngine;

namespace Internal.Grids {
public static class WorldGrid {
    public static readonly int[] Dimensions = { 10, 10 }; // width (x), height (z)
    public static readonly int[,] ID;

    static WorldGrid() {
        ID = new int[Dimensions[0], Dimensions[1]];
        var index = 0;

        for (var x = 0; x < Dimensions[0]; x++) {
            for (var z = 0; z < Dimensions[1]; z++) {
                ID[x, z] = index++;
            }
        }
    }

    public static Vector2 PositionAt(int x, int z) {
        if (x < 0 || x >= Dimensions[0] || z < 0 || z >= Dimensions[1]) {
            return Vector2.zero;
        }

        // returns the bottom-left corner for each tile
        return new Vector2(x, z);
    }
}
}