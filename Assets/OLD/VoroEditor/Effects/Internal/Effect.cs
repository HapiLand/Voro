using System;
using EditorGUI.Source.Utility;
using OLD.VoroEditor.Effects.Internal.enums;
using OLD.VoroEditor.Elements;
using OLD.VoroEditor.Source;
using Display = OLD.VoroEditor.Elements.Display;

namespace OLD.VoroEditor.Effects.Internal {
/// <summary>
///     abstract generic base class for effects which use a specific type of data
///     the foundation for different effect types that have their own configuration
/// </summary>
/// <typeparam name="TEffectData"></typeparam>
public abstract class Effect<TEffectData> : EffectBase, IEffect {
    protected TEffectData Data;

    public Effect(string name, TEffectData data) {
        EffectName = name;
        Data = data;
    }

    public string EffectName { get; }
    // ToDo compute the diagram all at once, every Point processed together

    public abstract Display Display { get; }

    public abstract void Compute(ref VoroDiagram voroDiagram);

    protected FloatSlider CreateFloatSlider(string label, Func<float> getter, Action<float> setter,
        (float min, float max) range) {
        return UIHelper.CreateFloatSlider(
            label,
            getter,
            value => {
                setter(value);
                NotifyDataChanged(this); // recompute
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
                NotifyDataChanged(this); // recompute
            },
            range
        );
    }

    protected TypeDropdown CreateTypeDropdown(string label, Func<ComputeTypes> getter, Action<ComputeTypes> setter) {
        return UIHelper.CreateTypeDropdown(
            label,
            getter,
            value => {
                setter(value);
                NotifyDataChanged(this); // recompute
            }
        );
    }


    public override string ToString() {
        return $"{nameof(Data)}: {Data}, {nameof(EffectName)}: {EffectName}";
    }
}
}