using System;
using UnityEditor;
using UnityEngine;

namespace Voro.Wizard {
public class Wizard : ScriptableObject {
  #region Serialized Fields
  public string title;
  public Section[] sections;
  #endregion

  #region Event Functions
  void OnEnable() {
    AssetDatabase.Refresh();
  }
  #endregion

  [Serializable]
  public class Section {
    #region Serialized Fields
    public string heading;
    public string text;
    #endregion
  }
}
}