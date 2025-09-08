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
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.noiseScale), 0f, 10f, 0f,
                    newValue => { Data.noiseScale = newValue; }));
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.noiseSize), 0f, 10f, 0f,
                    newValue => { Data.noiseScale = newValue; }));
            }

            return _inspectorDisplay;
        }
    }

    public override void Compute(ref VoroDiagram voroDiagram) {
        Debug.Log($"Compute effect: {EffectName}");

        // ToDo compute the diagram all at once, every Point processed together
        // for testing, use a generic for loop over the diagram

        // get index map for every point
        for (var i = 0; i < voroDiagram.PointMap.Length; i++) {
            // find the index of the current point map point
            // this index value is for a specific Point that the diagram contains
            var index = voroDiagram.PointMap[i];
            // access the point at the index
            var point = voroDiagram.Points[index];

            // ToDo replace placeholder modification with what the effect actually does
            // placeholder modification to verify things work
            var pos = point; // read the points position
            var yChange = 5f; // change the Y value by 5
            pos.y += yChange; // modify value

            // write the value back into the diagram
            voroDiagram.Points[index] = pos;
        }
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