using System;
using System.Reflection;

namespace VoroSystem.UI.Editor.Reflection {
public class EditorField : EditorMember {
  readonly FieldInfo _field;

  public EditorField(FieldInfo field) : base(field) {
    _field = field;
  }

  public override Type Type => _field.FieldType;

  public override bool IsReadOnly => false;

  public override object? GetValue(object? obj) {
    return _field.GetValue(obj);
  }

  public override void SetValue(object? obj, object? value) {
    _field.SetValue(obj, value);
  }
}
}