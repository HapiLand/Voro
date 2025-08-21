namespace Internal.Configuration {
public struct SlopeCfg : IConfig {
    public float[] ConfigArr { get; set; }
    public SlopeCfg(float[] configArr) {
        ConfigArr = configArr;
    }
}
}