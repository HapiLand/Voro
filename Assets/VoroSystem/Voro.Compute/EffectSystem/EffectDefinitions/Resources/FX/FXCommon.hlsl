int Operation;
int TextureSize;
RWTexture2D<float4> Result;
float OffsetX;
float OffsetY;

float ApplyOperation(float o, float s)
{
    switch (Operation)
    {
    case 0:
        // None
        o = s;
        break;

    case 1:
        // Set - Overwrites any existing height
        o = s;
        break;

    case 2:
        // Subtract - Reduces existing height by solved
        o -= s;
        break;

    case 3:
        // Add - Increases existing height by solved
        o += s;
        break;

    case 4:
        // Multiply - Scales existing height by solved
        o *= s;
        break;

    default: return o;
    }
    return o;
}

float2 GetUV(uint3 id)
{
    float2 co = float2(OffsetX, OffsetY);
    float2 uv = float2(id.xy) / TextureSize;
    uv += co;
    return uv;

    /*float2 co = float2(OffsetX, OffsetY);
    float2 uv = float2(id.xy) / TextureSize;
    uv += co;
    return saturate(uv);*/
}
