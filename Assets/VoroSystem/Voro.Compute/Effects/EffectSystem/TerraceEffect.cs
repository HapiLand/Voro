using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Core;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Parameters;
using VoroSystem.Voro.Designer.Graph;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem {
[Serializable]
public class TerraceEffect : EffectBase {
  public TerraceEffect() {
    type = EffectName.Terrace;
  }

  public override void ConfigureShader() {
    base.ConfigureShader();
    SetParameter<float>("Direction");
    SetParameter<int>("Iterations");
    SetParameter<float>("MinStepSize");
    SetParameter<float>("MaxStepSize");
    SetParameter<float>("StepSize");
  }

  public override void Initialize(Node node) {
    shader = Resources.Load<ComputeShader>("FX/Terrace");
    mode = node.operation;
    parameters = node.fields.Select(fb => new EffectParameter(fb.name, fb.defaultValue, fb.type)).ToList();
    base.Initialize(node);
  }
}
}