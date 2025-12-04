using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.EditorSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.Core.Components.Editor.UIElements;
using VoroSystem.Voro.Utilities.Extensions;

namespace VoroSystem.Voro.Core.Components.Editor {
public class CoreEditorWindow : EditorWindow {
  #region Serialized Fields

  [SerializeField] VoroEvents events;
  [SerializeField] Diagram diagram;
  [SerializeField] string newLayerName;

  #endregion


  static int Padding => 10;

  #region Event Functions

  void OnEnable() {
    events = VoroEvents.GetInstance();
    events.OnDiagramCreatedEvent += OnDiagramCreated;
    events.OnDiagramUpdatedEvent += OnDiagramUpdated;
    newLayerName = "Default Name";
  }


  void OnDisable() {
    events.OnDiagramCreatedEvent -= OnDiagramCreated;
    events.OnDiagramUpdatedEvent -= OnDiagramUpdated;
  }

  void CreateGUI() {
    rootVisualElement.Clear();
    rootVisualElement.Add(new Container("editor", "-- Voro Core --"));
    
    rootVisualElement.Q<Container>("editor").Add(new Container("compute", "-- Voro Compute --"));
    CreateComputeGUI(rootVisualElement.Q<Container>("compute"));
    
    rootVisualElement.Q<Container>("editor").Add(new Container("graph", "-- Voro Diagram --"));
    if (diagram != null) {
      CreateDiagramGUI(rootVisualElement.Q<Container>("graph"), diagram);
    }
  }

  static void CreateDiagramGUI(Container container, Diagram diagram) {
    container.Add(new LabelElement("", "Name", diagram.name));
    
    container.Add(new Container("items", "-- Layers -- "));
    container.Q<Container>("items").Add(new Container("create", "Create Layer"));
    
    if (diagram.layers.Count > 0) {
      diagram.layers.ForEach(l => {
        container.Q<Container>("items").Add(new Container("layer", "Layer"));
      });
    }
  }
  
  static void CreateComputeGUI(Container container) {
    container.Add(new ButtonElement("button compute", "Compute", VoroEvents.GetInstance().RaiseClickCompute));
  }

  #endregion

  void OnDiagramUpdated() {
    CreateGUI();
  }

  void DiagramGUI() {
    CreateContainerElement(FlexDirection.Column, out var root);
    rootVisualElement.Add(root);

    CreateContainerElement(FlexDirection.Column, out var diagramRootElement);
    root.Add(diagramRootElement);

    diagramRootElement.Add(CreateHeading("Diagram", diagram.name));
    diagramRootElement.Add(CreateNewLayerButton(ref newLayerName));

    if (diagram.layers.Count > 0) {
      diagram.layers.ForEach(LayerGUI);
    }

    return;

    void FieldGUI(VisualElement nodeRootElement, FieldBase field) {
      CreateContainerElement(FlexDirection.Column, out var fieldElement);
      nodeRootElement.Add(fieldElement);
      fieldElement.Add(field.FieldUI());
    }

    void NodeGUI(VisualElement layerRootElement, Node node) {
      CreateContainerElement(FlexDirection.Column, out var nodeRootElement);
      layerRootElement.Add(nodeRootElement);

      nodeRootElement.Add(CreateHeading("Node", node.Name));
      nodeRootElement.Add(CreateModeChange(node));

      node.Fields.ForEach(field => { FieldGUI(nodeRootElement, field); });
    }

    void LayerGUI(Layer layer) {
      CreateContainerElement(FlexDirection.Column, out var layerRootElement);
      diagramRootElement.Add(layerRootElement);

      layerRootElement.Add(CreateHeading("Layer", layer.name));
      layerRootElement.Add(CreateNewNodeButton());

      if (layer.nodes.Count > 0) {
        layer.nodes.ForEach(node => { NodeGUI(layerRootElement, node); });
      }
    }
  }

  static VisualElement CreateNewLayerButton(ref string newName) {
    CreateContainerElement(FlexDirection.Row, out var visualElement);
    var layerNameField = new TextField("New Layer: ")
    {
      value = newName
    };
    var button = new Button(() => { VoroEvents.GetInstance().RaiseCreateNewLayer(layerNameField.value); })
    {
      text = $"Create '{layerNameField.value}'"
    };
    layerNameField.RegisterValueChangedCallback(evt => { button.text = $"Create '{evt.newValue}'"; });
    visualElement.Add(layerNameField);
    visualElement.Add(button);
    return visualElement;
  }

  static VisualElement CreateNewNodeButton() {
    CreateContainerElement(FlexDirection.Row, out var row);
    var effectTypes = Enum.GetNames(typeof(EffectBase.EffectType)).ToList();
    var dropdown = new DropdownField("Effect: ", effectTypes, 0);
    row.Add(dropdown);
    var button = new Button(() => {
      VoroEvents.GetInstance()
        .RaiseCreateNewNode((EffectBase.EffectType)Enum.Parse(typeof(EffectBase.EffectType), dropdown.value));
    })
    {
      text = $"Add: {dropdown.value}"
    };
    row.Add(button);
    dropdown.RegisterValueChangedCallback(evt => { button.text = $"Add: {evt.newValue}"; });
    return row;
  }


  static void CreateContainerElement(FlexDirection flexDirection, out VisualElement element) {
    element = new VisualElement
    {
      style =
      {
        backgroundColor = new StyleColor(EditorBackgroundColor.Bg.ToRGB()),
        paddingBottom = Padding,
        paddingTop = Padding,
        paddingLeft = Padding,
        paddingRight = Padding,
        alignItems = Align.FlexStart,
        flexDirection = flexDirection
      }
    };
  }

  static VisualElement CreateHeading(string headingTitle, string label) {
    CreateContainerElement(FlexDirection.Row, out var visualElement);
    visualElement.Add(new Label($"{headingTitle}: "));
    CreateContainerElement(FlexDirection.Row, out var headingLabel);
    headingLabel.Add(new Label(label));
    visualElement.Add(headingLabel);
    return visualElement;
  }

  static VisualElement CreateModeChange(Node node) {
    CreateContainerElement(FlexDirection.Row, out var row);
    var modeTypes = Enum.GetNames(typeof(EffectBase.EffectMode)).ToList();
    var current = modeTypes.IndexOf(node.Mode.ToString());
    current = current < 0 ? 0 : current;
    var dropdown = new DropdownField("Mode: ", modeTypes, current);
    row.Add(dropdown);
    node.Mode = (EffectBase.EffectMode)Enum.Parse(typeof(EffectBase.EffectMode), dropdown.value);
    dropdown.RegisterValueChangedCallback(evt => {
      VoroEvents.GetInstance().RaiseDiagramUpdated();
      node.Mode = (EffectBase.EffectMode)Enum.Parse(typeof(EffectBase.EffectMode), evt.newValue);
    });
    return row;
  }

  void OnDiagramCreated(Diagram diagram) {
    this.diagram = diagram;
    CreateGUI();
  }

  public static void ShowWindow() {
    var wnd = GetWindow<CoreEditorWindow>();
    wnd.titleContent = new GUIContent("Voro");
  }
}
}