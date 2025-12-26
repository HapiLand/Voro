using UnityEngine;

namespace Voro.UserInterface.Internal.TerrainOperators.Editor {
public static class OpFactory {
  public static void CreateOperator(OpData data) {
    var op = ScriptableObject.CreateInstance<Operator>();
    op.title = data.Title;
    op.compute = data.Compute;
    OpAssetUtility.SaveAsset(op);
  }
}
}