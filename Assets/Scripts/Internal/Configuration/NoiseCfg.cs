namespace Internal.Configuration {
public struct NoiseCfg : IConfig {
    public float[] ConfigArr { get; set; }
    public NoiseCfg(float[] configArr) {
        ConfigArr = configArr;
    }
}
}