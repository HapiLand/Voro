using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Voro.Internal;
using Voro.Internal.Terrain.Algorithms;
using Voro.Internal.Terrain.Attributes;
using Voro.Internal.Terrain.Effects;

namespace Voro.Wizard {
public class Wizard : ScriptableObject {
  #region Serialized Fields
  public string title;
  public Section[] sections;
  #endregion

  [Serializable]
  public class Section {
    #region Serialized Fields
    public string heading;
    public string text;
    #endregion
  }

  void OnEnable() {
    AssetDatabase.Refresh();
  }
}
}