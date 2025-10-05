using UnityEditor;
using UnityEngine;
using VoroSystem.UserInterface.Interface;

namespace VoroSystem.UserInterface {
public class Window : EditorWindow {
    IUserInterfaceMediator _mediator;

    void OnGUI() {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Layers", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        _mediator.ForEachLayer(DrawLayer);
        EditorGUI.indentLevel--;

        if (GUILayout.Button("+ Layer +")) {
            _mediator.CreateLayer("Foo");
        }

        EditorGUILayout.EndVertical();

        return;

        void DrawLayer(Layer layer) {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(layer.Name, layer.Active ? EditorStyles.boldLabel : EditorStyles.label);
            layer.Active = EditorGUILayout.Toggle("", layer.Active);

            var currentIndex = _mediator.GetLayerIndex(layer);
            if (GUILayout.Button("↑")) {
                _mediator.MoveUp(layer);
            }

            if (GUILayout.Button("↓")) {
                _mediator.MoveDown(layer);
            }

            if (GUILayout.Button("🗑")) {
                _mediator.RemoveLayer(layer);
                return;
            }

            EditorGUILayout.EndHorizontal();

            if (layer.Active) {
                DrawNodesInLayer(layer);

                if (GUILayout.Button("+ Node +")) {
                    _mediator.CreateNode(EffectName.SetElevation);
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        void DrawNodesInLayer(Layer layer) {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Nodes");
            EditorGUI.indentLevel++;

            _mediator.ForEachNode(DrawNode, layer);

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        void DrawNode(Node node) {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(node.Name, node.Active ? EditorStyles.boldLabel : EditorStyles.label);
            node.Active = EditorGUILayout.Toggle("", node.Active);

            var currentIndex = _mediator.GetNodeIndex(node);
            if (GUILayout.Button("↑")) {
                _mediator.MoveUp(node);
            }

            if (GUILayout.Button("↓")) {
                _mediator.MoveDown(node);
            }


            if (GUILayout.Button("🗑")) {
                _mediator.RemoveNode(node);
                return;
            }

            EditorGUILayout.EndHorizontal();

            if (node.Active) {
                DrawNodeControls(node);
            }

            EditorGUILayout.EndVertical();
        }

        void DrawNodeControls(Node node) {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Controls");
            EditorGUI.indentLevel++;

            _mediator.ForEachControl(DrawControl, node);

            void DrawControl(Control control) {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField(control.Name);
                control.Value = EditorGUILayout.FloatField(control.Value);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }
    }

    public void SetMediator(IUserInterfaceMediator mediator) {
        _mediator = mediator;
    }
}
}