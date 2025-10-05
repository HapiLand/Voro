using System;
using VoroSystem.UserInterface.Interface;

namespace VoroSystem.UserInterface {
public class Node : IContainer<Control>, IOrderedItem, ISelectable {
    public Node(string name) {
        Name = name;
    }

    public string Name { get; }
    public Control[] Container { get; set; }

    public void AddToContainer(Control data) {
        throw new NotImplementedException();
    }

    public void ClearContainer() {
        throw new NotImplementedException();
    }

    public void RemoveFromContainer(Control item) {
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
    public void SetContent(Control[] data) {
        Content = data;
    }
    public IEnumerable<Control> GetControls() {
        for (var i = 0; i < Content.Length; i++) {
            yield return Content[i];
        }
    }
    // }*/
}
}