using System;
using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects;
using VoroSystem.GraphEditor.UserInterface.Elements;

namespace VoroSystem.GraphEditor.Output {
public class Graph : IGraph {
    Graph(string name, List<IEffect> effects) {
        Name = name;
        Items = effects ?? new List<IEffect>();
    }

    public string Name { get; }
    public List<IEffect> Items { get; private set; }

    public List<IEffect> Effects {
        get => Items;
        set => Items = value ?? new List<IEffect>();
    }

    public IEffect this[int index] => Items[index];

    public void ForEach(Action<IEffect> action) {
        Items.ForEach(action);
    }

    public static Graph CreateInstance(ILayer layer) {
        var effects = new List<IEffect>();
        layer.ForEach(node => { effects.Add(node.Effect); });
        return new Graph(layer.Name, effects);
    }
}
}