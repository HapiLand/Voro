using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Core;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Parameters;
using VoroSystem.Voro.Designer.Graph;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem {
[Serializable]
public class NoiseEffect : EffectBase {
  public NoiseEffect() {
    type = EffectName.Noise;
  }

  public override void ConfigureShader() {
    base.ConfigureShader();
    SetParameter<float>("Size");
    SetParameter<float>("Steepness");
  }

  public override void Initialize(Node node) {
    shader = Resources.Load<ComputeShader>("FX/Noise");
    mode = node.operation;
    parameters = node.fields.Select(fb => new EffectParameter(fb.name, fb.defaultValue, fb.type)).ToList();
    base.Initialize(node);
  }
}
}