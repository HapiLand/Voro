namespace VoroSystem.Util {
public static class HelperUtility {
    public static int GetIndex(int x, int z, int sizeX) {
        return z * sizeX + x;
    }
}
}