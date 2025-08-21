using System;

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
 */


[Serializable]
public class JsonConfig {
    public float[] slope;
    // f[]@slope = { do_slope, slope_dir, slope_mult };
    
    public float[] noise;
    // f[]@noise = { do_noise, noise_scale, noise_size };
    
    public float[] terrace;
    // f[]@terrace = { do_terrace, terrace_iter, terrace_min, terrace_max, terrace_scale, terrace_tilt };
}
}