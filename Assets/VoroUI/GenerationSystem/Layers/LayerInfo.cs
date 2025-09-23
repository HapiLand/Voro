using System;

namespace VoroUI.EditorTabs.Layers {
public class LayerInfo {
    public bool Active;
    public LayerElement Element;
    public string Name;

    public LayerInfo(string name) {
        Name = name;
        Active = false;

        Element = new LayerElement(this);
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