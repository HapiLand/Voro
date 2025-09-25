#include "PointData.hlsl"
#include "SimplexNoise3D.hlsl"

float ConstantHeight(float heightValue)
{
    return heightValue;
}

float Noise(PointData pnt, float noiseScale)
{
    float3 pos = float3(pnt.P.xyz) * noiseScale;
    return SimplexNoise(pos);
}
