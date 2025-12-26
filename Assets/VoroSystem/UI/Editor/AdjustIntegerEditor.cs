using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VoroSystem.UI.Editor.Attributes;
using VoroSystem.UI.Editor.Fields;
using VoroSystem.UI.Editor.Reflection;

namespace VoroSystem.UI.Editor {
/// <summary>
/// a custom editor that is used to generate integer values
/// </summary>
[CustomEditorOf(typeof(AdjustIntegerEditorData))]
public class AdjustIntegerEditor : CustomEditor {
  List<EditorMember> _members;
  object? _targetInstance;

  public void Initialize() {
    if (!AttributeHelper.TryGetAttribute<CustomEditorOfAttribute>(GetType(), out var attributeType)) {
      return;
    }

    // get all the fields for the editor
    _targetInstance = Activator.CreateInstance(attributeType.OfType);
    _members = new List<EditorMember>();

    var targetType = attributeType.OfType;
    bool IsSupportedType(Type type) => type == typeof(int) || type == typeof(string) || type == typeof(bool) || type.IsEnum;

    // fields
    var fieldMembers = targetType
      .GetFields(BindingFlags.Public | BindingFlags.Instance)
      .Where(f => IsSupportedType(f.FieldType))
      .Select(f => EditorMember.Create(f));

    // properties
    var propertyMembers = targetType
      .GetProperties(BindingFlags.Public | BindingFlags.Instance)
      .Where(p => IsSupportedType(p.PropertyType) &&
                  p.CanRead &&
                  p.CanWrite &&
                  p.GetIndexParameters().Length == 0)
      .Select(p => EditorMember.Create(p));

    foreach (var member in fieldMembers.Concat(propertyMembers)) {
      _members.Add(member);
    }
  }

  public void Draw() {
    Check(out var allowDraw);
    if (!allowDraw) {
      return;
    }

    /*EditorGUILayout.BeginHorizontal();
    GUILayout.Label("[ICON]", EditorStyles.boldLabel);
    GUILayout.Label("Test Editor", EditorStyles.boldLabel);
    GUILayout.FlexibleSpace();
    GUILayout.Label("[SETTINGS]", EditorStyles.boldLabel);
    EditorGUILayout.EndHorizontal();*/

    using var table = new EditorTable(150, -1);
    foreach (var member in _members) {
      table.NextRow();
      var value = member.GetValue(_targetInstance!);

      var result = CustomField.DrawValueWithLabel(member, value, member.Name, table);
      if (!result.modified) {
        table.DrawHorizontalLine(1, Color.black);
        continue;
      }
      member.SetValue(_targetInstance!, result.result);
      GUI.changed = true; // trigger repaint
      
      table.DrawHorizontalLine(1, Color.black);
    }

    /*using var table = new EditorTable(120, 120, 120, 120);
    foreach (var (member, processor) in _fields) {
      table.NextRow();
      GUILayout.Label(member.Member.Name, EditorStyles.boldLabel);
      var value = member.GetValue(_targetInstance!);
      var result = processor.ProcessInput(member, value);
      if (!result.modified) {
        continue;
      }
      member.SetValue(_targetInstance!, result.result);
      GUI.changed = true; // trigger repaint
      table.DrawHorizontalLine(1, Color.black);
    }
    DrawGeneral(table);
    table.DrawHorizontalLine(1, Color.black);
    DrawFields(table);*/
  }


  void DrawGeneral(EditorTable table) {
    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("General", EditorStyles.boldLabel);
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Group", EditorStyles.boldLabel);
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[text input field]");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[dropdown arrow]");
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Attribute Name", EditorStyles.boldLabel);
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[text input field]");
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Adjustment Value", EditorStyles.boldLabel);
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[checkbox]");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("Adjust Value");
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Operation", EditorStyles.boldLabel);
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[dropdown] (e.g. Add)");
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Pattern Type", EditorStyles.boldLabel);
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[dropdown] (e.g. Constant)");
      table.EndColumn();
    }
  }

  void DrawFields(EditorTable table) {
    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Constant Value", EditorStyles.boldLabel);
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[numeric input]");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[slider]");
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Post-Process", EditorStyles.boldLabel);
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("[checkbox]");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("Minimum", EditorStyles.boldLabel);
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[numeric input]");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[slider]");
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("[checkbox]");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("Maximum", EditorStyles.boldLabel);
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[numeric input]");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[slider]");
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Attribute Properties", EditorStyles.boldLabel);
      table.EndColumn();
    }

    {
      table.NextRow();
      table.NextColumn();
      GUILayout.Label("Default Value", EditorStyles.boldLabel);
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[numeric input]");
      table.EndColumn();
      table.NextColumn();
      GUILayout.Label("[slider]");
      table.EndColumn();
    }
  }

  /// <summary>
  /// check to make sure drawing is allowed
  /// </summary>
  /// <param name="b"> </param>
  void Check(out bool b) {
    b =
      _targetInstance != null &&
      _members is { Count: > 0 };
  }
}
}