using System;
using System.Collections.Generic;
using VoroUI.Elements.Base;
using VoroUI.Elements.Controls;

namespace VoroWorld.Generation.Effects.Base {
public abstract class Effect<TEffectData> : EffectBase, IEffect {
    protected TEffectData Data;

    public Effect(string name, TEffectData data) {
        Name = name;
        Data = data;
    }

    public abstract List<ControlElementBase> Controls { get; }

    public abstract void Compute();
    public string Name { get; }

    protected void CreateFloatSlider(
        string name,
        Func<float> dataGet,
        Action<float> dataSet,
        float sliderMin,
        float sliderMax,
        float sliderDefault
    ) {
        var element = new FloatSliderElement
        {
            DisplayName = name,
            MinValue = sliderMin,
            MaxValue = sliderMax,
            Value = sliderDefault
        };

        element.BindToData(dataGet, value => {
            dataSet(value);
            OnValueChanged(this, value); // recompute
        });

        Controls.Add(element);
    }

    protected void CreateLogFloatSlider(
        string name,
        Func<float> dataGet,
        Action<float> dataSet,
        float sliderMin,
        float sliderMax,
        float sliderDefault
    ) {
        if (sliderMin <= 0f) {
            sliderMin = 0.0001f; // avoid log(0)
        }

        var element = new FloatSliderElement
        {
            DisplayName = name,
            MinValue = sliderMin,
            MaxValue = sliderMax,
            Value = sliderDefault,
            IsLogScale = true
        };

        element.BindToData(dataGet, value => {
            dataSet(value);
            OnValueChanged(this, value); // recompute
        });

        Controls.Add(element);
    }

    protected void CreateIntSlider(
        string name,
        Func<int> dataGet,
        Action<int> dataSet,
        int sliderMin,
        int sliderMax,
        int sliderDefault
    ) {
        var element = new IntSliderElement
        {
            DisplayName = name,
            MinValue = sliderMin,
            MaxValue = sliderMax,
            Value = sliderDefault
        };
        element.BindToData(dataGet, value => {
            dataSet(value);
            OnValueChanged(this, value); // recompute
        });
        Controls.Add(element);
    }
}
}