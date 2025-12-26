namespace Voro.UserInterface.Internal.TerrainComputes.Editor {
public class ComputeData {
  public string InputKernel { get; set; }

  /// <summary> kernel ID for shader </summary>
  public string Kernel { get; private set; }


  public void ApplyInputs() {
    Kernel = InputKernel;
  }
}
}