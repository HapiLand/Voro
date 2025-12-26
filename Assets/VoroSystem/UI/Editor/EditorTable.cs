using System;
using UnityEditor;
using UnityEngine;

namespace VoroSystem.UI.Editor {
[Serializable]
public class EditorTable : IDisposable {
  readonly float[] _columnWidths;
  int _columnIndex;
  bool _rowOpen;
  bool _tableOpen;
  Rect _tableRect;

  public EditorTable(params float[] columnWidths) {
    _columnWidths = columnWidths;
    _columnIndex = 0;
    _rowOpen = false;

    EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(true) /*, GUILayout.ExpandHeight(true)*/);
    _tableOpen = true;
  }

  #region IDisposable Members
  public void Dispose() {
    EndRow();
    if (!_tableOpen) {
      return;
    }

    EditorGUILayout.EndVertical();
    _tableRect = GUILayoutUtility.GetLastRect();
    if (Event.current.type == EventType.Repaint) {
      Handles.BeginGUI();
      Handles.DrawSolidRectangleWithOutline(_tableRect, Color.clear, Color.black);
      Handles.EndGUI();
    }

    _tableOpen = false;
  }
  #endregion

  public void NextRow() {
    EndRow();
    EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true) /*, GUILayout.ExpandHeight(true)*/);
    _columnIndex = 0;
    _rowOpen = true;
  }

  public void DrawHorizontalLine(float thickness = 1f, Color? color = null, float padding = 2f) {
    EndRow();
    color ??= Color.black;
    if (padding > 0) {
      GUILayout.Space(padding);
    }

    var lineRect = EditorGUILayout.GetControlRect(false, thickness);
    EditorGUI.DrawRect(lineRect, color.Value);
    if (padding > 0) {
      GUILayout.Space(padding);
    }
  }

  public void NextColumn() {
    if (!_rowOpen) {
      NextRow();
    }

    var width = _columnWidths[_columnIndex];

    switch (width) {
    case > 0: // columnA
      EditorGUILayout.BeginVertical(GUILayout.Width(width), GUILayout.ExpandHeight(false));
      break;
    case -1: // columnB
      EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));
      break;
    default:
      EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));
      break;
    }

    _columnIndex++;
  }

  public void EndColumn() {
    EditorGUILayout.EndVertical();
  }

  void EndRow() {
    if (!_rowOpen) {
      return;
    }

    EditorGUILayout.EndHorizontal();
    _rowOpen = false;
  }
}
}