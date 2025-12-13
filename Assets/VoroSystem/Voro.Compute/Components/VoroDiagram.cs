using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;
using VoroSystem.Voro.Core.Events;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.Components {
[ExecuteAlways]
public class VoroDiagram : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] public Diagram diagram;
  [SerializeField] public TextAsset jsonSource;

  #endregion

  DiagramDTO DiagramDTO {
    get
    {
      var root = JObject.Parse(jsonSource.text);
      var diagramDto = root.ToObject<DiagramDTO>();

      // the fieldDTOs need to be constructed as are not contained in the file
      foreach (var nodeDto in diagramDto.layers.SelectMany(layerDto => layerDto.nodes)) {
        nodeDto.LoadFields();
      }

      return diagramDto;
    }
  }

  #region Event Functions

  void Awake() {
    jsonSource = Resources.Load<TextAsset>("Template");
  }

  void Start() {
    LoadDiagram();
  }

  #endregion

  void LoadDiagram() {
    var diagramDto = DiagramDTO;

    diagram = diagramDto.ToDiagram();
    DiagramEvents.GetInstance().RaiseOnCreated(diagram);
  }
}
}