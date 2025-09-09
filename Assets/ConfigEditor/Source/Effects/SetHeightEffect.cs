using System;
using ConfigEditor.Source.Effects.Internal;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.Effects {
/// <summary>
///     sets the height of the terrain to a constant value to flatten it
/// </summary>
public class SetHeightEffect : Effect<SetHeightEffectData> {
    VisualElement _inspectorDisplay;

    public SetHeightEffect(SetHeightEffectData data) : base("SetHeight", data) { }

    public override VisualElement InspectorDisplay {
        get
        {
            if (_inspectorDisplay == null) {
                Debug.Log($"Get Display {EffectName}");

                // create the visual element that contains the elements in the display
                _inspectorDisplay = UIHelper.CreateGenericDisplay("SetHeightDisplay");

                // create the effect data elements
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.heightValue), 0f, 10f, 0f,
                    newValue => { Data.heightValue = newValue; }));
            }

            return _inspectorDisplay;
        }
    }

    public override void Compute(ref VoroDiagram voroDiagram) {
        // for every point in the diagram compute some value
        for (var i = 0; i < voroDiagram.PointMap.Length; i++) {
            var index = voroDiagram.PointMap[i];
            var pointPosition = voroDiagram.Points[index];

            // do compute
            pointPosition.y = Data.heightValue;

            // write new value back to the diagram
            voroDiagram.AppendComputeToDiagram(index, pointPosition);
        }
    }
}

[Serializable]
public class SetHeightEffectData : IEffectData {
    public float heightValue;
}
}