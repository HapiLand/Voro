using System;
using UnityEditor;
using VoroSystem.GraphEditor.Effects;
using VoroSystem.GraphEditor.Effects.Types;

namespace VoroSystem.GraphEditor.UserInterface.Elements {
public class Node : INode {
    Node(IEffect effect) {
        Name = effect.ToString();
        Effect = effect;
    }

    public IEffect Effect { get; }

    public void Draw() {
        EditorGUILayout.BeginVertical("box");
        // draw the Effect, which draws Control
        Effect.Draw();
        EditorGUILayout.EndVertical();
    }

    public string Name { get; }

    public static Node CreateInstance(EffectType effectType) {
        switch (effectType) {
        case EffectType.Constant:
            return new Node(ConstantEffect.CreateInstance());
        case EffectType.Slope:
            return new Node(SlopeEffect.CreateInstance());
        case EffectType.Noise:
            return new Node(NoiseEffect.CreateInstance());
        case EffectType.Terrace:
            return new Node(TerraceEffect.CreateInstance());
        default:
            throw new ArgumentOutOfRangeException($"{effectType} not supported");
        }
    }
}
}