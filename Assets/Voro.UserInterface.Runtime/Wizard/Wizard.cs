using System;
using UnityEngine;

namespace Voro.UserInterface.Runtime.Wizard {
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
}
}