using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.GraphEditor.Output;
using VoroSystem.GraphEditor.UserInterface.Elements;

namespace VoroSystem.GraphEditor {
[ExecuteAlways]
public class GraphDesigner : MonoBehaviour, IDesigner {
    void Awake() {
        Items = new List<ILayer>();
    }

    public List<ILayer> Items { get; private set; }

    public List<ILayer> Layers {
        get => Items;
        set => Items = value ?? new List<ILayer>();
    }

    /// <summary>
    ///     gets the list of Layers and converts them into Graphs
    /// </summary>
    public List<IGraph> Graphs {
        get
        {
            var graphs = new List<IGraph>();
            ForEach(layer => { graphs.Add(layer.ConvertToGraph); });
            return graphs;
        }
    }

    public ILayer this[int index] => Items[index];

    public void Add(ILayer item) {
        Items.Add(item);
    }

    public void Remove(ILayer item) {
        Items.Remove(item);
    }

    public void MoveUp(ILayer item) {
        var index = Items.IndexOf(item);
        if (index > 0) {
            (Items[index - 1], Items[index]) = (Items[index], Items[index - 1]);
        }
    }

    public void MoveDown(ILayer item) {
        var index = Items.IndexOf(item);
        if (index >= 0 && index < Items.Count - 1) {
            (Items[index + 1], Items[index]) = (Items[index], Items[index + 1]);
        }
    }

    public void ForEach(Action<ILayer> action) {
        Items.ForEach(action);
    }

    public void ForEachGraph(Action<IGraph> action) {
        Graphs.ForEach(action);
    }
}
}