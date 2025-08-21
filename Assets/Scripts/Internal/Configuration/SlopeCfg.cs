namespace Internal.Configuration {
public struct SlopeCfg : IConfig {
    // this struct stores parameters for the Slope instruction
    // to set the point height as a linear gradient
    public float[] ConfigArr { get; set; }
    public SlopeCfg(float[] configArr) {
        ConfigArr = configArr;
    }
}
}