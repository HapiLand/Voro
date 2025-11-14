using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Generation.DiagramSystem;
using VoroSystem.Generation.GraphSystem.Fields;

namespace VoroSystem.Generation.GraphSystem.Graph {
public class LayerEffect {
    public LayerEffect(string name, EffectOperation operation = EffectOperation.Add) {
        this.Name = name;
        Operation = operation;

        if (name == "Flat") {
            Operation = EffectOperation.Set;
        }
    }

    public string Name { get; }

    public EffectOperation Operation { get; set; }

    public List<EffectFieldBase> Fields { get; set; } = new();

    public void DrawGUI() {
        GUILayout.BeginHorizontal();
        Operation = (EffectOperation)EditorGUILayout.EnumPopup("Operation:", Operation);
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// detect if anything in the effect changes
    /// </summary>
    public int ComputeHash() {
        var hash = Name.GetHashCode() ^ Operation.GetHashCode();
        return Fields.Aggregate(hash, (current, field) => current * 31 + field.ComputeHash());
    }
}
}