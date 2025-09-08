using System;
using ConfigEditor.V2.Effects.Internal;
using Terrain;
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
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(nameof(Data.direction), 0f, 10f, 0f));
                _inspectorDisplay.Add(UIHelper.CreateEffectIntSlider(nameof(Data.iterations), 0, 10, 0));
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(nameof(Data.minStepSize), 0f, 10f, 0f));
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(nameof(Data.maxStepSize), 0f, 10f, 0f));
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(nameof(Data.stepScale), 0f, 10f, 0f));
                // ToDo sliders must update values in data
            }

            return _inspectorDisplay;
        }
    }

    public override void Compute(ref Diagram diagram) {
        Debug.Log($"Compute effect: {EffectName}");
        
        // ToDo compute the diagram all at once, every Point processed together
        // for testing, use a generic for loop over the diagram

        // get index map for every point
        for (var i = 0; i < diagram.PointMap.Length; i++) {
            // find the index of the current point map point
            // this index value is for a specific Point that the diagram contains
            int index = diagram.PointMap[i];
            // access the point at the index
            var point = diagram.Points[index];
            
            // ToDo replace placeholder modification with what the effect actually does
            // placeholder modification to verify things work
            Vector3 pos = point; // read the points position
            float yChange = 5f; // change the Y value by 5
            pos.y += yChange; // modify value
            
            // write the value back into the diagram
            diagram.Points[index] = pos;
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