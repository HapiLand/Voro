using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.VoroEditor.Elements {
[UxmlElement]
public partial class Display : VisualElement {
    readonly VisualElement _contents;

    public Display() {
        // instantiate the uxml
        var vt = UIHelper.LoadUxml("Display");
        vt.CloneTree(this);

        // query each element
        _contents = this.Q<VisualElement>("Contents");
    }

    public void AddToDisplay(VisualElement element) {
        _contents.Add(element);
    }
}
}