#nullable enable
using System;
using System.Reflection;

namespace VoroSystem.UI.Reflection {
public abstract class EditorMember {
    protected EditorMember(MemberInfo member) {
        Member = member;
    }

    public MemberInfo Member { get; }

    public virtual string Name => Member.Name;
    public abstract Type Type { get; }
    public abstract bool IsReadOnly { get; }
    public virtual Type? CustomElementType => default;

    public static EditorMember Create(MemberInfo member) {
        return member switch
        {
            FieldInfo field => new EditorField(field),
            PropertyInfo property => new EditorProperty(property),
            _ => throw new ArgumentException($"Unsupported member type: {member.GetType().Name}", nameof(member))
        };
    }

    public abstract object? GetValue(object? obj);
    public abstract void SetValue(object? obj, object? value);

    public virtual void SetValue<T>(ref T t, object? value) {
        object? boxed = t;
        SetValue(boxed, value);
        t = (T)boxed!;
    }

    public static EditorMember CreateFrom(Type type, string name, bool isReadOnly = false, Type? elementType = null) {
        var field = type.GetField(
            name,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        if (field != null) {
            return new EditorField(field);
        }

        var property = type.GetProperty(
            name,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        if (property != null) {
            return new EditorProperty(property);
        }

        throw new ArgumentException($"No field or property named '{name}' found in type '{type.Name}'");
    }
}
}