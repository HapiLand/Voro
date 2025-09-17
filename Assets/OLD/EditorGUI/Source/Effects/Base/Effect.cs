using System;
using EditorGUI.Elements;
using EditorGUI.Source.Utility;
using EditorGUI.Source.Voro.Grids;

namespace EditorGUI.Source.Effects.Base {
public abstract class Effect<TEffectData> : EffectBase, IEffect {
    /// <summary>
    ///     properties for the effect
    /// </summary>
    protected TEffectData Data;

    public Effect(string name, TEffectData data) {
        Name = name;
        Data = data;
    }

    /// <summary>
    ///     each derived effect has its own set of controls
    /// </summary>
    public abstract InspectorElement InspectorControls { get; }

    public string Name { get; }

    /// <summary>
    ///     computes the function this effect has on the provided Diagram
    /// </summary>
    /// <param name="diagram"></param>
    public abstract void Compute(ref WorldTile tile);

    /// <summary>
    ///     create a new instance of controls that the effect will display in the inspector
    /// </summary>
    /// <param name="label">display name</param>
    /// <param name="getter">externally gets the value set of the control</param>
    /// <param name="setter">externally sets the value of the control</param>
    /// <param name="range">the min-max range of the value</param>
    /// <param name="startingValue">the default value of the control</param>
    protected void CreateFloatControl(string label, Func<float> getter, Action<float> setter,
        (float min, float max) range, float startingValue) {
        // create the float control
        // add control into InspectorControls

        var element = UIHelper.CreateFloatControl(
            label,
            getter,
            value => {
                setter(value);
                NotifyOnChange(this); // notify the value changed to force a refresh to recompute
            },
            range,
            startingValue
        );

        InspectorControls.AddControl(element);
    }
}
}