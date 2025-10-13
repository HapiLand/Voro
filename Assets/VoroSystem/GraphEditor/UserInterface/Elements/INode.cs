using VoroSystem.GraphEditor.Effects;

namespace VoroSystem.GraphEditor.UserInterface.Elements {
public interface INode : IGUIElement {
    IEffect Effect { get; }
}
}