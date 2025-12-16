using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Core;

namespace VoroSystem.Voro.DataStructures.EffectDefinition.Core {
[Serializable]
public abstract class EffectData {
  #region Serialized Fields
  [SerializeReference] public List<ParameterData> parameters = new();
  [SerializeField] public EffectVariants effectType;
  [SerializeField] public OperationVariants operationType;
  #endregion
}
}