using System.Collections.Generic;
using UnityEngine;
using Voro.UI;
using Voro.World;

namespace Voro.Jen.Effects {
public class DefaultEffectData : IEffectData {
    public float Float;
    public int Int;
    public float LogFloat;
}

public class DefaultFX : Effect<DefaultEffectData> {
    /// <summary>
    ///     stores the field controls for the effect
    /// </summary>
    List<ControlElementBase> _controls;

    public DefaultFX() : base(nameof(EffectName.DefaultFX), new DefaultEffectData()) { }

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

    public override void Compute(ref Chunk diagram) {
        // Debug.Log("Effect.Compute ");

        var cellPoints = diagram.CellPoints;
        var tileOrigin = diagram.Tile.Position;

        // update diagram data
        for (var i = 0; i < cellPoints.Length; i++) {
            var point = cellPoints[i];
            point.Position = new Vector3(point.Position.x, Data.Float, point.Position.z);
            cellPoints[i] = point;
        }

        diagram.CellPoints = cellPoints;

        // update gameobjects
        for (var i = 0; i < diagram.Tile.Container.transform.childCount; i++) {
            var cellObject = diagram.Tile.Container.transform.GetChild(i);
            var pos = diagram.CellPoints[i].Position;
            cellObject.position = new Vector3(pos.x + tileOrigin.x, pos.y, pos.z + tileOrigin.z);
        }
    }
}
}