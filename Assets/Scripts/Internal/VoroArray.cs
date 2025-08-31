namespace Internal {
// ToDo make these Voros actually exist in the scene
public static class VoroArray {
    public static Voro[] Voros;

    static VoroArray() {
        // construct grid of voros
        Voros = new Voro[WorldGrid.ID.Length];
        for (var x = 0; x < WorldGrid.Dimensions[0]; x++) {
            for (var z = 0; z < WorldGrid.Dimensions[1]; z++) {
                Voros[WorldGrid.ID[x, z]] = new Voro("MyConfig", WorldGrid.PositionAt(x, z));
            }
        }
    }
}
}