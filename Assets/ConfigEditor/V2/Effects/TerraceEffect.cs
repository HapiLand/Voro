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
public class TerraceEffect : Effect<TerraceEffectData> {
    VisualElement _inspectorDisplay;
    public TerraceEffect(TerraceEffectData data) : base("Terrace", data) { }

    public override VisualElement InspectorDisplay {
        get
        {
            if (_inspectorDisplay == null) {
                Debug.Log($"Get Display {EffectName}");

                // create the visual element that contains the elements in the display
                _inspectorDisplay = UIHelper.CreateGenericDisplay("TerraceDisplay");

                // create the effect data elements
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.direction), 0f, 10f, 0f,
                    newValue => { Data.direction = newValue; }));

                _inspectorDisplay.Add(UIHelper.CreateEffectIntSlider(
                    nameof(Data.iterations), 0, 10, 0,
                    newValue => { Data.iterations = newValue; }));

                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.minStepSize), 0f, 10f, 0f,
                    newValue => { Data.minStepSize = newValue; }));

                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.maxStepSize), 0f, 10f, 0f,
                    newValue => { Data.maxStepSize = newValue; }));

                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.stepScale), 0f, 10f, 0f,
                    newValue => { Data.stepScale = newValue; }));
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
public class TerraceEffectData : IEffectData {
    public float direction;
    public int iterations;
    public float minStepSize;
    public float maxStepSize;
    public float stepScale;

    public override string ToString() {
        return
            $"TerraceEffectData {nameof(direction)}: {direction}, {nameof(iterations)}: {iterations}, {nameof(minStepSize)}: {minStepSize}, {nameof(maxStepSize)}: {maxStepSize}, {nameof(stepScale)}: {stepScale}";
    }
}
}