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
        // ToDo replace compute method to use an iterative one
        //  for all points marked as active, copy their elevation
        //  on to all points which are forwards of the active points
        //  (so now they have a matching height)
        //  also add an additional amount
        //  (so now those points are raise up)
        //  set only these points as active
        //  repeat while any points are still waiting to be computed

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