using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Compute.EffectSystem.Core;
using VoroSystem.Compute.EffectSystem.Parameters;
using VoroSystem.Designer.GraphSystem;

namespace VoroSystem.Compute.EffectSystem {
[Serializable]
public class FlatEffect : EffectBase {
  public FlatEffect() {
    type = EffectName.Flat;
  }

  public override void ConfigureShader() {
    base.ConfigureShader();
    SetParameter<float>("Height");
  }

  public override void Initialize(Node node) {
    shader = Resources.Load<ComputeShader>("FX/Flat");
    mode = node.operation;
    parameters = node.fields.Select(fb => new EffectParameter(fb.name, fb.defaultValue, fb.type)).ToList();
    base.Initialize(node);
  }
}
}