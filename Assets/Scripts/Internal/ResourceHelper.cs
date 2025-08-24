using System.IO;
using UnityEngine;

namespace Internal {
public static class ResourceHelper {
    public static T LoadResource<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public static TextAsset LoadVoroPoints() {
        var pathTo = "Points/DemoTable";
        return LoadResource<TextAsset>(pathTo);
    }

    // ToDo correctly work with Application.persistentDataPath
    // the problem is that VoroDemo loads MyConfig.json at this application path
    // if the user clones this project, does not run the Editor first to generate that json
    // then the Voro is unable to have its height set
    // the temporary measure is for me to manually copy the DataPath file into this projects Resources directory
    public static string LoadVoroConfig(string configName = "MyConfig") {
        var pathTo = $"Configs/{configName}";
        return LoadResource<TextAsset>(pathTo).text;
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