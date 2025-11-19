using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Compute.EffectSystem.Core;
using VoroSystem.Compute.EffectSystem.Parameters;
using VoroSystem.Designer.GraphSystem;

namespace VoroSystem.Compute.EffectSystem {
[Serializable]
public class SlopeEffect : EffectBase {
  public SlopeEffect() {
    type = EffectName.Slope;
  }

  public override void ConfigureShader() {
    base.ConfigureShader();
    SetParameter<float>("Direction");
    SetParameter<float>("Steepness");
    SetParameter<bool>("Reverse");
  }

  public override void Initialize(Node node) {
    shader = Resources.Load<ComputeShader>("FX/Slope");
    mode = node.operation;
    parameters = node.fields.Select(fb => new EffectParameter(fb.name, fb.defaultValue, fb.type)).ToList();
    base.Initialize(node);
  }
}
}