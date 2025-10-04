using System.Collections.Generic;
using System.Linq;

namespace VoroSystem {
public class LayerData {
    public bool Active;
    public string Name;
    public List<Node> Nodes;

    public LayerData(string name, Node[] nodes) {
        Name = name;
        Nodes = nodes.ToList();
        Active = false;
    }

    public class Node {
        public bool Active;
        public Control[] Controls;
        public string Name;

        public Node(string name, Control[] controls) {
            Name = name;
            Controls = controls;
            Active = false;
        }

        public class Control {
            public string Name;
            public float Value;

            public Control(string name, float value) {
                Name = name;
                Value = value;
            }
        }
    }
}
}