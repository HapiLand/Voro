using System;
using System.Collections.Generic;
using System.Linq;
using EditorGUI.Elements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorGUI.Panels.Managers {
/// <summary>
///     handles the user created diagrams within the editor
/// </summary>
public class DiagramManager {
    readonly VisualElement _diagramContainer;

    public DiagramManager(VisualElement container) {
        _diagramContainer = container;
    }

    public string SelectedDiagramName => DiagramElement.SelectedDiagram.DisplayName;
    // todo store actual Diagram here

    public IEnumerable<DiagramElement> DiagramElements {
        get
        {
            foreach (var child in _diagramContainer.Children()) {
                if (child is DiagramElement diagramElement) {
                    yield return diagramElement;
                }
            }
        }
    }

    public event Action<string> OnDiagramCreated;

    public DiagramElement CreateDiagramElement(Action<DiagramElement> onClicked = null) {
        var diagramName = DiagramElements.Count() switch
        {
            0 => "Player Spawn",
            1 => "Flat",
            2 => "Grass",
            3 => "Cliffs",
            4 => "Safehouse",
            5 => "Level Border",
            6 => "Forest",
            7 => "Props",
            8 => "Paths",
            9 => "Water",
            _ => "Null"
        };

        var element = new DiagramElement
        {
            DisplayName = diagramName,
            name = diagramName
        };

        element.clicked += () => {
            Debug.Log($"{diagramName} Selected");

            // notify any external listeners
            OnDiagramCreated?.Invoke(diagramName);

            // callback to the LayerList frame
            onClicked?.Invoke(element);
        };

        return element;
    }
}
}