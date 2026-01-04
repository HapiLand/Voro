using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Voro.Internal.Terrain.Attributes;
using VoroSystem.VoroWorldGeneration.HeightSystem;

namespace Voro.Internal.Terrain.Algorithms {
/// <summary>
/// Shader Function
/// <example> Noise Shader </example>
/// <example> Slope Shader </example>
/// </summary>
public abstract class TerrainAlgorithm : ScriptableObject {
  #region Mode enum
  [Serializable]
  public enum Mode {
    Set,
    Add,
    Multiply,
    Divide
  }
  #endregion

  #region Serialized Fields
  public string title;
  public Mode mode = Mode.Set;
  public Parameter[] parameters;
  public HeightGenerator heightGenerator;
  #endregion

  protected void CollectParameters() {
    var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    var paramList = new List<Parameter>();
    foreach (var field in fields) {
      var attr = field.GetCustomAttribute<ParameterOfAttribute>();
      if (attr == null) {
        continue;
      }

      var param = new Parameter(field.Name, attr.OfType, attr.DefaultValue);
      field.SetValue(this, param);
      paramList.Add(param);
    }

    parameters = paramList.ToArray();
  }

  [Serializable]
  public class Parameter {
    #region Serialized Fields
    public string name;
    #endregion

    public Parameter(string name, Type type, object defaultValue) {
      this.name = name;
      Type = type;
      DefaultValue = defaultValue;
    }

    public Type Type { get; }
    public object DefaultValue { get; }
  }

  [Serializable]
  public class HeightGenerator {
    ComputeBuffer _inputBuffer;
    int _kernelIndex;
    ComputeBuffer _resultBuffer;
    ComputeShader _shader;

    HeightGenerator(ComputeShader shader, string kernelName) {
      _shader = shader;
      _kernelIndex = _shader.FindKernel(kernelName);
    }

    public static HeightGenerator Create(ComputeShader shader, string kernelName) => new(shader, kernelName);

    public void SetParameters(Parameter[] parameters) {
      foreach (var p in parameters) {
        _shader.SetFloat(p.name, 0.0f);
      }
    }


    Func<(float x, float z), float> HeightFunc() {
      return pos => {
        var inputX = new[] { pos.x };
        var inputZ = new[] { pos.z };
        var result = new float[1];

        using (var bufferX = new ComputeBuffer(1, sizeof(float)))
        using (var bufferZ = new ComputeBuffer(1, sizeof(float)))
        using (var bufferResult = new ComputeBuffer(1, sizeof(float))) {
          bufferX.SetData(inputX);
          bufferZ.SetData(inputZ);
          bufferResult.SetData(result);

          _shader.SetBuffer(_kernelIndex, "InputX", bufferX);
          _shader.SetBuffer(_kernelIndex, "InputZ", bufferZ);
          _shader.SetBuffer(_kernelIndex, "Result", bufferResult);

          _shader.Dispatch(_kernelIndex, 1, 1, 1);

          bufferResult.GetData(result);
        }

        return result[0];
      };

      Array ProvideUntyped(TerrainRegion region, Vector3[] vertices) => Provide(region, vertices);

      float[] Provide(TerrainRegion region, Vector3[] vertices) {
        var result = new float[vertices.Length];
        var heightFunc = HeightFunc();

        for (var i = 0; i < vertices.Length; i++) {
          var worldX = vertices[i].x + region.Center.x - region.Size / 2f;
          var worldZ = vertices[i].z + region.Center.z - region.Size / 2f;
          result[i] = heightFunc((worldX, worldZ));
        }

        return result;
      }
    }
  }
}
}