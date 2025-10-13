namespace VoroSystem.GraphEditor.Effects.Parameters.Controls {
public readonly struct SliderConfig<T> where T : struct {
    /// <summary> min slider value </summary>
    public T Min { get; }

    /// <summary> max slider value </summary>
    public T Max { get; }

    /// <summary> current value </summary>
    public T Value { get; }

    public SliderConfig(T min, T max, T value) {
        Min = min;
        Max = max;
        Value = value;
    }
}
}