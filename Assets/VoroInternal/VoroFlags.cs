namespace VoroInternal {
readonly struct VoroFlags {
    public bool UseManualConstruction => true;
    public int LayerCount => 1;
    public string DefaultLayerName => "NO NAME";
    public int DefaultEffect => 0; // slope
    public string DefaultConfiguration => "Config0";
    public bool AllowUpdate => false;
    public bool ClipMaxWorldSize => true;
    public int MaxWorldSize => 10;
}
}