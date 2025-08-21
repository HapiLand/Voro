namespace Internal.Configuration {
public struct TerraceCfg : IConfig {
    public float[] ConfigArr { get; set; }
    public TerraceCfg(float[] configArr) {
        ConfigArr = configArr;
    }
}
}