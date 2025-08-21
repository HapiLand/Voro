using System;
using Internal.Configuration;
using UnityEngine;

namespace DataTypes {

/* v1
 * work in progress, config stores data in a dumb way
 * if the json could work as an expression, where the expression
 * is used to tell the voro how to produce the noise value
 *
 * the json config is currently structured as [ { value1, value2, value3 } ]
 * its array contains a single item, if this is changed to
 * [ { value1 }, { value2 }, { value3 } ] then the config now stores 3 items
 *
 * the expression for the config can allow each item to alter the height in a sequence
 * each item in the expression is an instruction
 * [ { slope }, { noise }, { terrace } ]
 *
 * v2
 * json to be restructured in order to match the expression style
 * the new design will format the data as
 * [ { do_slope, dir, mult },  { do_noise, scale, size }, { do_terrace, iter, min, max, scale, tilt } ]
 *
 * v3
 * at the moment the config json always has 3 arrays per config, this is only temporary.
 * the actual config json could have any number of configurations inside it
 * somehow the json is going to have to store its float arrays in a way
 * that lets JsonConfig be constructed with any amount of float[]
 * something possibly like [ { float[], float[], float[], float[], float[] } ]
 * where each float[] is what is storing the values like { do_noise, scale, size }
 * using that approach, JsonConfig only would need to store a single array for each public IConfig[] foo;
 *
 * v4
 * the json has been updated so the configuration is stored as
 * { IConfig{ float[] }, IConfig{ float[] }, IConfig{ float[] } }
 * once the json stores an array of the IConfig structs, the VoroHeight method can start to be automated
 * right now the configuration system that VoroHeight is using, is basically all hard coded
 */

// ToDo json needs to store an array of IConfig as the amount of IConfig stored can change

[Serializable]
public class JsonConfig {
    public SlopeCfg slope;
    public NoiseCfg noise;
    public TerraceCfg terrace;
}
}