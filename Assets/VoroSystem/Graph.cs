using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem {
public class Graph {
    public List<EffectBase> Effects;
    public string Name;

    public Graph(LayerData layer) {
        Debug.Log("Creating new Graph instance");
        Name = layer.Name;

        Effects = new List<EffectBase>();
        foreach (var node in layer.Content) {
            switch (node.Name) {
            case "SetElevation":
                Effects.Add(new SetElevation(node.Controls));
                Debug.Log("Added Effect.SetElevation to graph");
                break;
            }
        }
    }
}
}