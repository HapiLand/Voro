using System;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.Compute.EffectSystem.Parameters;

namespace VoroSystem.Voro.Compute.EffectSystem {
[Serializable]
public class TerraceEffect : EffectBase {
    public TerraceEffect() {
        type = EffectType.Terrace;
    }

    public override void ConfigureShader() {
        base.ConfigureShader();
        SetParameter<float>("Direction");
        SetParameter<int>("Iterations");
        SetParameter<float>("MinStepSize");
        SetParameter<float>("MaxStepSize");
        SetParameter<float>("StepSize");
    }

    public override void Initialize(INode node) {
        shader = Resources.Load<ComputeShader>("FX/Terrace");
        mode = node.Mode;
        parameters = node.Fields.Select(fb => new EffectParameter(fb.name, fb.defaultValue, fb.type)).ToList();
        base.Initialize(node);
    }
}
}