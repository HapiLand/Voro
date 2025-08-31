using UnityEngine;

namespace Internal {
// ToDo make game manager store the player position and a draw distance, for toggling
public static class WorldGrid {
    // ToDo dimensions as 100x100, only visible Voros are drawn
    public static readonly int[] Dimensions = { 5, 5 }; // width (x), height (z)
    static readonly float[] Size = { 1f, 1f }; // cell width, cell height

    public static readonly int[,] ID;
    // ToDo bool to toggle each point, construct Voro when first set to true

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

        // return the bottom-left corner
        var posX = x * Size[0] + Size[0] / 2f - x / 2f;
        var posZ = z * Size[1] + Size[1] / 2f - z / 2f;
        return new Vector2(posX, posZ);
    }
}
}