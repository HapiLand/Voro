using System;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.VoroEditor.GUI {
public class LayoutBuilder {
    readonly VisualElement _root;

    public LayoutBuilder(VisualElement root) {
        _root = root;
    }

    /// <summary>
    ///     build the main editor layout
    /// </summary>
    public void BuildMainLayout(Action onLayoutBuilt = null) {
        var path = "Assets/VoroEditor/GUI/Panels/Main.uxml";
        AssetHelper.LoadAssetPath<VisualTreeAsset>(path, OnLoaded);
        return;

        void OnLoaded(VisualTreeAsset vta) {
            if (vta != null) {
                // instance the UXML
                var templateContainer = vta.Instantiate();
                templateContainer.style.flexGrow = 1; // required as default is set to 0
                templateContainer.name = "Main";

                _root.Add(templateContainer); // add to window
                onLayoutBuilt?.Invoke(); // notify when the layout was built
            }
        }
    }
}
}