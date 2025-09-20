using System.Collections.Generic;
using UnityEngine;
using VoroUI.Effects.Base;
using VoroUI.Effects.Internal;
using VoroUI.Elements.Base;

namespace VoroUI.Effects {
public class DefaultEffectData : IEffectData {
    public float Float;
    public int Int;
    public float LogFloat;
}

public class DefaultEffect : Effect<DefaultEffectData> {
    /// <summary>
    ///     stores the field controls for the effect
    /// </summary>
    List<ControlElementBase> _controls;

    public DefaultEffect() : base(nameof(EffectNames.DefaultFX), new DefaultEffectData()) { }

    public override List<ControlElementBase> Controls {
        get
        {
            if (_controls == null) {
                // build controls the first time they are accessed
                _controls = new List<ControlElementBase>();

                CreateFloatSlider(
                    "Float Value",
                    () => Data.Float,
                    val => Data.Float = val,
                    0f,
                    1f,
                    0f
                );

                CreateIntSlider(
                    "Int Value",
                    () => Data.Int,
                    val => Data.Int = val,
                    0,
                    360,
                    180
                );

                CreateLogFloatSlider(
                    "Log Float Value",
                    () => Data.LogFloat,
                    val => Data.LogFloat = val,
                    0f,
                    1f,
                    0f
                );
            }

            return _controls;
        }
    }

    public override void Compute() {
        Debug.Log($"Compute Effect.{Name}");

        // var cellPoints = tile.Diagram.CellPoints;
        // var tileOrigin = tile.Origin;
        //
        // // update diagram data
        // for (var i = 0; i < cellPoints.Length; i++) {
        //     var point = cellPoints[i];
        //     point.Position = new Vector3(point.Position.x, 2f, point.Position.z);
        //     cellPoints[i] = point;
        // }
        //
        // tile.Diagram.CellPoints = cellPoints;
        //
        // // update gameobjects
        // for (var i = 0; i < tile.TileContainer.transform.childCount; i++) {
        //     var cellObject = tile.TileContainer.transform.GetChild(i);
        //     var pos = tile.Diagram.CellPoints[i].Position;
        //     cellObject.position = new Vector3(pos.x + tileOrigin.x, pos.y, pos.z + tileOrigin.z);
        // }
    }
}
}