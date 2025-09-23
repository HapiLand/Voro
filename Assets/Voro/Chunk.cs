using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Voro {
/// <summary>
///     - Base blueprint for terrain generation.
///     - Parses the point table and configuration that define the base terrain form.
///     - User-set effects are applied onto the point data in the chunk.
/// </summary>
public class Chunk {
    Configuration _config;
    public ChunkPoint[] _points;

    public Chunk() {
        var asset = Resources.Load<TextAsset>("Table0");
        if (asset) {
            var table = JObject.Parse(asset.text)["Points"].ToObject<TablePoint[]>();
            CreateConfiguration(table);
            CreatePoints(table);
        }

        return;

        void CreateConfiguration(TablePoint[] table) {
            _config = new Configuration();
        }

        void CreatePoints(TablePoint[] table) {
            _points = new ChunkPoint[table.Length];

            for (var i = 0; i < table.Length; i++) {
                var tablePoint = table[i];

                // create ChunkPoint and set properties
                var point = new ChunkPoint
                {
                    // local position
                    Position = new Vector3(tablePoint.Pos[0], 0, tablePoint.Pos[1]),
                    // mesh asset piece
                    ID = tablePoint.Id,
                    // point color
                    Color = new Color(tablePoint.Col[0], tablePoint.Col[1], tablePoint.Col[2], 1.0f)
                };

                _points[i] = point;
            }
        }
    }

    struct Configuration { }

    struct TablePoint {
        public float[] Col;
        public int Id;
        public float[] Pos;
    }

    public struct ChunkPoint {
        public Vector3 Position;
        public int ID;
        public Color Color;
    }
}
}