using DataTypes;
using Internal.Configuration;
using Internal.Instructions;
using UnityEngine;

namespace Internal {
// to be used to produce height value from a json config
public class VoroHeight {
    public VoroHeight() {}

    public void GetHeight(JsonConfig config, PointArray pointArr, Vector3 offset, out float[] heightArr) {
        // each height value for each point
        heightArr = new float[pointArr.points.Length];
        
        // intended goal is for this method to read the configuration as an expression
        // the expression has a set of instructions for how to alter the height
        // iterate across every point so a height value can be found per point
        
        for (var i = 0; i < pointArr.points.Length; i++) {
            // find the true position of the point in the world
            Vector3 pointWorldPos = pointArr.points[i].position + offset;
            // world position is 2D as the Y value is what is being solved
            Vector2 worldPos2D = new Vector2(pointWorldPos.x, pointWorldPos.z);
            
            // using the points position, find the world height there
            FindHeightAtPosition(worldPos2D, out heightArr[i]);
        }

        void FindHeightAtPosition(Vector2 pos, out float height) {
            // initial height value at 0
            height = 0;
            
            // there are 3 instructions in this config - slope, noise, terrace
            var instructionAmount = 3;

            // iterate through the expression to execute each instruction
            for (var i = 0; i < instructionAmount; i++) {
                // this code is going to be quite ugly until a true expression exists

                // the current iteration has the instruction to create a slope
                if (i == 0) {
                    // not a good place for this to be constructed
                    // use the json config array to produce the Configuration struct
                    // structs of the IConfig type is what the json data is used
                    // to construct
                    // each instruction should have its own configuration, as each
                    // instruction has unique parameters
                    // create the configuration for the slope instruction
                    var slopeConfig = new SlopeCfg(config.slope);
                    
                    var slope = new Slope();
                    slope.ComputeHeight(slopeConfig, new Vector3(pos.x, height, pos.y), out var slopeHeight);
                    height += slopeHeight;
                }
                
                // the current iteration has the instruction to apply noise
                if (i == 1) {
                    // create the configuration for the noise instruction
                    var noiseConfig = new NoiseCfg(config.noise);
                    
                    var noise = new Noise();
                    noise.ComputeHeight(noiseConfig, new Vector3(pos.x, height, pos.y), out var noiseHeight);
                    height += noiseHeight;
                }
                
                // the current iteration has the instruction to apply a terrace effect
                if (i == 2) {
                    // create the configuration for the terrace instruction
                    var terraceConfig = new TerraceCfg(config.terrace);
                    
                    var terrace = new Terrace();
                    terrace.ComputeHeight(terraceConfig, new Vector3(pos.x, height, pos.y), out var terraceHeight);
                    if (terraceHeight != 0.0) {
                        height += terraceHeight;
                        height /= 2;
                    }
                    

                }
            }
        }
    }
}
}

/*
   public int do_noise;
   public float noise_scale;
   public float noise_size;
   
   public int do_terrace;
   public int terrace_iter;
   public float terrace_min;
   public float terrace_max;
   public float terrace_scale;
   public float terrace_tilt;
*/