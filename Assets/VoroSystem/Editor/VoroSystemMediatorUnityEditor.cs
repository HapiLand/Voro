using UnityEditor;
using UnityEngine;

namespace VoroSystem.Editor {
[CustomEditor(typeof(VoroSystemMediator))]
public class VoroSystemMediatorUnityEditor : UnityEditor.Editor {
    VoroSystemMediator _mediator;

    void OnEnable() {
        _mediator = (VoroSystemMediator)target;
    }

    public override void OnInspectorGUI() {
        if (GUILayout.Button("New Map Designer")) {
            _mediator.InitializeMapDesigner();
        }

        if (GUILayout.Button("New Graph Designer")) {
            _mediator.InitializeGraphDesigner();
        }

        if (GUILayout.Button("New Terrain Generator")) {
            _mediator.InitializeTerrainGenerator();
        }
    }
}
}