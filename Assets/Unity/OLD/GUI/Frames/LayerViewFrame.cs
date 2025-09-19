using OLD.GUI.Elements;
using UnityEngine.UIElements;

namespace OLD.GUI.Frames {
[UxmlElement]
public partial class LayerViewFrame : TemplateFrame {
    public LayerViewFrame() {
        DisplayName = "Layer View";

        foreach (var s in new[]
                 {
                     "Slope",
                     "Terrace",
                     "Noise"
                 }) {
            AddEffectItem(s);
        }
    }

    void AddEffectItem(string displayName) {
        var effectItem = new EffectItemElement { DisplayName = displayName };
        // effectItem.clicked += () => Debug.Log($"LayerView.{displayName}");
        _body.Add(effectItem);
    }
}
}