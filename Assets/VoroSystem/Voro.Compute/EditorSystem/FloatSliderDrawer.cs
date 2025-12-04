using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class FloatSliderDrawer : FieldDrawerBase, IFieldDrawer<float> {
  #region Serialized Fields

  [SerializeField] float min;
  [SerializeField] float max;

  #endregion

  public FloatSliderDrawer(float min, float max) {
    this.min = min;
    this.max = max;
  }

  #region IFieldDrawer<float> Members



  public VisualElement DrawUI(ref float v, string name) {
    throw new NotImplementedException();
  }

  #endregion
}
}