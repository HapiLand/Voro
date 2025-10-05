using VoroSystem.Interface;

namespace VoroSystem {
public class Node : IElement<Control[]> {
    public Control[] Controls;

    public Node(string name, Control[] controls) {
        Name = name;
        SetContent(controls);
        Active = false;
    }


    public bool Active { get; set; }
    public string Name { get; }
    public int Index { get; private set; }
    public void SetIndex(int index) {
        throw new System.NotImplementedException();
    }
    public void MoveUp() {
        if (Index <= 0) {
            return;
        }
        Index -= 1;
        SetIndex(0);
        /*public void MoveUp(List<LayerData> layers, int index) {
            if (index <= 0) { return; }
            (layers[index - 1], layers[index]) = (layers[index], layers[index - 1]);
        }*/
    }
    public void MoveDown() {
        Index += 1;
        SetIndex(0);
        /*public void MoveDown(List<LayerData> layers, int index) {
            if (index >= layers.Count - 1) { return; }
            (layers[index + 1], layers[index]) = (layers[index], layers[index + 1]);
        }*/
    }
    
    public Control[] Content { get; set; }
    public void SetContent(Control[] data) {
        Controls = data;
    }
}
}