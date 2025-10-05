using System;
using VoroSystem.UserInterface.Interface;

namespace VoroSystem.UserInterface {
public class UserInterfaceMediator : IUserInterfaceMediator {
    readonly VoroEditor _editor;
    Window _window;

    public UserInterfaceMediator(VoroEditor editor) {
        _editor = editor;
        _editor.SetMediator(this);
    }

    public void Initialize(int preset) {
        throw new NotImplementedException();
    }

    public void OpenWindow() {
        throw new NotImplementedException();
    }

    public void MoveUp<T>(T item) where T : IOrderedItem {
        throw new NotImplementedException();
    }

    public void MoveDown<T>(T item) where T : IOrderedItem {
        throw new NotImplementedException();
    }

    public int GetIndex<T>(T item) where T : IOrderedItem {
        throw new NotImplementedException();
    }

    public void ForEach<T>(Action<T> action) where T : IOrderedItem {
        throw new NotImplementedException();
    }

    public T GetActiveItem<T>(Func<T, bool> action) where T : ISelectable {
        throw new NotImplementedException();
    }

    public void CreateItem<T>(T item) where T : IOrderedItem {
        throw new NotImplementedException();
    }

    public void RemoveItem<T>(T item) where T : IOrderedItem {
        throw new NotImplementedException();
    }

    /*

    public void Initialize(int preset) {
        _layerEditor.LoadPreset(preset);
    }

    public void MoveUp<T>(T item) where T : IOrderedItem {
        if (item is Layer layer) {
            var currentIndex = GetIndex(layer);
            if (currentIndex <= 0) {
                return;
            }

            _layerEditor.Layers.RemoveAt(currentIndex);
            var newIndex = currentIndex - 1;
            _layerEditor.Layers.Insert(newIndex, layer);
        }
        else if (item is Node node) {
            var parent = _layerEditor.GetParent(node);
            var currentIndex = GetIndex(node);
            if (currentIndex <= 0) {
                return;
            }

            parent.Content.RemoveAt(currentIndex);
            var newIndex = currentIndex - 1;
            parent.Content.Insert(newIndex, node);
        }
    }

    public int GetIndex<T>(T item) where T : IOrderedItem {
        switch (item) {
        case Layer layer:
            return _layerEditor.Layers.IndexOf(layer);
        case Node node: {
            var parent = _layerEditor.GetParent(node);
            return parent.GetNodeIndex(node);
        }
        }

        return 0;
    }

    public void OpenWindow() {
#if UNITY_EDITOR
        EditorApplication.delayCall += () => {
            _editorWindow = ScriptableObject.CreateInstance<Window>();
            _editorWindow.SetMediator(this);
            _editorWindow.Show();
        };
#endif
    }

    /// <summary>
    ///     creates and adds a new Layer to the Editor
    /// </summary>
    public void CreateLayer(string name) {
        var layer = new Layer(name);
        _layerEditor.AddLayer(layer);
    }

    public void RemoveLayer(Layer layer) {
        _layerEditor.Remove(layer);
    }

    /// <summary>
    ///     create a new Node inside the current active Layer
    /// </summary>
    public void CreateNode(EffectName name) {
        var activeLayer = GetActiveLayer(layer => layer.Active);
        var node = new Node(name.ToString());
        activeLayer.AddNode(node);
    }

    /// <summary>
    ///     remove a Node inside the current active Layer
    /// </summary>
    public void RemoveNode(Node node) {
        var activeLayer = GetActiveLayer(layer => layer.Active);
        activeLayer.Remove(node);
    }


    /// <summary>
    ///     gets every Layer in the Editor
    /// </summary>
    public void ForEachLayer(Action<Layer> action) {
        foreach (var layer in _layerEditor.GetLayers()) {
            action(layer);
        }
    }

    /// <summary>
    ///     returns the first Layer in the Editor that is set as Active
    /// </summary>
    public Layer GetActiveLayer(Func<Layer, bool> action) {
        foreach (var layer in _layerEditor.GetLayers()) {
            if (action(layer)) {
                Debug.Log($"found Active Layer {layer.Name}");
                return layer;
                ;
            }
        }

        return null;
    }

    /// <summary>
    ///     returns the first active Node in the Layer
    /// </summary>
    public Node GetActiveNode(Func<Node, bool> action, Layer layer) {
        foreach (var node in layer.GetNodes()) {
            if (action(node)) {
                Debug.Log($"found Active Node {node.Name} inside of {layer.Name}");
                return node;
            }
        }

        return null;
    }

    /// <summary>
    ///     gets every Node inside the Layer
    /// </summary>
    public void ForEachNode(Action<Node> action, Layer layer) {
        foreach (var node in layer.GetNodes()) {
            action(node);
        }
    }

    /// <summary>
    ///     get all controls inside this Node
    /// </summary>
    public void ForEachControl(Action<Control> action, Node node) {
        foreach (var control in node.GetControls()) {
            action(control);
        }
    }*/
}
}