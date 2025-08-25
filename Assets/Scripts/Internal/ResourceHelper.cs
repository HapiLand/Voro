using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace Internal {
public static class ResourceHelper {
    public static T LoadResource<T>(string path) where T : Object {
        return Resources.Load<T>(path);
    }

    public static TextAsset LoadVoroPoints() {
        var pathTo = "Points/DemoTable";
        return LoadResource<TextAsset>(pathTo);
    }

    public static VisualTreeAsset LoadEffectUXML(string name) {
        return LoadResource<VisualTreeAsset>(name);
    }

    public static void InstanceGeometry<T>(GameObject geo, out T instance) where T : UnityEngine.Object
    {
        instance = Object.Instantiate(geo) as T;
    }
    
    public static string LoadVoroConfig(string configName = "MyConfig") {
        var fileName = configName + ".json";
        var path = Path.Combine(Application.persistentDataPath, fileName);
        return File.ReadAllText(path);

        //var pathTo = $"Configs/{configName}";
        //return LoadResource<TextAsset>(pathTo).text;
    }
    // until this is fixed, it is not possible to use the below methods
    /*static string GetVoroConfigPath(string configName) {
        var fileName = configName + ".json";
        var path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }
    public static string LoadVoroConfigString(string configName) {
        return File.ReadAllText(GetVoroConfigPath(configName));
    }*/
}
}