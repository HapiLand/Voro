using UnityEngine;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.EffectDefinitions {
public class NoiseShader<TParam> : TypedBaseShader<TParam> where TParam : class {
  public NoiseShader(TParam parameters) : base(parameters) {
    ComputeShader = Resources.Load<ComputeShader>("FX/Noise");
    
    SetOperationMode();
    
    SetParameter<float>("Size");
    SetParameter<float>("Steepness");
  }

  public override void SetParameter<T>(string name) {
    var prop = Parameters.GetType().GetProperty(name);
    if (prop == null) {
      Debug.LogError($"Property '{name}' not found on {Parameters.GetType().Name}");
      return;
    }
    var value = prop.GetValue(Parameters);
    if (typeof(T) == typeof(float)) {
      ComputeShader.SetFloat(name, (float)value);
      Debug.Log($"[Noise] {typeof(T)} Parameter: {name} = {(float)value}");
    }
    else if (typeof(T) == typeof(bool)) {
      ComputeShader.SetBool(name, (bool)value);
      Debug.Log($"[Noise] {typeof(T)} Parameter: {name} = {(bool)value}");
    }
  }

  public override Texture2D ReadResult() {
    var tex = new Texture2D(TextureSize, TextureSize, TextureFormat.ARGB32, false);
    RenderTexture.active = Texture;
    tex.ReadPixels(new Rect(0, 0, TextureSize, TextureSize), 0, 0);
    tex.Apply();
    RenderTexture.active = null;
    tex.filterMode = FilterMode.Point;
    return tex;
  }

  public override void Compute(Chunk chunk) {
    SetShaderBuffer();

    var offset = chunk.Entity.transform.position.ToVector2();
    ComputeShader.SetFloat("OffsetX", offset.x);
    ComputeShader.SetFloat("OffsetY", offset.y);

    ComputeShader.Dispatch(0, chunk.VertexPerAxis, 1, 1);
    return;

    void SetShaderBuffer() {
      var buffer = chunk.PointBuffer;
      ComputeShader.SetBuffer(0, "VertexBuffer", buffer);
    }
  }
}
}