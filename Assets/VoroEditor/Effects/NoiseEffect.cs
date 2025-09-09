using System;
using UnityEngine;
using VoroEditor.Effects.Internal;
using VoroEditor.Source;
using VoroEditor.Utility;
using Display = VoroEditor.Elements.Display;

namespace VoroEditor.Effects {
public class NoiseEffect : Effect<NoiseEffectData> {
    Display _display;
    public NoiseEffect(NoiseEffectData data) : base("Noise", data) { }

    public override Display Display {
        get
        {
            if (_display == null) {
                _display = UIHelper.CreateDisplay("NoiseDisplay");

                // create the effect data elements
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
            pointPosition.y += (float)noise;
            // ToDo implement way to choose whether to add or subtract

            // write new value back to the diagram
            voroDiagram.AppendComputeToDiagram(index, pointPosition);
        }
    }
}


[Serializable]
public class NoiseEffectData : IEffectData {
    public float noiseScale;
    public float noiseSize;
}
}