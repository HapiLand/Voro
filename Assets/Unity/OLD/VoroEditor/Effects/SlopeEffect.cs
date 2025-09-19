using System;
using EditorGUI.Source.Utility;
using OLD.VoroEditor.Effects.Internal;
using OLD.VoroEditor.Effects.Internal.enums;
using OLD.VoroEditor.Source;
using UnityEngine;
using Display = OLD.VoroEditor.Elements.Display;

namespace OLD.VoroEditor.Effects {
/// <summary>
///     concrete implementation of a foo-effect
///     uses FooEffectData to define the configuration for the effect
///     --
///     this effect is what will be executed when the editor processes a voro
/// </summary>
public class SlopeEffect : Effect<SlopeEffectData> {
    Display _display;

    public SlopeEffect(SlopeEffectData data) : base("Slope", data) { }

    public override Display Display {
        get
        {
            if (_display == null) {
                _display = UIHelper.CreateDisplay("SlopeDisplay");

                // create the effect data elements
                _display.AddToDisplay(CreateTypeDropdown(
                    "Type",
                    () => Data.computeType,
                    val => Data.computeType = val
                ));
                _display.AddToDisplay(CreateFloatSlider(
                    "Direction",
                    () => Data.slopeDirection,
                    val => Data.slopeDirection = val,
                    (0f, 1f)
                ));
                _display.AddToDisplay(CreateFloatSlider(
                    "Scale",
                    () => Data.slopeScale,
                    val => Data.slopeScale = val,
                    (0f, 1f)
                ));
            }

            return _display;
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
            var axis = new Vector2(Mathf.Cos(radians),
                Mathf.Sin(radians));
            // ToDo direction value over 180 causes slope to be negative
            var slopeHeight = Vector2.Dot(new Vector2(pointPosition.x,
                    pointPosition.z),
                axis);
            slopeHeight *= Data.slopeScale;

            switch (Data.computeType) {
            case ComputeTypes.Addition:
                pointPosition.y += slopeHeight;
                break;
            case ComputeTypes.Subtraction:
                pointPosition.y -= slopeHeight;
                break;
            }

            // write new value back to the diagram
            voroDiagram.AppendComputeToDiagram(index, pointPosition);
        }
    }
}

[Serializable]
public class SlopeEffectData : IEffectData {
    public ComputeTypes computeType;
    public float slopeDirection;
    public float slopeScale;
}
}