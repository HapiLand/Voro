using System.IO;
using DataTypes;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Internal {
public static class ResourceHelper {
    public static T LoadResource<T>(string path) where T : Object {
        return Resources.Load<T>(path);
    }

    public static Voro CreateVoro(Vector3 position, string configName = "MyConfig") {
        return new Voro(configName, position);
    }

    static TextAsset LoadVoroPoints() {
        var pathTo = "Points/DemoTable";
        return LoadResource<TextAsset>(pathTo);
    }

    static Cell CreateCell(JsonPoint jsonPoint) {
        return new Cell(jsonPoint);
    }

    public static Cell[] CreateCellArray() {
        // <id,float[]> point data from DemoTable.json
        var pointData = LoadVoroPoints();
        var jsonPointArray = JObject.Parse(pointData.text)["points"].ToObject<JsonPoint[]>();

        // create the cell array
        var cells = new Cell[jsonPointArray.Length];
        for (var i = 0; i < cells.Length; i++) {
            var jsonPoint = jsonPointArray[i];
            cells[i] = CreateCell(jsonPoint);
        }

        return cells;
    }

    public static VisualTreeAsset LoadEffectUXML(string name) {
        return LoadResource<VisualTreeAsset>(name);
    }

    public static void InstanceGeometry<T>(GameObject geo, out T instance) where T : Object {
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