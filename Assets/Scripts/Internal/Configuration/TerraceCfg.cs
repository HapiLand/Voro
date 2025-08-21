namespace Internal.Configuration {
public struct TerraceCfg : IConfig {
    // this struct stores parameters for the Terrace instruction
    // creates a staircase effect for the point height
    public float[] ConfigArr { get; set; }
    public TerraceCfg(float[] configArr) {
        ConfigArr = configArr;
    }
}
}