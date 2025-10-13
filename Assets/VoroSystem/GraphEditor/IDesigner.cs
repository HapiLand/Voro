using System;
using System.Collections.Generic;
using VoroSystem.GraphEditor.Output;
using VoroSystem.GraphEditor.UserInterface.Elements;

namespace VoroSystem.GraphEditor {
public interface IDesigner : IContainerMutable<ILayer> {
    List<ILayer> Layers { get; set; }
    List<IGraph> Graphs { get; }
    void ForEachGraph(Action<IGraph> action);
}
}