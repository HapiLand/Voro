using DataTypes;
using Internal.Configuration;
using Internal.Instructions;
using UnityEngine;

namespace Internal {
// to be used to produce height value from a json config
public class VoroHeight {

    // the internal names for these types are IConfig and INode
    // "Node" was chosen partly for convenience to save writing "Instruction"
    // and technically each instruction is a single object where all instructions
    // are executed in a sequence, not unlike a series of connected nodes
    // Slope -> Noise -> Terrace
    // if a GUI is to ever exist to allow you to build a configuration
    // these instructions could probably exist as Nodes for a Node Tree
    
    NoiseCfg _noiseCfg;
    Noise _noiseNde;
    
    SlopeCfg _slopeCfg;
    Slope _slopeNde;
    
    TerraceCfg _terraceCfg;
    Terrace _terraceNde;
    
    public VoroHeight(JsonConfig config) {
        // generate the various IConfig structs found in the config data
        // these configurations contain the data in the json file
        // these are used to configure each instruction to control its output
        _slopeCfg = config.slope;
        _noiseCfg = config.noise;
        _terraceCfg = config.terrace;

        _slopeNde = new Slope();
        _noiseNde = new Noise();
        _terraceNde = new Terrace();
    }

    public void GetHeight(PointArray pointArr, Vector3 offset, out float[] heightArr) {
        /*
         * v2 will use an array of IConfig objects, iterate through each IConfig
         * the config is provided to the related Instruction, and a height value
         * can be returned
         * this new approach will aim to clear out all the Slope/Noise/Terrace code
         * which is hardcoded into this class
         */
        
        
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
                // the current iteration has the instruction to create a slope
                if (i == 0) {
                    _slopeNde.ComputeHeight(_slopeCfg, new Vector3(pos.x, height, pos.y), out var slopeHeight);
                    height += slopeHeight;
                }
                
                // the current iteration has the instruction to apply noise
                if (i == 1) {
                    _noiseNde.ComputeHeight(_noiseCfg, new Vector3(pos.x, height, pos.y), out var noiseHeight);
                    height += noiseHeight;
                }
                
                // the current iteration has the instruction to apply a terrace effect
                if (i == 2) {
                    _terraceNde.ComputeHeight(_terraceCfg, new Vector3(pos.x, height, pos.y), out var terraceHeight);
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