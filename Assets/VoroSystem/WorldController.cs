using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
[ExecuteAlways]
public class WorldController : MonoBehaviour {
    [SerializeField] List<LayerNodePair> _contents = new();
    public List<LayerNodePair> Contents => _contents;

    public void GenerateWorldMap() {
        Debug.Log("generating world map");
        var sw = new Stopwatch();
        sw.Start();

        // set up the world - copy Chunk points to the TileMap
        var tileMap = new TileMap();
        tileMap.SetSize(5, 5); // fixed map size, produce Tile[,]
        tileMap.UpdateVisibility(); // update visibility

        var chunk = new Chunk();
        AssetLoader.BeginLoadingAssets(chunk); // routine to load asset library
        tileMap.Blit(chunk); // copy multi chunks to each visible tile position

        if (true) {
            // basic debug to verify the chunks exist correctly
            // select each Tile and its position
            foreach (var point in tileMap.AsPoints()) {
                var height = 1f;
                Debug.DrawLine(point, point + Vector3.up * height, Color.cyan, 2);
            }
        }

        sw.Stop();
        Debug.Log($"world map took {sw.ElapsedMilliseconds}ms to generate");
    }

    public void LaunchEditor() {
        Debug.Log("launching editor");
        var sw = new Stopwatch();
        sw.Start();

        // set up the editor - load the initial preset template
        var editor = new VoroEditor();
        editor.ShowWindow(); // open the editor window
        editor.RunDemoCamera(0.2f); // auto fly around the world, 20% speed
        // loading a preset populates the editor with its layer+node content
        editor.LoadPreset(1); // default preset no.1

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to launch the editor");
    }
}

public class VoroEditor {
    public bool clean; // true when editor is up to date
    List<LayerNodePair> Contents; // the diagram content

    /// <summary>
    ///     loads a pre-made voro terrain configuration
    /// </summary>
    /// <param name="i">(1base-index) preset to read</param>
    public void LoadPreset(int i) {
        // parse the text into layer,node value
        AssetLoader.ParsePreset(i, out var assetText); // parse json
        ParsePreset(assetText, out Contents); // parse the text to generate the elements
        return;

        void ParsePreset(string text, out List<LayerNodePair> pairs) {
            // parse the text in the .json file to produce the content
            var jObject = JObject.Parse(text);
            var configArray = jObject["Config"] as JArray;
            if (configArray == null) {
                Debug.LogError("Editor failed to parse text, returning empty");
                pairs = new List<LayerNodePair>();
                return;
            }


            pairs = new List<LayerNodePair>();
            for (var i = 0; i < configArray.Count; i++) {
                var token = configArray[i];
                // extract the Layer and Layer.Node within the text to produce the instances
                var pair = token["Pair"].ToObject<object[]>(); // each LayerNode
                pairs.Add(new LayerNodePair());
            }

            Debug.Log($"{pairs.Count} Layers created in Editor");
        }
    }

    /// <summary>
    ///     displays the GUI
    /// </summary>
    public void ShowWindow() {
#if UNITY_EDITOR
        EditorApplication.delayCall += () => {
            // todo draw the UI for the editor
        };
#endif
    }

    /// <summary>
    ///     auto free cam to fly around the world
    /// </summary>
    /// <param name="speed">camera velocity</param>
    public void RunDemoCamera(float speed) {
        // todo move the camera around to random locations above the terrain
    }

    /// <summary>
    ///     build a diagram from the editor contents
    /// </summary>
    /// <param name="result">input to compute</param>
    public void CreateDiagram(out object result) {
        // Layer -> Graph
        // Node  -> Effect
        // create the dictionary to hold the editor content
        // var effectGraph = new Dictionary<Graph, List<Effect>>();
        // effectGraph.Build(Contents); // convert layer node to the dictionary

        // create the diagram so graph is accessible to compute
        // result = new Diagram(effectGraph);
        result = null;
    }
}
}