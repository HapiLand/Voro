using System;
using VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Core;

namespace VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Variants {
[Serializable]
public class BoolValue : ParameterValue {
  #region Serialized Fields
  public bool value;
  #endregion
}
}