using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.Source.Effects.Internal;

namespace VoroEditor.Source.Effects {
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