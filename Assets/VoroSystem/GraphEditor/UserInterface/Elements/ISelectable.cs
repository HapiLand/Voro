using System;

namespace VoroSystem.GraphEditor.UserInterface.Elements {
public interface ISelectable {
    bool Selected { get; set; }
    void Select();
    void Deselect();
    event Action<ISelectable, bool> OnSelectionValueChange;
}
}