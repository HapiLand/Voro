using System;
using DataTypes;
using UnityEngine;

namespace Internal.Instructions {
public class Slope : IEffect {
    readonly IConfiguration _configuration;

    public Slope(IConfiguration configuration) {
        Debug.Log(configuration);
        _configuration = configuration;
    }

    public float ComputeEffect(float height, Vector3 worldPos) {
        // ToDo replace with a real solve method

        var direction = _configuration.PropertiesArray[0];
        var multiplier = _configuration.PropertiesArray[1];
        var radians = direction * Mathf.Deg2Rad;
        var axis = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        // ToDo direction value over 180 causes slope to be negative
        var slopeHeight = Vector2.Dot(new Vector2(worldPos.x, worldPos.z), axis);

        slopeHeight *= multiplier;
        return slopeHeight;
    }

    public void ComputeEffect(ref Cell cell, Vector3 worldPoint) {
        throw new NotImplementedException();
    }
}
}