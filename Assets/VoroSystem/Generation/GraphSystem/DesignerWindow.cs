using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Generation.GraphSystem.Graph;

namespace VoroSystem.Generation.GraphSystem {
public class DesignerWindow : EditorWindow {
    [SerializeField] DesignerComponent target;
    string _newLayerName = "";
    int _selectedEffectIndex;

    void OnGUI() {
        if (!target) {
            GUILayout.Label("No Designer Component found");
            return;
        }

        DrawGraph();
        return;

        void DrawGraph() {
            var graph = target.graph;
            GUILayout.Label($"Graph: {graph.Name}");
            _newLayerName = EditorGUILayout.TextField("Layer Name", _newLayerName);
            if (GUILayout.Button("Create Layer")) {
                if (string.IsNullOrWhiteSpace(_newLayerName)) {
                    return;
                }

                target.CreateLayer(_newLayerName);
            }

            DrawUILine(Color.red);
            GUILayout.Space(10);
            foreach (var layer in graph.Layers) {
                DrawLayer(layer);
            }
        }

        void DrawLayer(GraphLayer gl) {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Layer: {gl.Name} ({gl.SortOrder})");
            gl.DrawGUI();
            GUILayout.EndHorizontal();
            var effectNames = EffectLookup.Names.ToList();
            _selectedEffectIndex = EditorGUILayout.Popup("Select Effect", _selectedEffectIndex, effectNames.ToArray());
            if (GUILayout.Button("Add Effect")) {
                var selectedName = effectNames[_selectedEffectIndex];
                var effectDef = EffectLookup.Get(selectedName);
                gl.CreateEffect(effectDef);
            }

            DrawUILine(Color.lightGreen);
            GUILayout.Space(10);
            foreach (var effect in gl.Effects) {
                DrawEffect(effect);
            }
        }

        void DrawEffect(LayerEffect effect) {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Effect: {effect.Name} ({effect.Operation.ToString()})");
            effect.DrawGUI();
            GUILayout.EndHorizontal();
            foreach (var field in effect.Fields) {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Field: {field.Name}"); // display the name of this field
                field.DrawGUI();
                GUILayout.EndHorizontal();
            }

            DrawUILine(Color.cornflowerBlue);
        }

        void DrawUILine(Color color, int thickness = 1, int padding = 10) {
            var r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2f;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }
    }

    [MenuItem("Voro/Diagram/Show Editor", false, 1)]
    public static void ShowDesigner() {
        var comp = DesignerComponent.Instance;
        var wnd = GetWindow<DesignerWindow>();
        wnd.titleContent = new GUIContent("Designer");
        wnd.target = comp;
    }
}
}