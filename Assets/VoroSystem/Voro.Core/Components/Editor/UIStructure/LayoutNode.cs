using System;
using System.Collections.Generic;

namespace VoroSystem.Voro.Core.Components.Editor.UIStructure {
/// <summary>
/// layout tree structure
/// </summary>
[Serializable]
public class LayoutNode {
    [Serializable]
    public enum ElementType {
        Container,
        Label,
        Button
    }
    /// <summary>
    /// visual element name
    /// </summary>
    public string id;
    /// <summary>
    /// label text
    /// </summary>
    public string title;
    /// <summary>
    /// the ui element type
    /// </summary>
    public ElementType type;
    /// <summary>
    /// content
    /// </summary>
    public List<LayoutNode> children = new();
}
}