namespace Voro.World.Internal {
struct Dimension {
    public readonly int XSize;
    public readonly int ZSize;

    public Dimension(int x, int z) {
        XSize = x;
        ZSize = z;
    }
}
}