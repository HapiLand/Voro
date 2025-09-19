using System;
using EditorGUI.Source.Utility;
using OLD.VoroEditor.Effects.Internal;
using OLD.VoroEditor.Effects.Internal.enums;
using OLD.VoroEditor.Source;
using UnityEngine;
using Display = OLD.VoroEditor.Elements.Display;

namespace OLD.VoroEditor.Effects {
public class NoiseEffect : Effect<NoiseEffectData> {
    Display _display;
    public NoiseEffect(NoiseEffectData data) : base("Noise", data) { }

    public override Display Display {
        get
        {
            if (_display == null) {
                _display = UIHelper.CreateDisplay("NoiseDisplay");

                // create the effect data elements
                _display.AddToDisplay(CreateTypeDropdown(
                    "Type",
                    () => Data.computeType,
                    val => Data.computeType = val
                ));
                _display.AddToDisplay(CreateFloatSlider(
                    "Scale",
                    () => Data.noiseScale,
                    val => Data.noiseScale = val,
                    (0f, 1f)
                ));
                _display.AddToDisplay(CreateFloatSlider(
                    "Size",
                    () => Data.noiseSize,
                    val => Data.noiseSize = val,
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
            var perlin = new Perlin();
            double dx = Mathf.Abs(pointPosition.x * Data.noiseSize);
            double dy = Mathf.Abs(pointPosition.y * Data.noiseSize);
            double dz = Mathf.Abs(pointPosition.z * Data.noiseSize);
            var noise = perlin.Noise(dx, dy, dz);
            noise *= Data.noiseScale;

            switch (Data.computeType) {
            case ComputeTypes.Addition:
                pointPosition.y += (float)noise;
                break;
            case ComputeTypes.Subtraction:
                pointPosition.y -= (float)noise;
                break;
            }

            // write new value back to the diagram
            voroDiagram.AppendComputeToDiagram(index, pointPosition);
        }
    }
}


[Serializable]
public class NoiseEffectData : IEffectData {
    public ComputeTypes computeType;
    public float noiseScale;
    public float noiseSize;
}
}