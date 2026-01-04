using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using VoroSystem.UI.Editor.Reflection;

namespace VoroSystem.UI.Editor.Attributes {
public static class AttributeHelper {
  public static bool TryGetAttribute<T>(EditorMember member, [NotNullWhen(true)] out T? attribute)
    where T : Attribute => TryGetAttribute(member.Member, out attribute);

  public static bool TryGetAttribute<T>(MemberInfo member, [NotNullWhen(true)] out T? attribute) where T : Attribute {
    if (Attribute.IsDefined(member, typeof(T)) &&
        Attribute.GetCustomAttribute(member, typeof(T)) is T customAttribute) {
      attribute = customAttribute;
      return true;
    }

    attribute = default;
    return false;
  }
}
}