using VoroUI.Effects;
using VoroWorld.Grids;

namespace VoroWorld {
public class VoroCompute {
    /// <summary>
    /// </summary>
    /// <param name="tile"></param>
    public void Compute(IEffect effect, ref WorldTile tile) {
        // compute the function of the effect
        effect.Compute(ref tile);
    }
}
}