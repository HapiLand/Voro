using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Voro.GraphEditor.Data {
public class GraphScriptableObject : ScriptableObject {
  #region Event Functions

  void OnEnable() {
    Name = "Example Name";
    Foo = new List<LayerData>();
  }

  #endregion

  public string Name;
  public List<LayerData> Foo;
}
}