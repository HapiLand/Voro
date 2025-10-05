using System;
using VoroSystem.UserInterface.Interface;

namespace VoroSystem.UserInterface {
public class Layer : IContainer<Node>, IOrderedItem, ISelectable {
    public Layer(string name) {
        Name = name;
    }

    public string Name { get; }

    public Node[] Container { get; set; }

    public void AddToContainer(Node data) {
        throw new NotImplementedException();
    }

    public void ClearContainer() {
        throw new NotImplementedException();
    }

    public void RemoveFromContainer(Node item) {
        throw new NotImplementedException();
    }

    public int Index { get; }

    public void SetIndex(int index) {
        throw new NotImplementedException();
    }

    public bool IsSelected { get; }

    public void Select() {
        throw new NotImplementedException();
    }

    public void Deselect() {
        throw new NotImplementedException();
    }

    /*

    public bool Contains(Node node) {
        foreach (var item in GetNodes()) {
            if (item == node) {
                return true;
            }
        }

        return false;
    }

    public IEnumerable<Node> GetNodes() {
        for (var i = 0; i < Content.Count; i++) {
            yield return Content[i];
        }
    }

    public void Remove(Node node) {
        Content.Remove(node);
    }

    public void AddNode(Node node) {
        Content.Add(node);
    }

    public int GetNodeIndex(Node node) {
        return Content.IndexOf(node);
    }

    public bool IsSelected { get; }
    }*/
}
}