using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects.Parameters.Controls;
using VoroSystem.GraphEditor.UserInterface.Elements;
using VoroSystem.Terrain.Generation.PostCompute;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.GraphEditor.Effects {
public interface IEffect : IGUIElement, IContainer<IBaseControl> {
    List<IBaseControl> Controls { get; }
    IResult Dispatch(ITile tile);
}
}