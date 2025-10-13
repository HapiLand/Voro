using System;
using System.Collections.Generic;
using UnityEditor;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;

namespace VoroSystem.GraphEditor.Effects {
public abstract class BaseEffect : IEffect {
    protected BaseEffect(string name, EffectType type, List<IBaseControl> controls) {
        Name = name;
        Type = type;
        Items = controls;
    }

    public EffectType Type { get; }
    public string Name { get; }

    public virtual void Draw() {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField($"Effect: {Name}");

        EditorGUI.indentLevel++;
        // draw each Control, which displays is Field
        ForEach(control => { control.Draw(); });
        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
    }

    public List<IBaseControl> Items { get; }

    public IBaseControl this[int index] => Items[index];

    public void ForEach(Action<IBaseControl> action) {
        Items.ForEach(action);
    }

    public List<IBaseControl> Controls => Items;
}
}