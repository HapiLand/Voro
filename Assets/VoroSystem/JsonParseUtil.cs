using System;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace VoroSystem {
public static class JsonParseUtil {
    /// <summary>
    ///     convert JArray to array of type T
    /// </summary>
    /// <param name="array"> </param>
    /// <param name="factory"> </param>
    /// <typeparam name="T"> </typeparam>
    /// <returns> </returns>
    public static T[] ParseArray<T>([CanBeNull] JArray array, Func<JToken, T> factory) {
        if (array == null || array.Count == 0) {
            Debug.LogError("Array is empty");
            return Array.Empty<T>();
        }

        var result = new T[array.Count];
        for (var i = 0; i < array.Count; i++) {
            result[i] = factory(array[i]);
        }

        return result;
    }

    /// <summary>
    ///     retrieves a property from the JToken, convert it to a value
    /// </summary>
    /// <param name="token"> </param>
    /// <param name="propertyName"> </param>
    /// <param name="defaultValue"> </param>
    /// <typeparam name="TValue"> </typeparam>
    /// <returns> </returns>
    public static TValue GetValue<TValue>(JToken token, string propertyName, TValue defaultValue = default!) {
        var propertyToken = token[propertyName];
        if (propertyToken == null) {
            Debug.LogError("Token is nul");
            return defaultValue;
        }

        return propertyToken.ToObject<TValue>() ?? defaultValue;
    }

    /*
     *         var cells = new Cell[pointsArray.Count];
       for (var i = 0; i < pointsArray.Count; i++) {
           var token = pointsArray[i];
           var pos = token["Pos"].ToObject<float[]>();
           var col = token["Col"].ToObject<float[]>();
           var id = token["Id"].ToObject<int>();

           cells[i] = new Cell(
               new Vector3(pos[0], 0, pos[1]),
               id,
               new Color(col[0], col[1], col[2], 1.0f)
           );
       }
     */

    /*
     * foreach (var token in configArray) {
           var layerName = token["Name"]?.ToObject<string>();
           var nodeArray = token["Nodes"] as JArray;
           var nodes = new List<Node>();

           var st = $"Parsed Layer: {layerName} ";
           if (nodeArray != null) {
               foreach (var nodeToken in nodeArray) {
                   var nodeName = nodeToken["Name"]?.ToObject<string>();
                   var controlsArray = nodeToken["Controls"] as JArray;
                   var controls = new List<Control>();

                   if (controlsArray != null) {
                       foreach (var controlToken in controlsArray) {
                           var controlName = controlToken["Name"]?.ToObject<string>();
                           var controlValue = controlToken["Value"]?.ToObject<float>() ?? 0f;
                           controls.Add(new Control(controlName, controlValue));
                       }
                   }

                   nodes.Add(new Node(nodeName, controls.ToArray()));
               }
           }

           st += $"has {nodes.Count} nodes";
           Debug.Log(st);

           layers.Add(new LayerData(layerName, nodes.ToArray()));
       }
     */
}
}