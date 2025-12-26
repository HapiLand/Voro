using Voro.UserInterface.Internal.TerrainComputes;

namespace Voro.UserInterface.Internal.TerrainOperators.Editor {
public class OpData {
  public string InputTitle { get; set; }
  public Compute InputCompute { get; set; }

  /// <summary> title name of the operator </summary>
  public string Title { get; private set; }
  /// <summary> compute shader </summary>
  public Compute Compute { get; private set; }

  public void ApplyInputs() {
    Title = InputTitle;
    Compute = InputCompute;
  }
}
}