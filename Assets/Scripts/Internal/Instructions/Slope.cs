using UnityEngine;

namespace Internal.Instructions {
public class Slope : INode {
    readonly IConfig config;

    public Slope(IConfig config) {
        this.config = config;
    }

    public float Solve(float height, Vector3 worldPos) {
        // ToDo replace with a real solve method

        var direction = config.ConfigArr[0];
        var multiplier = config.ConfigArr[1];
        var radians = direction * Mathf.Deg2Rad;
        var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        // ToDo direction value over 180 causes slope to be negative
        var slopeHeight = Vector2.Dot(new Vector2(worldPos.x, worldPos.z), axis);

        slopeHeight *= multiplier;
        return slopeHeight;
    }
}
}