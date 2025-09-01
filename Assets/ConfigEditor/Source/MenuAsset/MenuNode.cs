using System.Collections.Generic;
using Internal;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.MenuAsset {
/// <summary>
///     a menu node, this stores child menu nodes
///     this is used by the EffectMenu for a collapsible menu of UXML objects
/// </summary>
class MenuNode {
    static readonly Dictionary<string, KeyValuePair<string, string>> _findByUXMLName = new();
    readonly Dictionary<string, MenuNode> _children = new();
    public string Name;
    public string UxmlName;

    public MenuNode(string name) {
        Name = name;
    }

    public MenuNode GetOrAddChild(string name) {
        // recursive create MenuNodes as the menu is made of nested objects
        if (!_children.TryGetValue(name, out var child)) {
            child = new MenuNode(name);
            _children[name] = child;
        }

        return child;
    }

    public void SetKVP(string path, string uxmlName) {
        _findByUXMLName[uxmlName] = new KeyValuePair<string, string>(path, uxmlName);
    }

    /// <summary>
    ///     get the menu path and the uxml asset
    /// </summary>
    /// <param name="uxmlName">the UXML asset to load</param>
    /// <returns></returns>
    public KeyValuePair<string, VisualTreeAsset>? GetKVP(string uxmlName) {
        if (_findByUXMLName.TryGetValue(uxmlName, out var kvp)) {
            var path = kvp.Key;
            var asset = ResourceHelper.LoadEffectUXML(kvp.Value);
            return new KeyValuePair<string, VisualTreeAsset>(path, asset);
        }

        return null;
    }
}
}