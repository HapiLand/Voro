#nullable enable
using System;
using System.Reflection;

namespace VoroSystem.UI.Reflection {
public class EditorProperty : EditorMember {
    readonly PropertyInfo _property;

    public EditorProperty(PropertyInfo property) : base(property) {
        _property = property;
    }

    public override Type Type => _property.PropertyType;

    /// <summary>
    /// True if the property cannot be set (no setter).
    /// </summary>
    public override bool IsReadOnly => _property.SetMethod is null;

    public override object? GetValue(object? obj) {
        return _property.GetValue(obj);
    }

    public override void SetValue(object? obj, object? value) {
        _property.SetValue(obj, value);
    }
}
}