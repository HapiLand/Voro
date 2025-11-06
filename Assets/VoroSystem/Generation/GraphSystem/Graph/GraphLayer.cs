using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VoroSystem.Generation.GraphSystem.Graph {
[Serializable]
public class GraphLayer {
    public GraphLayer(string name, int sortOrder = 0) {
        Name = name;
        SortOrder = sortOrder;
    }

    public string Name { get; }
    public int SortOrder { get; set; }
    public List<LayerEffect> Effects { get; set; } = new();

    public void CreateEffect(LayerEffect effect) {
        Effects.Add(effect);
    }

    public void DrawGUI() {
        GUILayout.BeginHorizontal();
        SortOrder = int.Parse(GUILayout.TextField(SortOrder.ToString()));
        GUILayout.EndHorizontal();
    }
    
    /// <summary>
    /// detect if anything in the layer changes
    /// </summary>
    public int ComputeHash() {
        var hash = Name.GetHashCode() ^ SortOrder;
        return Effects.Aggregate(hash, (current, effect) => current * 31 + effect.ComputeHash());
    }
}
}