using System;
using UnityEngine;
using VoroSystem.UI.Editor.Attributes;
using VoroSystem.VoroDataStructures;

namespace VoroSystem.UI.Editor {
[Serializable]
public class AdjustIntegerEditorData {
  #region Serialized Fields
  // [Text("Default Group")] public string groupName; // todo read existing groups that have been set in other places
  [Text("Attribute Name")] public string attributeName;
  
  // [Toggle(true)] public bool adjustValue;
  
  // [Variant(typeof(OperationVariants), OperationVariants.Set)] public OperationVariants operation;
  // [Slider(-999, 999)] [SerializeField] public int constantValue;
  
  // [Toggle(false)] public bool postProcessMinimum;
  // [Toggle(false)] public bool postProcessMaximum;
  // [Slider(-999, 999)] [SerializeField] public int minimum;
  // [Slider(-999, 999)] public int maximum;
  // [Slider(-999, 999)] [SerializeField] public int defaultValue;
  #endregion
}
}