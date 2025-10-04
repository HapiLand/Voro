using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public class Chunk {
    /// <summary>
    ///     parsed data for the chunk
    /// </summary>
    readonly Cell[] _points;

    public Chunk() {
        Debug.Log("creating a new Chunk");
        var sw = new Stopwatch();
        sw.Start();

        AssetLoader.LoadTable(0, out var assetText); // load the text within the .json
        ParseCells(assetText, out _points); // parse the text to produce the cells

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to generate the Chunk");
        return;

        void ParseCells(string text, out Cell[] cells) {
            // parse the text in the .json file to produce the cells
            var jObject = JObject.Parse(text);
            var pointsArray = jObject["Points"] as JArray;
            if (pointsArray == null) {
                Debug.LogError("Chunk failed to parse text, returning empty");
                cells = Array.Empty<Cell>();
                return;
            }

            cells = new Cell[pointsArray.Count];
            for (var i = 0; i < pointsArray.Count; i++) {
                var token = pointsArray[i];

                var pos = token["Pos"].ToObject<float[]>(); // local position of cell
                var col = token["Col"].ToObject<float[]>(); // color of cell
                var id = token["Id"].ToObject<int>(); // mesh piece ID

                cells[i] = new Cell(
                    new Vector3(pos[0], 0, pos[1]),
                    id,
                    new Color(col[0], col[1], col[2], 1.0f)
                );
            }

            Debug.Log($"{cells.Length} Cells created in Chunk");
        }
    }

    public Cell[] Points => _points;

    public struct Cell {
        public Vector3 Position;
        public int ID;
        public Color Color;

        public Cell(Vector3 position, int id, Color color) {
            Position = position;
            ID = id;
            Color = color;
        }
    }
}
}