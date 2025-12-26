#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VoroSystem.UI.Editor.Attributes;
using VoroSystem.UI.Editor.Reflection;

namespace VoroSystem.UI.Editor.Fields {
[Serializable]
public abstract class CustomField {
  static readonly Dictionary<Type, CustomField> FieldMap = BuildFieldMap();
  public abstract (bool modified, object? result) ProcessInput(EditorMember member, object? fieldValue);

  static Dictionary<Type, CustomField> BuildFieldMap() {
    /*var assembly = Assembly.GetExecutingAssembly();
    Debug.Log($"assembly name: {assembly.FullName}");

    var types = assembly.GetTypes();
    Debug.Log($"types length: {types.Length}");

    var eligibleTypes = types
      .Where(t => !t.IsAbstract && typeof(CustomField).IsAssignableFrom(t))
      .ToArray();
    Debug.Log($"eligible types length:  {eligibleTypes.Length}");

    foreach (var t in eligibleTypes) {
      Debug.Log($"custom field name {t.FullName}");
    }

    var withAttributes = eligibleTypes
      .Select(t => new
      {
        Type = t,
        Attribute = t.GetCustomAttribute<CustomFieldOfAttribute>()
      })
      .ToArray();
    foreach (var x in withAttributes) {
      if (x.Attribute == null) {
        Debug.LogWarning($"{x.Type.FullName} no custom field attribute");
      }
      else {
        Debug.Log($"{x.Type.FullName} custom field attribute type: {x.Attribute.OfType}");
      }
    }

    var validMappings = withAttributes
      .Where(x => x.Attribute != null)
      .ToArray();
    Debug.Log($"valid map length: {validMappings.Length}");
    var dict = new Dictionary<Type, CustomField>();
    foreach (var x in validMappings) {
      Debug.Log($"creating: {x.Type.FullName}");
      var instance = (CustomField)Activator.CreateInstance(x.Type)!;
      var keyType = x.Attribute!.OfType;
      Debug.Log($"key type {keyType}  {instance.GetType().FullName}");
      dict.Add(keyType, instance);
    }

    Debug.Log($"completed, dictionary length: {dict.Count}");
    foreach (var kvp in dict) {
      Debug.Log($"final dictionary: {kvp.Key} : {kvp.Value.GetType().FullName}");
    }*/

    return Assembly.GetExecutingAssembly()
      .GetTypes()
      .Where(t => !t.IsAbstract && typeof(CustomField).IsAssignableFrom(t))
      .Select(t => new
      {
        Type = t,
        Attribute = t.GetCustomAttribute<CustomFieldOfAttribute>()
      })
      .Where(x => x.Attribute != null)
      .ToDictionary(
        x => x.Attribute!.OfType,
        x => (CustomField)Activator.CreateInstance(x.Type)!
      );
  }

  public static (bool modified, object? result) DrawValue(EditorMember member, object? value) {
    if (!FieldMap.TryGetValue(member.Type, out var field)) {
      return (false, value);
    }

    return field.ProcessInput(member, value);
  }

  public static (bool modified, object? result) DrawValueWithLabel(EditorMember member, object? value, string fieldName,
    EditorTable table) {
    table.NextColumn();
    GUILayout.Label(fieldName, EditorStyles.boldLabel);
    table.EndColumn();

    table.NextColumn();
    var result = DrawValue(member, value);
    table.EndColumn();

    return result;
  }

  static bool DrawValue<T>(ref T target, EditorMember member) {
    var (modified, boxedResult) = DrawValue(member, member.GetValue(target));
    if (!modified || boxedResult is not T typedResult) {
      return modified;
    }

    member.SetValue(ref target, typedResult);
    return true;
  }

  static bool DrawValue<T>(ref T target, string fieldName) {
    var member = TryGetFieldForEditor(typeof(T), fieldName);
    return member != null && DrawValue(ref target, member);
  }

  static bool DrawValueWithLabel<T>(ref T target, string fieldName) {
    var member = TryGetFieldForEditor(typeof(T), fieldName);
    if (member == null) {
      return false;
    }

    GUILayout.Label(fieldName, EditorStyles.boldLabel);
    return DrawValue(ref target, member);
  }

  static bool DrawValue<T>(EditorMember member, T input, [NotNullWhen(true)] out T? result) {
    var (modified, boxedResult) = DrawValue(member, input);
    result = boxedResult is T typed ? typed : default;
    return modified;
  }

  static EditorMember? TryGetFieldForEditor(Type type, string name) {
    var allFields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    var fieldInfo = allFields.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
    if (fieldInfo != null) {
      return EditorMember.Create(fieldInfo);
    }

    var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    var propInfo = allProperties.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
    return propInfo != null ? EditorMember.Create(propInfo) : null;
  }
}
}