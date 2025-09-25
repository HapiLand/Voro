using System;
using System.Collections.Generic;
using Voro.Jen.Compute.FX.Internal;
using Voro.UI.EditorTabs.Nodes.Controls.Base;
using Voro.UI.EditorTabs.Nodes.Controls.Controls;

namespace Voro.UI.EditorTabs.Nodes {
public abstract class Node<TEffectData> : NodeBase, INode {
    protected TEffectData Data;

    public Node(EffectName name, TEffectData data) {
        Name = name;
        Data = data;
    }

    public abstract List<ControlElementBase> Controls { get; }
    public EffectName Name { get; }

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
            OnValueChanged(); // recompute
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
            OnValueChanged(); // recompute
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
            OnValueChanged(); // recompute
        });
        Controls.Add(element);
    }
}
}