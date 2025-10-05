using System.Collections.Generic;
using UnityEngine;
using VoroSystem.UserInterface;

namespace VoroSystem.Terrain {
public class Graph {
    public List<EffectBase> Effects;
    public string Name;

    public Graph(Layer layer) {
        Debug.Log("Creating new Graph instance");
        Name = layer.Name;

        Effects = new List<EffectBase>();
        foreach (var node in layer.GetItems()) {
            switch (node.Name) {
            case "SetElevation":
                // Effects.Add(new SetElevation());
                // Debug.Log("Added Effect.SetElevation to graph");
                break;
            }
        }
    }
}
}