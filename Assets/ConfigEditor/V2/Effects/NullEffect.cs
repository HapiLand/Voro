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
public class NullEffect : Effect<NullEffectData> {
    VisualElement _inspectorDisplay;
    public NullEffect(NullEffectData data) : base("Null", data) { }

    public override VisualElement InspectorDisplay {
        get
        {
            if (_inspectorDisplay == null) {
                Debug.Log($"Get Display {EffectName}");

                // create the visual element that contains the elements in the display
                _inspectorDisplay = UIHelper.CreateGenericDisplay("NullDisplay");
            }

            return _inspectorDisplay;
        }
    }

    public override void Compute() {
        Debug.Log($"Compute effect: {EffectName}");
    }
}


[Serializable]
public class NullEffectData : IEffectData {
    public override string ToString() {
        return "NullEffectData";
    }
}
}