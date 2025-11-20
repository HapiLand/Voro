namespace VoroInternal {
public readonly struct InputToSystem {
    public InputToSystem(int widthMeters, int lengthMeters, float angleMedian, float angleMaximum, bool enablePath,
        float pathIrregularity, float flatRegionDensity, int flatRegionRelaxIterations, float flatRegionDiameter,
        float tileSize) {
        WidthMeters = widthMeters;
        LengthMeters = lengthMeters;
        AngleMedian = angleMedian;
        AngleMaximum = angleMaximum;
        EnablePath = enablePath;
        PathIrregularity = pathIrregularity;
        FlatRegionDensity = flatRegionDensity;
        FlatRegionRelaxIterations = flatRegionRelaxIterations;
        FlatRegionDiameter = flatRegionDiameter;
        TileSize = tileSize;
    }

    // size
    public int WidthMeters { get; }

    public int LengthMeters { get; }

    // angle
    public float AngleMedian { get; }

    public float AngleMaximum { get; }

    // path = true
    public bool EnablePath { get; }

    public float PathIrregularity { get; }

    // flat region spawn %
    public float FlatRegionDensity { get; }

    public int FlatRegionRelaxIterations { get; }

    // flat region size
    public float FlatRegionDiameter { get; }
    public float TileSize { get; }

    public static InputToSystem Default() {
        return new InputToSystem(
            1000,
            4000,
            45,
            60,
            true,
            0.3f,
            0.4f,
            6,
            6,
            1f);
    }
}
}