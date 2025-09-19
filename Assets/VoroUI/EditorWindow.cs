using UnityEngine.UIElements;

namespace VoroUI {
public class EditorWindow : VisualElement {
    public void CreateGui() {
        // create components
        var layers = new LayersTab();
        var effects = new EffectsTab(layers);
        var cam = new CameraTab();
        // left vertical layout
        var ve = new VisualElement();
        ve.Add(layers);
        ve.Add(effects);
        // full layout
        Add(ve);
        Add(cam);
    }
}

public class CameraTab : VisualElement { }
}