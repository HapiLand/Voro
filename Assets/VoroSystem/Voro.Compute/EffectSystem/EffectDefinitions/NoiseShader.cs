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
      Debug.Log($"[{Parameters.GetType().Name}] {name} = {(float)value}");
    }
    else if (typeof(T) == typeof(bool)) {
      ComputeShader.SetBool(name, (bool)value);
      Debug.Log($"[{Parameters.GetType().Name}] {name} = {(bool)value}");
    }
  }

  public override void Compute(Chunk chunk) {
    ComputeShader.SetBuffer(0, "VertexBuffer", chunk.PointBuffer);

    var offset = chunk.Entity.transform.position.ToVector2();
    ComputeShader.SetFloat("OffsetX", offset.x);
    ComputeShader.SetFloat("OffsetY", offset.y);

    ComputeShader.Dispatch(0, chunk.VertexPerAxis, 1, 1);
  }
}
}