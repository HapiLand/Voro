using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;
using VoroSystem.UserInterface.Interface;
using Debug = UnityEngine.Debug;

namespace VoroSystem.UserInterface {
public class VoroEditorBase {
    IUserInterfaceMediator _mediator;

    public void SetMediator(IUserInterfaceMediator mediator) {
        _mediator = mediator;
    }
}

public class VoroEditor : VoroEditorBase {
    public List<Layer> Layers { get; private set; }

    /// <summary>
    ///     identifier for loading the text asset
    /// </summary>
    public int ID { get; private set; }

    public void Remove(Layer layer) {
        Layers.Remove(layer);
    }


    public void LoadPreset(int preset) {
        var sw = Stopwatch.StartNew();

        ID = preset;
        var assetText = LoadAssetText(ID);
        var layers = ParseLayers(assetText);
        SetLayers(layers);

        sw.Stop();
        LogConstructionTime(sw.ElapsedMilliseconds);
        return;

        void LogConstructionTime(long elapsedMilliseconds) {
            Debug.Log($"VoroEditor {ID} constructed in {elapsedMilliseconds} ms");
        }
    }

    public IEnumerable<Layer> GetLayers() {
        for (var i = 0; i < Layers.Count; i++) {
            yield return Layers[i];
        }
    }

    public void SetLayers(Layer[] data) {
        Debug.Log($"{data.Length} Layers added to Editor");
        Layers = data.ToList();
        for (var i = 0; i < data.Length; i++) {
            AddLayer(data[i]);
        }
    }

    public void AddLayer(Layer layer) {
        Debug.Log($"Adding layer {layer.Name} to Editor");
        Layers.Add(layer);
    }

    public Layer GetParent(Node node) {
        foreach (var layer in GetLayers()) {
            if (layer.Contains(node)) {
                return layer;
            }
        }

        return null;
    }


    string LoadAssetText(int assetId) {
        AssetLoader.LoadEditorPreset(assetId, out var assetText);
        return assetText;
    }

    Layer[] ParseLayers(string text) {
        if (string.IsNullOrEmpty(text)) {
            Debug.LogError("Editor failed to parse text: input is null or empty");
            return Array.Empty<Layer>();
        }

        var jObject = JObject.Parse(text);
        // parse each Layer from the text
        var layersArray = jObject["Layers"] as JArray;

        return JsonParseUtil.ParseArray(layersArray, token => {
            var name = JsonParseUtil.GetValue(token, "Name", "");
            var nodes = JsonParseUtil.GetValue(token, "Nodes", Array.Empty<Node>());

            var layer = new Layer(name);
            layer.SetContent(nodes.ToList());
            return layer;
        });
    }

    /// <summary>
    ///     convert the Layer Content to a Diagram to be computed
    /// </summary>
    /// <param name="result">the diagram that will computed</param>
    public void CreateDiagram(out Diagram result) {
        throw new NotImplementedException();
    }
}
}