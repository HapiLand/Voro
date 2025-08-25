using UnityEngine;

namespace Internal.Instructions {
public class Slope : INode {
    // this is an instruction to find some linear gradient value at a world position
    // the slope returns a height value that increases steadily in elevation

    public void ComputeHeight(IConfig configuration, Vector3 worldPos, out float height) {
        // the slopes direction value, this float is used as an angle
        // changing this will rotate the slope
        // 0 = slope along X axis
        // 90 = slope along Z axis
        // 180 = slope along -X axis
        // 270 = slope along -Z axis
        var direction = configuration.ConfigArr[0];
        // multiplier scales the final slope, to change the final steepness
        var multiplier = configuration.ConfigArr[1];

        // find a linear gradient of the slope along a direction value
        var radians = direction * Mathf.Deg2Rad;
        var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        var slopeHeight = Vector2.Dot(new Vector2(worldPos.x, worldPos.z), axis);
        // scale the value
        slopeHeight *= multiplier;
        height = slopeHeight;
    }
}
}