using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.Compute.EffectSystem.Parameters;

namespace VoroSystem.Voro.Compute.EffectSystem {
[Serializable]
public class SlopeEffect : EffectBase {
  public SlopeEffect() {
    type = EffectType.Slope;
  }

  public override void ConfigureShader() {
    base.ConfigureShader();
    SetParameter<float>("Direction");
    SetParameter<float>("Steepness");
    SetParameter<bool>("Reverse");
  }

  public override void Initialize(Node node) {
    shader = Resources.Load<ComputeShader>("FX/Slope");
    mode = node.Mode;
    parameters = node.Fields.Select(fb => new EffectParameter(fb.name, fb.defaultValue, fb.type)).ToList();
    base.Initialize(node);
  }
}
}