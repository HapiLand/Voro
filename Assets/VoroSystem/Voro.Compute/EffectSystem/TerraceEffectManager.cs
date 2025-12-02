using System;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.EffectSystem {
[Serializable]
public class TerraceEffectManager : EffectManager {
    public TerraceEffectManager(INode node) {
        Effect = (TerraceEffect)MakeEffect(node);
        Name = "Terrace Effect";
    }

    public override string Name { get; }

    protected override IEffect MakeEffect(INode node) {
        var effect = new TerraceEffect();
        effect.Initialize(node);
        return effect;
    }
}
}