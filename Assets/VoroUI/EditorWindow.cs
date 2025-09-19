using UnityEngine.UIElements;

namespace VoroUI {
public class EditorWindow : VisualElement {
    public void CreateGui() {
        // create components
        var layers = new LayersTab();
        var effects = new EffectsTab();
        var cam = new CameraTab();
        // left vertical layout
        var ve = new VisualElement();
        ve.Add(layers as VisualElement);
        ve.Add(effects as VisualElement);
        // full layout
        Add(ve);
        Add(cam as VisualElement);
    }
}
}