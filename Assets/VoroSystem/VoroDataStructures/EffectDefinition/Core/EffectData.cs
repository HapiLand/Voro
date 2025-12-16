using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;

namespace VoroSystem.VoroDataStructures.EffectDefinition.Core {
[Serializable]
public abstract class EffectData {
  #region Serialized Fields
  [SerializeReference] public List<ParameterData> parameters = new();
  [SerializeField] public EffectVariants effectType;
  [SerializeField] public OperationVariants operationType;
  #endregion
}
}