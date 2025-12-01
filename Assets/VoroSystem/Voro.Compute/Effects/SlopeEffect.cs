using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Compute.Effects.Parameters;
using VoroSystem.Voro.Compute.Graphs;

namespace VoroSystem.Voro.Compute.Effects {
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