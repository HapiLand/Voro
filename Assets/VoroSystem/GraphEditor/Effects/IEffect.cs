using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;
using VoroSystem.GraphEditor.UserInterface.Elements;

namespace VoroSystem.GraphEditor.Effects {
public interface IEffect : IGUIElement, IContainer<IBaseControl> {
    List<IBaseControl> Controls { get; }
}
}