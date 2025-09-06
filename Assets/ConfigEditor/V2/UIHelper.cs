using Internal;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
public static class UIHelper {
    public static VisualElement Create(string name, string className) {
        var ve = new VisualElement { name = name };
        ve.AddToClassList(className);
        return ve;
    }

    public static StyleSheet LoadStyleSheet(string path) {
        return ResourceHelper.LoadResource<StyleSheet>(path);
    }
}
}