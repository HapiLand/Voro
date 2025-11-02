using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using VoroSystem.World.FX.Configuration;
using VoroSystem.World.Generate;

namespace VoroSystem.World.FX.Base {
/// <summary> Common functionality for all Effects </summary>
abstract class BaseEffect : IEffect {
    protected EffectOperation Operation { get; set; }
    protected List<FXField> Fields { get; set; }
    protected abstract string EffectName { get; }


    public ComputeShader Shader { get; set; }
    public ComputeBuffer Buffer { get; set; }

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
        Buffer = new ComputeBuffer(vertices.Length, Marshal.SizeOf(typeof(MeshVertex)));
        Buffer.SetData(vertices);
        Shader.SetBuffer(0, "vertex_buffer", Buffer);
        Shader.SetInt("vertex_count", Buffer.count);
    }

    public virtual void Initialize(JConfigFX? config) {
        if (config.HasValue) {
            Shader = Resources.Load<ComputeShader>(EffectName);
            Operation = Enum.TryParse(config.Value.Operation, true, out EffectOperation parsed)
                ? parsed
                : EffectOperation.Set;
            Fields = config.Value.Fields?
                .Select(f => new FXField(f))
                .ToList() ?? new List<FXField>();
            return;
        }

        Shader = Resources.Load<ComputeShader>("Slope");
        Operation = EffectOperation.Set;
        Fields = new List<FXField>
        {
            new(new JConfigField { FieldName = "Direction", FieldType = "Radial", DefaultValue = 0f }),
            new(new JConfigField { FieldName = "Steepness", FieldType = "FloatSlider", DefaultValue = 1f }),
            new(new JConfigField { FieldName = "Reverse", FieldType = "Toggle", DefaultValue = false })
        };
    }

    public virtual void ReadResult(BaseResult baseResult) {
        var result = new MeshVertex[Buffer.count];
        Buffer.GetData(result);
        Buffer.Release();
        baseResult.GiveResult(result);
    }

    public virtual void Compute(BaseResult baseResult) {
        // Debug.Log($"[{EffectName} Effect] Compute");
        CreateBuffer(baseResult);
        DispatchShader();
    }

    public virtual void ConfigureShader() {
        Shader.SetInt("operation", (int)Operation);
    }
}
}