using Newtonsoft.Json.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.Components {
[ExecuteAlways]
public class VoroDiagram : MonoBehaviour {
    #region Event Functions
    void Awake() {
        jsonSource = Resources.Load<TextAsset>("Template");
        LoadDiagram();
    }
    #endregion

    void LoadDiagram() {
        var diagramDto = BuildDiagramDTO();
        diagram = diagramDto.ToDiagram();
        return;

        DiagramDTO BuildDiagramDTO() {
            var root = JObject.Parse(jsonSource.text);
            var diagramName = root["Name"]?.ToString() ?? "Unnamed Diagram";
            var diagramDto = new DiagramDTO(diagramName);

            BuildLayers(root, diagramDto);

            return diagramDto;
        }

        void BuildLayers(JObject root, DiagramDTO diagramDto) {
            if (root["Layers"] is not JArray jArray) {
                return;
            }

            foreach (var layerToken in jArray) {
                var layerDto = layerToken.ToObject<LayerDTO>();
                BuildNodes(layerToken, layerDto);
                diagramDto.layers.Add(layerDto);
            }
        }

        void BuildNodes(JToken layerToken, LayerDTO layerDto) {
            if (layerToken["Nodes"] is not JArray jArray) {
                return;
            }

            foreach (var nodeToken in jArray) {
                var nodeDto = nodeToken.ToObject<NodeDTO>();
                layerDto.nodes.Add(nodeDto);
            }
        }
    }

    #region Serialized Fields
    [SerializeField] public Diagram diagram;
    [SerializeField] public TextAsset jsonSource;
    #endregion
}
}