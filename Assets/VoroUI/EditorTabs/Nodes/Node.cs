using System;
using System.Collections.Generic;
using VoroUI.EditorTabs.Nodes.Controls.Base;
using VoroUI.EditorTabs.Nodes.Controls.Controls;
using VoroWorld.Generation.Effects.Internal;

namespace VoroUI.EditorTabs.Nodes {
public abstract class Node<TControlData> : NodeBase, INode {
    protected TControlData Data;

    public Node(EffectNames name, TControlData data) {
        Name = name;
        Data = data;
    }

    public abstract List<ControlElementBase> Controls { get; }
    public EffectNames Name { get; }

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