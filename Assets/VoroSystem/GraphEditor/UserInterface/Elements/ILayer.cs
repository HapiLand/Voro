using System.Collections.Generic;
using VoroSystem.GraphEditor.Output;

namespace VoroSystem.GraphEditor.UserInterface.Elements {
public interface ILayer : IGUIElement, ISelectable, IContainerMutable<INode> {
    List<INode> Nodes { get; set; }
    IGraph ConvertToGraph { get; }
}
}