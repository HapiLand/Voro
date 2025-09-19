using System;
using EditorGUI.Source.Utility;
using EditorGUI.Source.Voro.TableData;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace EditorGUI.Source.Voro {
/// <summary>
///     the primary data object for the voro system
///     - has a point table to serve as the location for each cell (used to set GameObject positions)
///     - stores constant configuration, these define the base properties of the diagram
///     the constant configuration originates from the PointTable.json
/// </summary>
public class VoroDiagram {
    /// <summary>
    ///     CellPoints are stored locally to the Diagram, which exists in its own UV space
    ///     {0,1} {1,1}
    ///     {0,0} {1,0}
    ///     when used by a WorldTile, the actual world position of each CellPoint can be found
    /// </summary>
    public Point[] CellPoints;

    public Config Configuration;

    public VoroDiagram(int tableIndex = 0, Action<VoroDiagram> onLoaded = null) {
        LoadFromAddressable(tableIndex, onLoaded);
    }

    public bool IsLoaded { get; private set; }

    void LoadFromAddressable(int tableIndex, Action<VoroDiagram> onLoaded) {
        AssetHelper.LoadAssetPath<TextAsset>($"Assets/EditorGUI/Source/Voro/TableData/Table{tableIndex}.json",
            OnTableLoaded);

        void OnTableLoaded(TextAsset table) {
            if (table != null) {
                var tableData = JObject.Parse(table.text)["Points"].ToObject<TablePoint[]>();
                ConstructDiagram(tableData);

                IsLoaded = true;
                Debug.Log($"VoroDiagram created with {CellPoints.Length} points");
                onLoaded?.Invoke(this);
            }
            else {
                Debug.LogError($"VoroDiagram failed to load Table{tableIndex}.json");
            }
        }
    }

    void ConstructDiagram(TablePoint[] data) {
        CellPoints = new Point[data.Length];
        Configuration = new Config
        {
            PointColors = new Color[data.Length]
        };

        for (var i = 0; i < data.Length; i++) {
            var tablePoint = data[i];

            // convert the table into CellPoints
            var position = new Vector3(tablePoint.Pos[0], 0, tablePoint.Pos[1]);
            CellPoints[i] = new Point(position, tablePoint.Id);

            // write the configuration for the Diagram
            var color = tablePoint.Col;
            Configuration.PointColors[i] = new Color(color[0], color[1], color[2], 1.0f);
        }
    }


    public struct Config {
        public Color[] PointColors;
    }
}
}