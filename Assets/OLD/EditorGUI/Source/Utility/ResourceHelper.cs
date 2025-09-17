using UnityEngine;

namespace EditorGUI.Source.Utility {
public static class ResourceHelper {
    // public static Cell[] CreateCellArray() {
    //     // <id,float[]> point data from DemoTable.json
    //     var pointData = LoadVoroPoints();
    //     var jsonPointArray = JObject.Parse(pointData.text)["points"].ToObject<JsonPoint[]>();
    //
    //     // create the cell array
    //     var cells = new Cell[jsonPointArray.Length];
    //     for (var i = 0; i < cells.Length; i++) {
    //         var jsonPoint = jsonPointArray[i];
    //         cells[i] = CreateCell(jsonPoint);
    //     }
    //
    //     return cells;
    // }

    // static Cell CreateCell(JsonPoint jsonPoint) {
    //     return new Cell(jsonPoint);
    // }

    // static TextAsset LoadVoroPoints() {
    //     var pathTo = "Points/DemoTable";
    //     return LoadResource<TextAsset>(pathTo);
    // }

    public static T LoadResource<T>(string path) where T : Object {
        return Resources.Load<T>(path);
    }

    public static void LoadAndInstanceResource<T>(string path, out T instance) where T : Object {
        instance = Object.Instantiate(Resources.Load<T>(path));
    }
}
}