using System;
using VoroEditor.Effects.Internal;
using VoroEditor.Source;
using VoroEditor.Utility;
using Display = VoroEditor.Elements.Display;

namespace VoroEditor.Effects {
/// <summary>
///     sets the height of the terrain to a constant value to flatten it
/// </summary>
public class SetHeightEffect : Effect<SetHeightEffectData> {
    Display _display;

    public SetHeightEffect(SetHeightEffectData data) : base("SetHeight", data) { }

    public override Display Display {
        get
        {
            if (_display == null) {
                _display = UIHelper.CreateDisplay("SetHeightDisplay");

                // create the effect data elements
                _display.AddToDisplay(CreateFloatSlider(
                    "Height",
                    () => Data.heightValue,
                    val => Data.heightValue = val,
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
            pointPosition.y = Data.heightValue;

            // write new value back to the diagram
            voroDiagram.AppendComputeToDiagram(index, pointPosition);
        }
    }
}

[Serializable]
public class SetHeightEffectData : IEffectData {
    public float heightValue;
}
}