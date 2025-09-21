using System;

namespace VoroUI.EditorTabs.Nodes {
public class NodeInfo {
    public bool Active;
    public NodeElement Element;
    public string Name;

    public NodeInfo(string name) {
        Name = name;
        Active = false;

        Element = new NodeElement(this);
        Element.Clicked += state => {
            Active = state;
            if (state) {
                OnActive?.Invoke();
            }
        };
    }

    public event Action OnActive;
}
}