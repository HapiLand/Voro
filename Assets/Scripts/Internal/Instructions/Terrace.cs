using System;
using DataTypes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Internal.Instructions {
public class Terrace : IEffect {
    readonly IConfiguration _configuration;

    public Terrace(IConfiguration configuration) {
        _configuration = configuration;
    }

    // something I dislike with other terrace functions is that those
    // typically are limited by ONLY creating a regular staircase size.
    // those do their job, but end up looking far too artificial. yuck.
    // terraces in reality are hardly always the same size, see: Banaue Rice Terraces

    // in this approach, it is designed to create steps with some randomness
    // the height of each step can vary between a min and max height
    // this allows terracing to appear far more natural
    public float ComputeEffect(float height, Vector3 worldPoint) {
        // the iteration controls how much randomness occurs
        // at 0, the step size is regular
        // increasing the iteration causes the height to be moved
        // by a random amount for every step
        var iterations = _configuration.PropertiesArray[0];
        // the min and max value that the iteration will move the height
        // higher values causes the height to change by a lot
        var min = _configuration.PropertiesArray[1];
        var max = _configuration.PropertiesArray[2];
        // step scale is how large the width of the steps are
        // lower values produces steps that are very thin, so more exist
        // higher values causes the steps to be very wide, so there are less in total
        var stepScale = _configuration.PropertiesArray[3];
        // this controls which direction the terrace is applied
        // ( see Slope.direction on how the direction works, both uses are the same )
        // it can allow the slope of terrain to move in one direction
        // and a terrace effect is then applied in another direction
        var direction = _configuration.PropertiesArray[4];
        // in this demo, the slopes direction moves along the X axis
        // the json configuration has a direction rotated 41 degrees
        // the terrace ends up moving down along the slope
        // but the steps all run diagonally down that slope

        // find the direction the terrace
        var radians = direction * Mathf.Deg2Rad;
        var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        var terraceHeight = Vector2.Dot(new Vector2(worldPoint.x, worldPoint.z), axis);

        var h = terraceHeight;

        var div = h / stepScale;
        var flat = Mathf.Floor(div);
        var seed = 0;
        Random.InitState(Mathf.RoundToInt(flat) + seed);
        var val = Random.value;
        val = remap(val, min, max) * iterations;

        float remap(float value, float newMin, float newMax) {
            // remap a value from an old range of [0,1] into a new range [min,max]
            // val = 0.5 | newMin = 10 | newMax = 20
            // result = 15
            // Debug.Log(fit01(0.5f, 10f, 20f));
            return value * (newMax - newMin) + newMin;
        }

        // find the final value of the terrace
        var level = (flat + val) * stepScale;

        level /= 2f;

        // apply the value to the height
        return level;
    }

    public void ComputeEffect(ref Cell cell, Vector3 worldPoint) {
        throw new NotImplementedException();
    }
}
}