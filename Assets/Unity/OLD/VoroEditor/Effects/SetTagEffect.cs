using System;
using EditorGUI.Source.Utility;
using OLD.VoroEditor.Effects.Internal;
using OLD.VoroEditor.Source;
using Display = OLD.VoroEditor.Elements.Display;

namespace OLD.VoroEditor.Effects {
/// <summary>
///     concrete implementation of a foo-effect
///     uses FooEffectData to define the configuration for the effect
///     --
///     this effect is what will be executed when the editor processes a voro
/// </summary>
public class SetTagEffect : Effect<SetTagEffectData> {
    Display _display;
    public SetTagEffect(SetTagEffectData data) : base("SetTag", data) { }

    public override Display Display {
        get
        {
            if (_display == null) {
                _display = UIHelper.CreateDisplay("SetTagDisplay");

                // create the effect data elements
                // _inspectorDisplay.Add(UIHelper.CreateEffectStringField(nameof(Data.tagName)));
                _display.AddToDisplay(CreateIntSlider(
                    "Tag Value",
                    () => Data.tagID,
                    val => Data.tagID = val,
                    (0, 1)
                ));
            }

            return _display;
        }
    }

    public override void Compute(ref VoroDiagram voroDiagram) {
        // for every point in the diagram compute some value
        for (var i = 0; i < voroDiagram.PointMap.Length; i++) {
            var index = voroDiagram.PointMap[i];
            // do compute
            // ToDo set the tag for this point
            // voroDiagram.AppendComputeToDiagram(index, pointPosition);
        }
    }
}


[Serializable]
public class SetTagEffectData : IEffectData {
    public string tagName;
    public int tagID;
}
}