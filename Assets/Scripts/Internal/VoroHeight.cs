using DataTypes;
using Internal.Configuration;
using Internal.Instructions;
using UnityEngine;

namespace Internal {
/// <summary>
///     computes the elevation of the voro terrain
/// </summary>
public class VoroHeight {
    public VoroHeight((JsonConfiguration, Cell[]) input, Vector2 origin, out float[] outElevation) {
        // the configurations that are a result of the effects that the user
        // selected in the GUI editor
        var configuration = input.Item1.EffectData;

        var voroPos = new Vector3(origin.x, 0f, origin.y);

        // get the world position of every cell
        var cells = input.Item2;
        var cellPositions = new Vector3[input.Item2.Length];
        for (var i = 0; i < cellPositions.Length; i++) {
            cellPositions[i] = cells[i].position;
            cellPositions[i] += voroPos;
        }

        // each effect computes a different result
        // most effects are used to alter the elevation of each cell
        var effects = new IEffect[configuration.Length];
        // the resulting elevation for every cell
        outElevation = new float[cellPositions.Length];

        // determine every effect that was selected in the GUI editor
        for (var i = 0; i < effects.Length; i++) {
            effects[i] = configuration[i] switch
            {
                SlopeCfg => new Slope(configuration[i]),
                NoiseCfg => new Noise(configuration[i]),
                TerraceCfg => new Terrace(configuration[i]),
                NullCfg => new Null(configuration[i]),
                SetGroupCfg => new SetGroup(configuration[i]),
                _ => effects[i]
            };
        }

        // compute each effect to generate the final result
        for (var i = 0; i < cellPositions.Length; i++) {
            var worldPosition = cellPositions[i];
            var height = 0f;

            foreach (var effect in effects) {
                if (effect is SetGroup setGroup) {
                    // this effect is used to set the GroupID for this cell
                    // setGroup.Solve(ref Cell)
                }
                else {
                    // this effect is used to find the elevation at this location
                    height += effect.ComputeEffect(height, worldPosition);
                }
            }

            outElevation[i] = height;
        }
    }
}
}