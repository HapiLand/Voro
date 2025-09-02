namespace Internal {
/// <summary>
///     interface for any configuration that is stored in the json
/// </summary>
public interface IConfiguration {
    /// <summary>
    ///     stores the values that this configuration use
    /// </summary>
    float[] PropertiesArray { get; set; }
}
}