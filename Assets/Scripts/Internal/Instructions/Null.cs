using System;
using DataTypes;
using UnityEngine;

namespace Internal.Instructions {
public class Null : IEffect {
    readonly IConfiguration _configuration;

    public Null(IConfiguration configuration) {
        _configuration = configuration;
    }

    public float ComputeEffect(float height, Vector3 worldPos) {
        return 0f;
    }

    public void ComputeEffect(ref Cell cell, Vector3 worldPoint) {
        throw new NotImplementedException();
    }
}
}