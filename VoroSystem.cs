using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VoroWorld;

public class VoroSystem {
    void Main() {
        #region World Map

        // set up the world - copy chunk points to the tilemap
        var tileMap = new TileMap();
        tileMap.SetSize(100, 100); // lock map size, produce Tile[,]
        tileMap.LocateCamera(); // update visibility
        
        var chunk = new Chunk();
        AssetLoader.BeginLoadingAssets(chunk); // routine to load asset library
        tileMap.Blit(chunk); // copy multi chunks to each visible tile position

        #endregion

        #region Editor Interface

        // set up the editor - load the initial preset template
        var editor = new VoroEditor();
        editor.ShowWindow(); // open the editor window
        editor.RunDemoCamera(0.2f); // auto fly around the world, 20% speed
        // loading a preset populates the editor with its layer+node content
        editor.LoadPreset(1); // default preset no.1

        #endregion

        #region Lifecycle

        // compute the default terrain
        var compute = new VoroCompute();

        var editorDirty = !editor.clean; // have any new layers/nodes been created
        while (editorDirty) {
            #region Editor

            editor.CreateDiagram(out var dg); // turn the layers into the compute type
            
            #endregion

            #region Compute

            compute.ComputeDiagramMap(tileMap, dg, out var result); // dispatch

            #endregion

            #region Mesh

            // the compute data is used to build mesh data
            MeshBuilder.BuildVertices(result, out var vtxInfo); // translate result to vertices then
            MeshBuilder.BuildMesh(vtxInfo, out var meshData);   // translate to mesh data
            
            #endregion

            #region Scene

            // the mesh data is used to create GameObjects
            WorldBuilder.GenerateWorldMap(meshData); // instances the geometry where it should be

            #endregion
        }

        #endregion
    }

    class VoroEditor {
        public bool clean; // true when editor is up to date
        List<LayerNodePair> Contents = new(); // the diagram content
        /// <summary>
        /// loads a pre-made voro terrain configuration
        /// </summary>
        /// <param name="i">(1base-index) preset to read</param>
        public void LoadPreset(int i) {
            // parse the text into layer,node value
            AssetLoader.ParsePreset(i, out var text); // parse json
            Contents.Extract(text); // generate the elements from text
        }

        /// <summary>
        /// display the gui
        /// </summary>
        public void ShowWindow() { }

        /// <summary>
        /// auto free cam to fly around the world
        /// </summary>
        /// <param name="speed">camera velocity</param>
        public void RunDemoCamera(float speed) { }

        /// <summary>
        /// build a diagram from the editor contents
        /// </summary>
        /// <param name="result">input to compute</param>
        public void CreateDiagram(out object result) {
            // Layer -> Graph
            // Node  -> Effect
            // create the dictionary to hold the editor content
            var effectGraph = new Dictionary<Graph, List<Effect>>();
            effectGraph.Build(Contents); // convert layer node to the dictionary

            // create the diagram so graph is accessible to compute
            result = new Diagram(effectGraph);
        }
    }
    public static class LayerNodePairExtensions {
        /// <summary>
        /// builds the collection of layers and nodes
        /// </summary>
        /// <param name="destination">write to this</param>
        /// <param name="source">text asset string</param>
        public static void Extract(this List<LayerNodePair> destination, TextAsset source) {
            destination.ExtractLayers(source); // get the Layer
            destination.ExtractNodes(source);  // todo get each Node within the Layer
            destination.SetDataValue(source);  // todo set the value for the Controls
        }

        /// <summary>
        /// creates a pair for each key in the source
        /// </summary>
        /// <param name="contents">contents of the editor</param>
        /// <param name="source">text asset string</param>
        static void ExtractLayers(this List<LayerNodePair> contents, TextAsset source) {
            foreach (var key in source) {
                var layer = new LayerNodePair(key.name);
                contents.Add(layer);
            }
        }
    }
    public class LayerNodePair { }
}


