using DataTypes;
using UnityEngine;

namespace Internal.Instructions {
public class Noise : INode {
    IConfig config;
    public Noise(IConfig config) {
        this.config = config;
    }
    public float Solve(float height, Vector3 worldPoint) {
        var scale = this.config.ConfigArr[0];
        var size = this.config.ConfigArr[1];
        var perlin = new Perlin();
        double dx = Mathf.Abs(worldPoint.x * size);
        double dy = Mathf.Abs(worldPoint.y * size);
        double dz = Mathf.Abs(worldPoint.z * size);
        var noise = perlin.Noise(dx, dy, dz);
        noise *= scale;
        return (float)noise;
    }

    public void Solve(ref Cell cell, Vector3 worldPoint) {
        throw new System.NotImplementedException();
    }
}
}