using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Compute.Effects.Parameters;
using VoroSystem.Voro.Designer.Canvas;

namespace VoroSystem.Voro.Compute.Effects {
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