using System;
using DataTypes;
using UnityEngine;

namespace Internal.Instructions {
public class Noise : IEffect {
    readonly IConfiguration _configuration;

    public Noise(IConfiguration configuration) {
        _configuration = configuration;
    }

    public float ComputeEffect(float height, Vector3 worldPoint) {
        var scale = _configuration.PropertiesArray[0];
        var size = _configuration.PropertiesArray[1];
        var perlin = new Perlin();
        double dx = Mathf.Abs(worldPoint.x * size);
        double dy = Mathf.Abs(worldPoint.y * size);
        double dz = Mathf.Abs(worldPoint.z * size);
        var noise = perlin.Noise(dx, dy, dz);
        noise *= scale;
        return (float)noise;
    }

    public void ComputeEffect(ref Cell cell, Vector3 worldPoint) {
        throw new NotImplementedException();
    }
}
}