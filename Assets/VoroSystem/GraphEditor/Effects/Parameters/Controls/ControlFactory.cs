namespace VoroSystem.GraphEditor.Effects.Parameters.Controls {
public static class ControlFactory {
    static ITypedControl<T> Create<T>(string name, T value) {
        var param = new EffectParameter<T>(name, value);
        return new EffectParameterController<T>(name, param);
    }

    public static ITypedControl<float> FloatControl(string name, float startValue) {
        return Create(name, startValue);
    }

    public static ITypedControl<int> IntControl(string name, int startValue) {
        return Create(name, startValue);
    }

    public static ITypedControl<SliderConfig<float>> FloatSliderControl(string name, float min, float max,
        float startValue) {
        return Create(name, new SliderConfig<float>(min, max, startValue));
    }

    public static ITypedControl<bool> ToggleControl(string name, bool startValue) {
        return Create(name, startValue);
    }

    public static ITypedControl<SliderConfig<int>> IntSliderControl(string name, int min, int max, int startValue) {
        return Create(name, new SliderConfig<int>(min, max, startValue));
    }
}
}