using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class IntSliderDrawer : FieldDrawerBase, IFieldDrawer<int> {
  #region Serialized Fields

  [SerializeField] int min;
  [SerializeField] int max;

  #endregion

  public IntSliderDrawer(int min, int max) {
    this.min = min;
    this.max = max;
  }

  #region IFieldDrawer<int> Members



  public VisualElement DrawUI(ref int v, string name) {
    throw new NotImplementedException();
  }

  #endregion
}
}