using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using UnityEngine;
using VoroSystem.GridSystem.Interface;
using Debug = UnityEngine.Debug;

namespace VoroSystem.GridSystem {
/// <summary>
///     collection of Cells loaded from a text asset
/// </summary>
public class Chunk : IChunk<Cell> {
    IGridSystemMediator _mediator;

    public Chunk(int id) {
        var sw = Stopwatch.StartNew();

        ID = id;
        var assetText = LoadAssetText(ID);
        var cells = ParseCells(assetText);
        SetContent(cells);

        sw.Stop();
        LogConstructionTime(sw.ElapsedMilliseconds);
        return;

        void LogConstructionTime(long elapsedMilliseconds) {
            Debug.Log($"Chunk {ID} constructed in {elapsedMilliseconds} ms");
        }
    }

    /// <summary>
    ///     parsed data for the chunk
    /// </summary>
    public Cell[] Content { get; private set; }

    /// <summary>
    ///     identifier for loading the text asset
    /// </summary>
    public int ID { get; }

    public IEnumerable<Cell> GetCells() {
        for (var i = 0; i < Content.Length; i++) {
            yield return Content[i];
        }
    }


    public void SetContent(Cell[] data) {
        Debug.Log($"{data.Length} Cells added to Chunk {ID}");
        Content = data;
    }

    public void SetMediator(IGridSystemMediator gridSystemMediator) {
        _mediator = gridSystemMediator;
    }

    string LoadAssetText(int assetId) {
        AssetLoader.LoadTable(assetId, out var assetText);
        return assetText;
    }

    Cell[] ParseCells(string text) {
        if (string.IsNullOrEmpty(text)) {
            Debug.LogError("Chunk failed to parse text: input is null or empty");
            return Array.Empty<Cell>();
        }

        var jObject = JObject.Parse(text);
        var pointsArray = jObject["Points"] as JArray;

        return JsonParseUtil.ParseArray(pointsArray, token => {
            var position = JsonParseUtil.GetValue(token, "Pos", Array.Empty<float>());
            var id = JsonParseUtil.GetValue(token, "Id", 0);
            var color = JsonParseUtil.GetValue(token, "Col", Array.Empty<float>());

            return new Cell(
                new Vector3(position[0], 0, position[1]),
                id,
                new Color(color[0], color[1], color[2], 1.0f)
            );
        });
    }
}
}