using UnityEngine;

namespace Voro {
public static class GameObjectUtility {
  public static T CreateWithComponent<T>(string name = null, Transform parent = null, Vector3? position = null)
    where T : Component {
    var go = new GameObject(name ?? typeof(T).Name);

    if (parent != null) {
      go.transform.SetParent(parent, false);
    }

    if (position.HasValue) {
      go.transform.position = position.Value;
    }

    return go.AddComponent<T>();
  }

  public static GameObject CreateEmpty(string name, Transform parent = null, Vector3? position = null) {
    var go = new GameObject(name);

    if (parent != null) {
      go.transform.SetParent(parent, false);
    }

    if (position.HasValue) {
      go.transform.position = position.Value;
    }

    return go;
  }
}
}