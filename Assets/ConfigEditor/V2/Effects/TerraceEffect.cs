using System;
using ConfigEditor.V2.Effects.Internal;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

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
        // for every point in the diagram compute some value
        for (var i = 0; i < voroDiagram.PointMap.Length; i++) {
            var index = voroDiagram.PointMap[i];
            var pointPosition = voroDiagram.Points[index];

            // do compute
            var radians = Data.direction * Mathf.Deg2Rad;
            var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            var terraceHeight = Vector2.Dot(new Vector2(pointPosition.x, pointPosition.z), axis);

            var div = terraceHeight / Data.stepScale;
            var flat = Mathf.Floor(div);
            var seed = 0;
            Random.InitState(Mathf.RoundToInt(flat) + seed);
            var val = Random.value;
            val = remap(val, Data.minStepSize, Data.maxStepSize) * Data.iterations;

            float remap(float value, float newMin, float newMax) {
                // remap a value from an old range of [0,1] into a new range [min,max]
                // val = 0.5 | newMin = 10 | newMax = 20
                // result = 15
                // Debug.Log(fit01(0.5f, 10f, 20f));
                return value * (newMax - newMin) + newMin;
            }

            // find the final value of the terrace
            var level = (flat + val) * Data.stepScale;

            level /= 2f;
            pointPosition.y += level;

            // write new value back to the diagram
            voroDiagram.AppendComputeToDiagram(index, pointPosition);
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
}
}