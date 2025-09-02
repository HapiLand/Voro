using DataTypes;
using UnityEngine;

namespace Internal {
public interface IEffect {
    float ComputeEffect(float height, Vector3 worldPoint);
    void ComputeEffect(ref Cell cell, Vector3 worldPoint);
}
}