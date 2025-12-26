using System;
using System.Reflection;

namespace VoroSystem.UI.Editor.Reflection {
public abstract class EditorMember {
  readonly MemberInfo _member;
  public MemberInfo Member => _member;
  public static EditorMember Create(MemberInfo member)
  {
    return member switch
    {
      FieldInfo field => new EditorField(field),
      PropertyInfo property => new EditorProperty(property),
      _ => throw new NotImplementedException(),
    };
  }
  protected EditorMember(MemberInfo member) => _member = member;
  public abstract object? GetValue(object? obj);
  public abstract void SetValue(object? obj, object? value);
  public virtual void SetValue<T>(ref T t, object? value)
  {
    object? obj = t;
    SetValue(obj, value);

    t = (T)obj!;
  }
  public virtual string Name => _member.Name;
  public abstract Type Type { get; }
  public abstract bool IsReadOnly { get; }
  public virtual Type? CustomElementType => default;
}
}