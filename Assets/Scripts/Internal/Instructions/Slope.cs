using UnityEngine;

namespace Internal.Instructions {
public class Slope : INode {
    public Slope() { }

    public void ComputeHeight(IConfig configuration, Vector3 worldPos, out float height) {
        var doSlope = configuration.ConfigArr[0];
        var direction = configuration.ConfigArr[1];
        var multiplier = configuration.ConfigArr[2];
        
        if (doSlope == 0) {
            height = 0f;
            return;
        }
        
        // find a linear gradient of the slope along a direction value
        var radians = direction * Mathf.Deg2Rad;
        var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        float slopeHeight = Vector2.Dot(new Vector2(worldPos.x, worldPos.z), axis);
        // scale the value
        slopeHeight *= multiplier;
        height = slopeHeight;
    }
}
}