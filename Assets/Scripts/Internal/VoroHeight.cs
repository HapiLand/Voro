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
    
    // ToDo store the instructions as a collection
    
    // the new way to store configruations for each instruction
    // this originated from the json file
    // now when VoroHeight can find the height, it will iterate through this array
    public IConfig[] Configs;
    
    Noise _noiseNde;
    Slope _slopeNde;
    Terrace _terraceNde;
    
    public VoroHeight(JsonConfig config) {
        // generate the various IConfig structs found in the config data
        // these configurations contain the data in the json file
        // these are used to configure each instruction to control its output
        /*_slopeCfg = config.Configs[0] is SlopeCfg ? (SlopeCfg)config.Configs[0] : default;
        _noiseCfg = config.Configs[1] is NoiseCfg ? (NoiseCfg)config.Configs[1] : default;
        _terraceCfg = config.Configs[2] is TerraceCfg ? (TerraceCfg)config.Configs[2] : default;*/

        Configs = config.Configs;
        
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

            for (var i = 0; i < Configs.Length; i++) {
                // read the current configuration
                IConfig cfg = Configs[i];
                
                // determine the actual IConfig struct this cfg is
                if (cfg is SlopeCfg slope) {
                    // the IConfigs type is found
                    // ths configuration is provided to the Instruction
                    // the result of this is that height is computed where the resulting height
                    // has been controlled by the instruction based on what the config says
                    // for a Slope effect, the config says how steep the slope is and what its direction is
                    
                    _slopeNde.ComputeHeight(slope, new Vector3(pos.x, height, pos.y), out var slopeHeight);
                    height += slopeHeight;
                }
                else if (cfg is NoiseCfg noise) {
                    _noiseNde.ComputeHeight(noise, new Vector3(pos.x, height, pos.y), out var noiseHeight);
                    height += noiseHeight;
                }
                else if (cfg is TerraceCfg terrace) {
                    _terraceNde.ComputeHeight(terrace, new Vector3(pos.x, height, pos.y), out var terraceHeight);
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