using UnityEngine;

namespace Internal.Instructions {
public class Noise : INode {
    // this is an instruction to find some perlin noise value at a world position
    // the noise returns a height value, so that points in a voro can have
    // some random noise to their elevation
    public Noise() { }
    public void ComputeHeight(IConfig configuration, Vector3 worldPos, out float height)  {
        var doNoise = configuration.ConfigArr[0];
        // scale alters the value of the noise to increase the final height
        var scale = configuration.ConfigArr[1];
        // size is how large of a region to sample in a noise texture
        // smaller values cause a noise pattern to appear larger
        // this is because the region being sampled has been shrunk
        // so it will "zoom in" on the noise there
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