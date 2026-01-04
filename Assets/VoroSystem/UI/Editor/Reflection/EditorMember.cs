using System;
using System.Reflection;

namespace VoroSystem.UI.Editor.Reflection {
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
      _ => throw new NotImplementedException()
    };
  }

  public abstract object? GetValue(object? obj);
  public abstract void SetValue(object? obj, object? value);

  public virtual void SetValue<T>(ref T t, object? value) {
    object? obj = t;
    SetValue(obj, value);

    t = (T)obj!;
  }
}
}