using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Voro.Core.World;

namespace Voro.Core.Effects.Internal {
/// <summary> Common functionality for all Effects </summary>
abstract class BaseEffect : IEffect {
    protected EffectOperation Operation { get; set; }
    protected List<FXField> Fields { get; set; }
    protected abstract string EffectName { get; }
    public ComputeShader Shader { get; set; }
    public ComputeBuffer Buffer { get; set; }

    public virtual void Initialize(ConfigFX config) {
        Shader = Resources.Load<ComputeShader>(EffectName);
        Operation = Enum.TryParse(config.Operation, true, out EffectOperation parsed)
            ? parsed
            : EffectOperation.Set;
        Fields = config.Fields?
            .Select(f => new FXField(f))
            .ToList() ?? new List<FXField>();
    }

    public virtual void ReadResult(BaseResult baseResult) {
        var result = new Vertex[Buffer.count];
        Buffer.GetData(result);
        Buffer.Release();
        baseResult.GiveResult(result);
    }

    public virtual void Compute(BaseResult baseResult) {
        Debug.Log($"[{EffectName} Effect] Compute");
        CreateBuffer(baseResult);
        DispatchShader();
    }

    public virtual void ConfigureShader() {
        Shader.SetInt("operation", (int)Operation);
    }

    protected void SetParameter<T>(string name) {
        var field = Fields.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
        if (field == null) {
            return;
        }

        var value = Convert.ChangeType(field.DefaultValue, typeof(T));
        if (typeof(T) == typeof(float)) {
            Shader.SetFloat(name, (float)value);
        }
        else if (typeof(T) == typeof(bool)) {
            Shader.SetBool(name, (bool)value);
        }
        else if (typeof(T) == typeof(int)) {
            Shader.SetInt(name, (int)value);
        }
    }

    void DispatchShader() {
        Shader.GetKernelThreadGroupSizes(0, out var x, out var y, out var z);
        var group = new Vector3Int((int)x, (int)y, (int)z);
        var gx = Mathf.CeilToInt(Buffer.count / (float)group.x);
        var gy = Mathf.CeilToInt(1 / (float)group.y);
        var gz = Mathf.CeilToInt(1 / (float)group.z);
        Shader.Dispatch(0, gx, gy, gz);
    }

    void CreateBuffer(BaseResult baseResult) {
        var vertices = baseResult.QuadVertices.ToArray();
        Buffer = new ComputeBuffer(vertices.Length, Marshal.SizeOf(typeof(Vertex)));
        Buffer.SetData(vertices);
        Shader.SetBuffer(0, "vertex_buffer", Buffer);
        Shader.SetInt("vertex_count", Buffer.count);
    }
}
}