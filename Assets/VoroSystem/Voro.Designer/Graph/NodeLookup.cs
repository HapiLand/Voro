using System.Collections.Generic;
using VoroSystem.Voro.Designer.Graph.Core;
using VoroSystem.Voro.Designer.Graph.Fields;

namespace VoroSystem.Voro.Designer.Graph {
public static class NodeLookup {
  static readonly Dictionary<string, Node> Effects = new()
  {
    ["Slope"] = new Node("Slope")
    {
      fields = new List<FieldBase>
      {
        new Radial("Direction", 0f),
        new FloatField("Steepness", 0.1f),
        new Toggle("Reverse", false)
      }
    },
    ["Noise"] = new Node("Noise")
    {
      fields = new List<FieldBase>
      {
        new FloatSlider("Size", 0.75f, 0.2f, 0.75f),
        new FloatSlider("Steepness", 0.5f, 0.15f, 1f)
      }
    },
    ["Flat"] = new Node("Flat")
    {
      fields = new List<FieldBase>
      {
        new FloatSlider("Height", 0f, 0f, 1f)
      }
    },
    ["Terrace"] = new Node("Terrace")
    {
      fields = new List<FieldBase>
      {
        new FloatSlider("min_step_size", 0.15f, 0f, 1f),
        new FloatSlider("max_step_size", 0.75f, 0f, 1f),
        new FloatSlider("StepSize", 1f, 0f, 1f),
        new Radial("direction", 0f),
        new IntSlider("iterations", 3, 0, 10)
      }
    }
  };

  public static IEnumerable<string> Names => Effects.Keys;

  public static Node Get(string name) {
    return Effects.GetValueOrDefault(name);
  }
}
}