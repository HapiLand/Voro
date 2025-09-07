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
public class SlopeEffect : Effect<SlopeEffectData> {
    VisualElement _inspectorDisplay;

    public SlopeEffect(SlopeEffectData data) : base("Slope", data) { }

    public override VisualElement InspectorDisplay {
        get
        {
            if (_inspectorDisplay == null) {
                Debug.Log($"Get Display {EffectName}");

                // create the visual element that contains the elements in the display
                _inspectorDisplay = UIHelper.CreateGenericDisplay("SlopeDisplay");

                // create the effect data elements
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(nameof(Data.slopeDirection), 0f, 10f, 0f));
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(nameof(Data.slopeScale), 0f, 10f, 0f));
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
public class SlopeEffectData : IEffectData {
    public float slopeDirection;
    public float slopeScale;

    public override string ToString() {
        return $"SlopeEffectData {nameof(slopeDirection)}: {slopeDirection}, {nameof(slopeScale)}: {slopeScale}";
    }
}
}