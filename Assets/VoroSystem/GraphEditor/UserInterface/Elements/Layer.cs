using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VoroSystem.GraphEditor.Effects;
using VoroSystem.GraphEditor.Output;

namespace VoroSystem.GraphEditor.UserInterface.Elements {
public class Layer : ILayer {
    EffectType _newEffectType = EffectType.Constant;

    Layer(string name) {
        Name = name;
        Items = new List<INode>();
    }

    public string Name { get; }

    public void Draw() {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField($"Layer: {Name}");

        EditorGUI.indentLevel++;
        // draw each Node, which draws its Effect
        ForEach(node => {
            EditorGUILayout.BeginHorizontal();
            node.Draw();
            if (GUILayout.Button("↑")) {
                MoveUp(node);
            }

            if (GUILayout.Button("↓")) {
                MoveDown(node);
            }

            if (GUILayout.Button("🗑")) {
                Remove(node);
            }

            EditorGUILayout.EndHorizontal();
        });
        EditorGUI.indentLevel--;

        // create new node
        _newEffectType = (EffectType)EditorGUILayout.EnumPopup("Effect Type", _newEffectType);
        if (GUILayout.Button("Create Effect")) {
            Add(Node.CreateInstance(_newEffectType));
        }

        EditorGUILayout.EndVertical();
    }

    public List<INode> Items { get; private set; }

    public List<INode> Nodes {
        get => Items;
        set => Items = value ?? new List<INode>();
    }

    public IGraph ConvertToGraph => Graph.CreateInstance(this);

    public INode this[int index] => Items[index];

    public void Add(INode item) {
        Items.Add(item);
    }

    public void Remove(INode item) {
        Items.Remove(item);
    }

    public void MoveUp(INode item) {
        var index = Items.IndexOf(item);
        if (index > 0) {
            (Items[index - 1], Items[index]) = (Items[index], Items[index - 1]);
        }
    }

    public void MoveDown(INode item) {
        var index = Items.IndexOf(item);
        if (index >= 0 && index < Items.Count - 1) {
            (Items[index + 1], Items[index]) = (Items[index], Items[index + 1]);
        }
    }

    public void ForEach(Action<INode> action) {
        Items.ForEach(action);
    }

    public bool Selected { get; set; }

    public void Select() {
        OnSelectionValueChange?.Invoke(this, true);
        throw new NotImplementedException();
    }

    public void Deselect() {
        OnSelectionValueChange?.Invoke(this, false);
        throw new NotImplementedException();
    }

    public event Action<ISelectable, bool> OnSelectionValueChange;

    public static Layer CreateInstance(string name) {
        return new Layer(name);
    }
}
}