using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public class VoroEditor {
    static VoroEditor _instance;
    static readonly object padlock = new();

    /// <summary>
    ///     indicate if new content exists in the editor
    /// </summary>
    public bool Dirty;

    /// <summary>
    ///     parsed data from a preset.json that has the diagrams content
    /// </summary>
    public List<LayerData> LayerContent;

    public static VoroEditor Instance {
        get
        {
            if (_instance != null) {
                return _instance;
            }

            lock (padlock) {
                _instance ??= new VoroEditor();
            }

            return _instance;
        }
    }


    /// <summary>
    ///     loads a pre-made voro terrain configuration
    /// </summary>
    /// <param name="i">(1base-index) preset to read</param>
    public void LoadPreset(int i) {
        Debug.Log("load preset for the Editor to give it Layer content");
        var sw = new Stopwatch();
        sw.Start();

        // parse the text into layer,node value
        AssetLoader.ParsePreset(i, out var assetText); // parse json
        ParsePreset(assetText, out LayerContent); // parse the text to generate the elements

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to produce {LayerContent.Count} Layers");
        return;

        void ParsePreset(string text, out List<LayerData> layers) {
            var jObject = JObject.Parse(text);
            var configArray = jObject["Layers"] as JArray;
            if (configArray == null) {
                Debug.LogError("Editor failed to parse text, returning empty");
                layers = new List<LayerData>();
                return;
            }

            layers = new List<LayerData>();
            foreach (var token in configArray) {
                var layerName = token["Name"]?.ToObject<string>();
                var nodeArray = token["Nodes"] as JArray;
                var nodes = new List<LayerData.Node>();

                var st = $"Parsed Layer: {layerName} ";
                if (nodeArray != null) {
                    foreach (var nodeToken in nodeArray) {
                        var nodeName = nodeToken["Name"]?.ToObject<string>();
                        var controlsArray = nodeToken["Controls"] as JArray;
                        var controls = new List<LayerData.Node.Control>();

                        if (controlsArray != null) {
                            foreach (var controlToken in controlsArray) {
                                var controlName = controlToken["Name"]?.ToObject<string>();
                                var controlValue = controlToken["Value"]?.ToObject<float>() ?? 0f;
                                controls.Add(new LayerData.Node.Control(controlName, controlValue));
                            }
                        }

                        nodes.Add(new LayerData.Node(nodeName, controls.ToArray()));
                    }
                }

                st += $"has {nodes.Count} nodes";
                Debug.Log(st);

                layers.Add(new LayerData(layerName, nodes.ToArray()));
            }

            Debug.Log($"{layers.Count} Layers created in Editor");
        }
    }

    /// <summary>
    ///     displays the GUI
    /// </summary>
    public void ShowWindow() {
#if UNITY_EDITOR
        Debug.Log("showing Editor Window");
        EditorApplication.delayCall += () => {
            Window.ShowWindow();
            // todo values from VoroEditor must be displayed inside the EditorWindow
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
    /// <param name="result">the diagram that will computed</param>
    public void CreateDiagram(out Diagram result) {
        // create the diagram so the content is accessible to compute
        result = new Diagram(LayerContent);
    }
}
}