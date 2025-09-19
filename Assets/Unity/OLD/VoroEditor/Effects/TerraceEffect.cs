using System;
using EditorGUI.Source.Utility;
using OLD.VoroEditor.Effects.Internal;
using OLD.VoroEditor.Source;
using UnityEngine;
using Random = UnityEngine.Random;
using Display = OLD.VoroEditor.Elements.Display;

namespace OLD.VoroEditor.Effects {
/// <summary>
///     concrete implementation of a foo-effect
///     uses FooEffectData to define the configuration for the effect
///     --
///     this effect is what will be executed when the editor processes a voro
/// </summary>
public class TerraceEffect : Effect<TerraceEffectData> {
    Display _display;
    public TerraceEffect(TerraceEffectData data) : base("Terrace", data) { }

    public override Display Display {
        get
        {
            if (_display == null) {
                _display = UIHelper.CreateDisplay("TerraceDisplay");

                // create the effect data elements
                _display.AddToDisplay(CreateFloatSlider(
                    "Direction",
                    () => Data.direction,
                    val => Data.direction = val,
                    (0f, 1f)
                ));
                _display.AddToDisplay(CreateIntSlider(
                    "Iterations",
                    () => Data.iterations,
                    val => Data.iterations = val,
                    (0, 1)
                ));
                _display.AddToDisplay(CreateFloatSlider(
                    "Min Step",
                    () => Data.minStepSize,
                    val => Data.minStepSize = val,
                    (0f, 1f)
                ));
                _display.AddToDisplay(CreateFloatSlider(
                    "Max Step",
                    () => Data.maxStepSize,
                    val => Data.maxStepSize = val,
                    (0f, 1f)
                ));
                _display.AddToDisplay(CreateFloatSlider(
                    "Step Scale",
                    () => Data.stepScale,
                    val => Data.stepScale = val,
                    (0f, 1f)
                ));
            }

            return _display;
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