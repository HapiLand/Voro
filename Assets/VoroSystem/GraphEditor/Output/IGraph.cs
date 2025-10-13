using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects;
using VoroSystem.GraphEditor.UserInterface.Elements;

namespace VoroSystem.GraphEditor.Output {
public interface IGraph : IItem, IContainer<IEffect> {
    List<IEffect> Effects { get; set; }
}
}