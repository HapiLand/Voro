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
public class SetTagEffect : Effect<SetTagEffectData> {
    VisualElement _inspectorDisplay;
    public SetTagEffect(SetTagEffectData data) : base("SetTag", data) { }

    public override VisualElement InspectorDisplay {
        get
        {
            if (_inspectorDisplay == null) {
                Debug.Log($"Get Display {EffectName}");

                // create the visual element that contains the elements in the display
                _inspectorDisplay = UIHelper.CreateGenericDisplay("SetTagDisplay");

                // create the effect data elements
                _inspectorDisplay.Add(UIHelper.CreateEffectStringField(nameof(Data.tagName)));
                _inspectorDisplay.Add(UIHelper.CreateEffectIntSlider(nameof(Data.tagID), 0, 10, 0));
                // ToDo elements must update values in data
                // ToDo implement different input for int besides slider, that would be suitable for this element
            }

            return _inspectorDisplay;
        }
    }

    public override void Compute() {
        Debug.Log($"Compute effect: {EffectName}");
    }
}


[Serializable]
public class SetTagEffectData : IEffectData {
    public string tagName;
    public int tagID;

    public override string ToString() {
        return $"SetTagEffectData {nameof(tagName)}: {tagName}, {nameof(tagID)}: {tagID}";
    }
}
}