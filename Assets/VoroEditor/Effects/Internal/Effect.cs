using System;
using VoroEditor.Elements;
using VoroEditor.Source;
using VoroEditor.Utility;
using Display = VoroEditor.Elements.Display;

namespace VoroEditor.Effects.Internal {
/// <summary>
///     abstract generic base class for effects which use a specific type of data
///     the foundation for different effect types that have their own configuration
/// </summary>
/// <typeparam name="TEffectData"></typeparam>
public abstract class Effect<TEffectData> : IEffect {
    protected TEffectData Data;

    public Effect(string name, TEffectData data) {
        EffectName = name;
        Data = data;
    }

    public string EffectName { get; }
    // ToDo compute the diagram all at once, every Point processed together

    public abstract Display Display { get; }

    public abstract void Compute(ref VoroDiagram voroDiagram);
    public event Action<Effect<TEffectData>> OnDataChanged;

    protected void NotifyDataChanged() {
        // the editor shall be notified that the data has changed
        // altering a value in the inspector calls this, to recompute the diagrams
        OnDataChanged?.Invoke(this);
    }

    protected FloatSlider CreateFloatSlider(string label, Func<float> getter, Action<float> setter,
        (float min, float max) range) {
        return UIHelper.CreateFloatSlider(
            label,
            getter,
            value => {
                setter(value);
                NotifyDataChanged(); // recompute
            },
            range
        );
    }

    protected IntSlider CreateIntSlider(string label, Func<int> getter, Action<int> setter,
        (int min, int max) range) {
        return UIHelper.CreateIntSlider(
            label,
            getter,
            value => {
                setter(value);
                NotifyDataChanged(); // recompute
            },
            range
        );
    }


    public override string ToString() {
        return $"{nameof(Data)}: {Data}, {nameof(EffectName)}: {EffectName}";
    }
}
}