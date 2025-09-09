using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.Source.Effects.Internal;

namespace VoroEditor.Source.Effects {
/// <summary>
///     concrete implementation of a foo-effect
///     uses FooEffectData to define the configuration for the effect
///     --
///     this effect is what will be executed when the editor processes a voro
/// </summary>
public class SlopeEffect : Effect<SlopeEffectData> {
    VisualElement _inspectorDisplay;

    public SlopeEffect(SlopeEffectData data) : base("Slope", data) { }

    public override VisualElement InspectorDisplay {
        get
        {
            if (_inspectorDisplay == null) {
                Debug.Log($"Get Display {EffectName}");

                // create the visual element that contains the elements in the display
                _inspectorDisplay = UIHelper.CreateGenericDisplay("SlopeDisplay");

                // create the effect data elements
                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.slopeDirection), 0f, 10f, 0f,
                    newValue => { Data.slopeDirection = newValue; }));

                _inspectorDisplay.Add(UIHelper.CreateEffectFloatSlider(
                    nameof(Data.slopeScale), 0f, 1f, 1f,
                    newValue => { Data.slopeScale = newValue; }));


                // ToDo slider does use default value to drive the effect when first added to column
            }

            return _inspectorDisplay;
        }
    }

    public override void Compute(ref VoroDiagram voroDiagram) {
        // ToDo replace compute method to use an iterative one
        //  for all points marked as active, copy their elevation
        //  on to all points which are forwards of the active points
        //  (so now they have a matching height)
        //  also add an additional amount
        //  (so now those points are raise up)
        //  set only these points as active
        //  repeat while any points are still waiting to be computed

        // for every point in the diagram compute some value
        for (var i = 0; i < voroDiagram.PointMap.Length; i++) {
            var index = voroDiagram.PointMap[i];
            var pointPosition = voroDiagram.Points[index];

            // do compute
            var radians = Data.slopeDirection * Mathf.Deg2Rad;
            var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            // ToDo direction value over 180 causes slope to be negative
            var slopeHeight = Vector2.Dot(new Vector2(pointPosition.x, pointPosition.z), axis);
            slopeHeight *= Data.slopeScale;
            pointPosition.y += slopeHeight;

            // write new value back to the diagram
            voroDiagram.AppendComputeToDiagram(index, pointPosition);
        }
    }
}

[Serializable]
public class SlopeEffectData : IEffectData {
    public float slopeDirection;
    public float slopeScale;

    public override string ToString() {
        return $"SlopeEffectData {nameof(slopeDirection)}: {slopeDirection}, {nameof(slopeScale)}: {slopeScale}";
    }
}
}