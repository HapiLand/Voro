using EditorGUI.Elements;
using EditorGUI.Source.Effects.Base;
using EditorGUI.Source.Effects.EffectData;
using UnityEngine;

namespace EditorGUI.Source.Effects {
public class SlopeEffect : Effect<SlopeEffectData> {
    public SlopeEffect(SlopeEffectData data) : base("Slope", data) {
        InspectorControls = new InspectorElement { DisplayName = "SlopeInspector", name = "SlopeInspector" };

        CreateFloatControl(
            "Direction",
            () => data.direction,
            val => data.direction = val,
            (0f, 360f),
            0f);
        CreateFloatControl(
            "Scale",
            () => data.scale,
            val => data.scale = val,
            (0f, 1f),
            1f);
    }

    public override InspectorElement InspectorControls { get; }


    public override void Compute(ref DiagramElement diagram) {
        Debug.Log($"Compute Effect.{Name} on Diagram.{diagram.DisplayName}");
        Debug.Log($"Data: direction {Data.direction}  scale {Data.scale}");
    }
}
}