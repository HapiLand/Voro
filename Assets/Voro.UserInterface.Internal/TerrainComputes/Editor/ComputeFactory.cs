using UnityEngine;
using Voro.UserInterface.Internal.TerrainOperators;

namespace Voro.UserInterface.Internal.TerrainComputes.Editor {
public static class ComputeFactory {
  public static void CreateCompute(ComputeData data) {
    var compute = ScriptableObject.CreateInstance<Compute>();
    compute.kernel = data.Kernel;
    ComputeAssetUtility.SaveAsset(compute);
  }
}
}