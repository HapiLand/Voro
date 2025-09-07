using System;
using ConfigEditor.V2.Effects.Internal;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.V2.Effects {
/// <summary>
///     concrete implementation of a foo-effect
///     uses FooEffectData to define the configuration for the effect
///     --
///     this effect is what will be executed when the editor processes a voro
/// </summary>
public class NoiseEffect : Effect<NoiseEffectData> {
    VisualElement _inspectorDisplay;
    public NoiseEffect(NoiseEffectData data) : base("Noise", data) { }

    public override VisualElement InspectorDisplay {
        get
        {
            if (_inspectorDisplay == null) {
                Debug.Log($"Get Display {EffectName}");

                // create the visual element that contains the elements in the display
                _inspectorDisplay = UIHelper.CreateGenericDisplay("NoiseDisplay");

                // create the effect data elements
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(nameof(Data.noiseScale), 0f, 10f, 0f));
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(nameof(Data.noiseSize), 0f, 10f, 0f));
                // ToDo sliders must update values in data
            }

            return _inspectorDisplay;
        }
    }

    public override void Compute() {
        Debug.Log($"Compute effect: {EffectName}");
    }
}


[Serializable]
public class NoiseEffectData : IEffectData {
    public float noiseScale;
    public float noiseSize;

    public override string ToString() {
        return $"NoiseEffectData {nameof(noiseScale)}: {noiseScale}, {nameof(noiseSize)}: {noiseSize}";
    }
}
}