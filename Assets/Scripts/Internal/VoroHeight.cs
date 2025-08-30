using DataTypes;
using Internal.Configuration;
using Internal.Instructions;
using UnityEngine;

namespace Internal {
/// <summary>
///     computes the elevation of the voro terrain
/// </summary>
// ToDo correct language in library to remove confusion (ie Effect/Instruction)
public class VoroHeight {
    /// <summary>
    ///     new version, height is solved on construction
    /// </summary>
    /// <param name="input">config.json, voro points</param>
    /// <param name="origin">the bottom-left corner of a square</param>
    /// <param name="outHeight">array of generated height value</param>
    public VoroHeight((JsonConfig, Cell[]) input, Vector3 origin, out float[] outHeight) {
        // the container of ui elements
        var configuration = input.Item1.EffectData;

        // get the world position of every point
        var voroPoints = input.Item2;
        var points = new Vector3[input.Item2.Length];
        for (var i = 0; i < points.Length; i++) {
            points[i] = voroPoints[i].position;
            points[i] += origin;
        }

        // what computes the height
        var solvers = new INode[configuration.Length];
        // the resulting elevation
        outHeight = new float[points.Length];

        // construct all the solvers that will produce the effect
        for (var i = 0; i < solvers.Length; i++) {
            solvers[i] = configuration[i] switch
            {
                // each solver is constructed with the configuration
                // pre-warm the solvers
                SlopeCfg => new Slope(configuration[i]),
                NoiseCfg => new Noise(configuration[i]),
                TerraceCfg => new Terrace(configuration[i]),
                _ => solvers[i]
            };
        }

        // solve the entire height map for the input
        // the height is found for every point
        for (var i = 0; i < points.Length; i++) {
            var worldPoint = points[i];
            var height = 0f;

            // for every effect that is in the configuration
            // solve the height at this world position
            foreach (var effect in solvers) {
                // find the height at the world position
                height += effect.Solve(height, worldPoint);
            }

            outHeight[i] = height;
        }
    }
}
}