namespace Internal.Configuration {
public struct NoiseCfg : IConfig {
    // this struct stores parameters for the Noise instruction
    // to control how noise is combined into the height of the points
    public float[] ConfigArr { get; set; }
    public NoiseCfg(float[] configArr) {
        ConfigArr = configArr;
    }
}
}