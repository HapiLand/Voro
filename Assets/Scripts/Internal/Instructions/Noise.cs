using UnityEngine;

namespace Internal.Instructions {
public class Noise : INode {
    public Noise() { }
    public void ComputeHeight(IConfig configuration, Vector3 worldPos, out float height)  {
        var doNoise = configuration.ConfigArr[0];
        var scale = configuration.ConfigArr[1];
        var size = configuration.ConfigArr[2];
        
        if (doNoise == 0) {
            height = 0;
            return;
        }
        
        // generate perlin noise
        Perlin perlin = new Perlin();
        double dx = Mathf.Abs(worldPos.x * size);
        // the y value for the noise uses the current y position (height)
        double dy = Mathf.Abs(worldPos.y * size);
        double dz = Mathf.Abs(worldPos.y * size);
        double noise = perlin.Noise(dx, dy, dz);
        noise *= (double)scale;
                    
        // value has been found, add it to the height output
        height = (float)noise;
    }
}
}