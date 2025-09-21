using System.Collections.Generic;
using UnityEngine;
using VoroUI.Elements.Base;
using VoroWorld.Diagrams;
using VoroWorld.Generation.Effects.Base;
using VoroWorld.Generation.Effects.Internal;

namespace VoroWorld.Generation.Effects {
public class DefaultEffectData : IEffectData {
    public float Float;
    public int Int;
    public float LogFloat;
}
public class DefaultEffect : Effect<DefaultEffectData> {
    public DefaultEffect() : base(nameof(EffectNames.DefaultFX), new DefaultEffectData()) { }
    public override void Compute(ref VoroDiagram diagram) {
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