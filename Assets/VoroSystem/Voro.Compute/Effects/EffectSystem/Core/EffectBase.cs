using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.Util.Extensions;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Parameters;
using VoroSystem.Voro.Designer.Graph;
using VoroSystem.Voro.World.TerrainOLD.Ground.Chunks;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem.Core {
[Serializable]
public abstract class EffectBase : IEffect {
  #region Serialized Fields

  /// <summary>
  /// blending mode, how the effect alters the texture, set/add/subtract/multiply
  /// </summary>
  [SerializeField] public EffectOperation mode;

  /// <summary>
  /// type of effect to find matching shader
  /// </summary>
  [SerializeField] public EffectName type;

  /// <summary>
  /// sets parameters in shader
  /// </summary>
  [SerializeField] public List<EffectParameter> parameters;

  /// <summary>
  /// shader that produces the texture
  /// </summary>
  [SerializeField] public ComputeShader shader;

  /// <summary>
  /// the computed texture the shader produces
  /// </summary>
  [SerializeField] public RenderTexture heightmap;

  /// <summary>
  /// data value for the shader
  /// </summary>
  [SerializeField] public EffectShaderData shaderData;

  #endregion

  #region IEffect Members

  public Texture2D ReadResult() {
    var tex = new Texture2D(shaderData.textureSize, shaderData.textureSize, TextureFormat.ARGB32, false);
    RenderTexture.active = heightmap;
    tex.ReadPixels(new Rect(0, 0, shaderData.textureSize, shaderData.textureSize), 0, 0);
    tex.Apply();
    RenderTexture.active = null;
    tex.filterMode = FilterMode.Point;
    return tex;
  }

  /// <summary>
  /// computes the heightmap texture
  /// </summary>
  public virtual void Compute(ChunkInstance instance) {
    SetShaderTexture();

    var offset = instance.gameObject.transform.position.ToVector2();
    var x = offset.x;
    var y = offset.y;
    shader.SetFloat("OffsetX", x);
    shader.SetFloat("OffsetY", y);
    shader.SetInt("TextureSize", shaderData.textureSize);

    DispatchShader();
  }

  public virtual void ConfigureShader() {
    shader.SetInt("Operation", (int)mode);
  }

  #endregion

  protected void SetParameter<T>(string name) {
    var field = parameters.FirstOrDefault(f => string.Equals(f.name, name));
    if (field == null) {
      return;
    }

    var value = Convert.ChangeType(field.defaultValue, typeof(T));
    if (typeof(T) == typeof(float)) {
      shader.SetFloat(name, (float)value);
    }
    else if (typeof(T) == typeof(bool)) {
      shader.SetBool(name, (bool)value);
    }
    else if (typeof(T) == typeof(int)) {
      shader.SetInt(name, (int)value);
    }
  }

  /// <summary>
  /// initialises the base effect with fields that are common in every type effect
  /// </summary>
  /// <param name="node"> </param>
  public virtual void Initialize(Node node) {
    shaderData = new EffectShaderData(Shader.PropertyToID("Result"),
      256,
      shader.FindKernel("CSMain"),
      RenderTextureFormat.ARGB32);
    heightmap = new RenderTexture(shaderData.textureSize, shaderData.textureSize, 0, shaderData.textureFormat)
    {
      enableRandomWrite = true
    };
    heightmap.Create();
  }

  /// <summary>
  /// dispatches the effects shader to generate the result texture
  /// </summary>
  void DispatchShader() {
    var threadGroups = shaderData.textureSize / 8;
    shader.Dispatch(shaderData.kernel, threadGroups, threadGroups, 1);
  }

  void SetShaderTexture() {
    shader.SetTexture(shaderData.kernel, shaderData.result, heightmap);
  }
}
}