using UnityEngine;

namespace Internal {
public static class ResourceHelper {
    public static T LoadResource<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
}
}