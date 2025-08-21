using UnityEngine;

namespace Internal.Instructions {
public class Terrace : INode {
    public Terrace() { }

    public void ComputeHeight(IConfig configuration, Vector3 worldPos, out float height) {
        var doTerrace = configuration.ConfigArr[0];
        var iterations = configuration.ConfigArr[1];
        var min = configuration.ConfigArr[2];
        var max = configuration.ConfigArr[3];
        var scale = configuration.ConfigArr[4];
        var tilt = configuration.ConfigArr[5];
        
        if (doTerrace == 0) {
            height = 0f;
            return;
        }
        
        // find the direction the terrace
        float radians = tilt * Mathf.Deg2Rad;
        Vector2 axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        float terraceHeight = Vector2.Dot(new Vector2(worldPos.x, worldPos.z), axis);
                    
        float h = terraceHeight;
                    
        float div = h / scale;
        float flat = Mathf.Floor(div);
        int seed = 0;
        Random.InitState(Mathf.RoundToInt(flat) + seed);
        float val = Random.value;
        val = fit01(val, min, max) * iterations;
        float fit01(float value, float newMin, float newMax) {
            return value * (newMax - newMin) + newMin;
        }
        // find the final value of the terrace
        float level = (flat + val) * scale;
        // apply the value to the height
        height = level;
    }
}
}