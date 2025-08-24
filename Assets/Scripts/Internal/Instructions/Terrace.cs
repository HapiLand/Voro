using UnityEngine;

namespace Internal.Instructions {
public class Terrace : INode {
    // this is an instruction for a terrace effect
    // the terrace can produce a staircase pattern along a given direction
    public Terrace() { }

    public void ComputeHeight(IConfig configuration, Vector3 worldPos, out float height) {
        // something I dislike with other terrace functions is that those
        // typically are limited by ONLY creating a regular staircase size.
        // those do their job, but end up looking far too artificial. yuck.
        // terraces in reality are hardly always the same size, see: Banaue Rice Terraces

        // in this approach, it is designed to create steps with some randomness
        // the height of each step can vary between a min and max height
        // this allows terracing to appear far more natural

        // ToDo improve the configuration design so it is more intuitive
        var doTerrace = configuration.ConfigArr[0];
        // the iteration controls how much randomness occurs
        // at 0, the step size is regular
        // increasing the iteration causes the height to be moved
        // by a random amount for every step
        var iterations = configuration.ConfigArr[1];
        // the min and max value that the iteration will move the height
        // higher values causes the height to change by a lot
        var min = configuration.ConfigArr[2];
        var max = configuration.ConfigArr[3];
        // step scale is how large the width of the steps are
        // lower values produces steps that are very thin, so more exist
        // higher values causes the steps to be very wide, so there are less in total
        var stepScale = configuration.ConfigArr[4];
        // this controls which direction the terrace is applied
        // ( see Slope.direction on how the direction works, both uses are the same )
        // it can allow the slope of terrain to move in one direction
        // and a terrace effect is then applied in another direction
        var direction = configuration.ConfigArr[5];
        // in this demo, the slopes direction moves along the X axis
        // the json configuration has a direction rotated 41 degrees
        // the terrace ends up moving down along the slope
        // but the steps all run diagonally down that slope

        if (doTerrace == 0) {
            height = 0f;
            return;
        }

        // find the direction the terrace
        var radians = direction * Mathf.Deg2Rad;
        var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        var terraceHeight = Vector2.Dot(new Vector2(worldPos.x, worldPos.z), axis);

        var h = terraceHeight;

        var div = h / stepScale;
        var flat = Mathf.Floor(div);
        var seed = 0;
        Random.InitState(Mathf.RoundToInt(flat) + seed);
        var val = Random.value;
        val = fit01(val, min, max) * iterations;

        float fit01(float value, float newMin, float newMax) {
            // remap a value from an old range of [0,1] into a new range [min,max]
            // val = 0.5 | newMin = 10 | newMax = 20
            // result = 15
            // Debug.Log(fit01(0.5f, 10f, 20f));

            return value * (newMax - newMin) + newMin;
        }

        // find the final value of the terrace
        var level = (flat + val) * stepScale;
        // apply the value to the height
        height = level;
    }
}
}