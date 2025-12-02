using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.Compute.EffectSystem.Parameters;

namespace VoroSystem.Voro.Compute.EffectSystem {
[Serializable]
public class FlatEffect : EffectBase {
    public FlatEffect() {
        type = EffectType.Flat;
    }

    public override void ConfigureShader() {
        base.ConfigureShader();
        SetParameter<float>("Height");
    }

    public override void Initialize(INode node) {
        shader = Resources.Load<ComputeShader>("FX/Flat");
        mode = node.Mode;
        parameters = node.Fields.Select(fb => new EffectParameter(fb.name, fb.defaultValue, fb.type)).ToList();
        base.Initialize(node);
    }
}
}