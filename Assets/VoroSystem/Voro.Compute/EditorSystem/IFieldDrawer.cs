using UnityEngine.UIElements;

namespace VoroSystem.Voro.Compute.EditorSystem {
public interface IFieldDrawer<T> {
  VisualElement DrawUI(ref T v, string name);
}
}