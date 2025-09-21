using System.Collections.Generic;
using UnityEngine;
using VoroUI.Elements.Base;
using VoroWorld.Diagrams;
using VoroWorld.Generation.Effects.Base;
using VoroWorld.Generation.Effects.Internal;

namespace VoroWorld.Generation.Effects {
public class SlopeEffectData : IEffectData {
    public float Direction;
    public float Scale;
}

public class SlopeEffect : Effect<SlopeEffectData> {
    public SlopeEffect() : base(nameof(EffectNames.Slope), new SlopeEffectData()) { }

    public override void Compute(ref VoroDiagram diagram) {
        var cellPoints = diagram.CellPoints;
        var tileOrigin = diagram.Tile.Position;

        // compute every point with the function
        for (var i = 0; i < cellPoints.Length; i++) {
            var point = cellPoints[i];
            var pointWorldPosition = point.Position + tileOrigin;

            // slope function
            var radians = Data.Direction * Mathf.Deg2Rad;
            var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            var slopeHeight = Vector2.Dot(new Vector2(pointWorldPosition.x, pointWorldPosition.z), axis);
            slopeHeight *= Data.Scale;

            point.Position = new Vector3(point.Position.x, slopeHeight, point.Position.z);
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