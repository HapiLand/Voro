using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.Source;

namespace VoroEditor.GUI.Elements {
[UxmlElement]
public partial class Node : VisualElement {
    static Node _activeNode;
    Button _deleteButton;
    Label _label;
    Button _moveDownButton;
    Button _moveUpButton;
    Toggle _toggle;

    public Node() {
        // ToDo construct Node with an effect instance so it can execute the effect
        var path = "Assets/VoroEditor/GUI/Elements/Node.uxml";
        AssetHelper.LoadAssetPath<VisualTreeAsset>(path, OnLoaded);
        return;

        void OnLoaded(VisualTreeAsset vta) {
            if (vta != null) {
                // instance the UXML
                var templateContainer = vta.Instantiate();
                templateContainer.style.flexGrow = 1; // required as default is set to 0
                templateContainer.name = "NewNode";

                Add(templateContainer);
                _label = this.Q<Label>("Label");
                _toggle = this.Q<Toggle>("Toggle");
                _toggle.RegisterValueChangedCallback(OnToggleValueChanged);

                _deleteButton = this.Q<Button>("Delete");
                _deleteButton.clicked += OnClick_Delete;

                _moveUpButton = this.Q<Button>("MoveUp");
                _moveUpButton.clicked += () => OnClick_Move(-1);

                _moveDownButton = this.Q<Button>("MoveDown");
                _moveDownButton.clicked += () => OnClick_Move(1);
            }
        }
    }

    #region UXML Attributes

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    #endregion

    void OnToggleValueChanged(ChangeEvent<bool> evt) { }

    void Select() { }

    void OnClick_Delete() {
        RemoveFromHierarchy();
    }

    void OnClick_Move(int direction) {
        Debug.Log($"dir {direction}");
        // ToDo move node
    }
}
}