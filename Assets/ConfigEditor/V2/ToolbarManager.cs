using System.Collections.Generic;
using Internal;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
/// <summary>
///     creates the toolbar menu and callbacks
/// </summary>
public class ToolbarManager {
    readonly ColumnManager _columnManager;
    readonly NodeFactory _nodeFactory;

    /// <summary>
    ///     generates the GUI toolbar, the toolbar contains a set of effects to be added to the active column
    /// </summary>
    /// <param name="toolbarPanel"></param>
    /// <param name="manager"></param>
    /// <param name="factory"></param>
    public ToolbarManager(VisualElement toolbarPanel, ColumnManager manager, NodeFactory factory) {
        _columnManager = manager;
        _nodeFactory = factory;

        var toolbar = new Toolbar();
        var addColumnButton = new Button { text = "Add Column" };
        addColumnButton.clicked += () => _columnManager.AddColumn();

        toolbar.Add(addColumnButton);
        toolbarPanel.Add(toolbar);

        SetupMenus(toolbar);
    }

    void SetupMenus(Toolbar toolbar) {
        // populate the toolbar menu with effects
        var menuPaths = new Dictionary<string, VisualTreeAsset>
        {
            { "Basic/Slope", ResourceHelper.LoadEffectUXML("Slope") },
            { "Fancy/Noise", ResourceHelper.LoadEffectUXML("Noise") },
            { "Special/Terrace", ResourceHelper.LoadEffectUXML("Terrace") },
            { "Special/Null", ResourceHelper.LoadEffectUXML("Null") },
            { "Groups/SetGroup", ResourceHelper.LoadEffectUXML("SetGroup") }
        };

        // store the menu for each category
        var menus = new Dictionary<string, ToolbarMenu>();

        // for every category, create its contents
        foreach (var kvp in menuPaths) {
            var pathParts = kvp.Key.Split('/');
            var category = pathParts[0];
            var effect = pathParts[1];
            // the uxml effect associated with this button
            var vta = kvp.Value;

            // check if the menu already exists for this category
            if (!menus.TryGetValue(category, out var menu)) {
                // a menu does not exist yet, creating now

                menu = new ToolbarMenu { text = category };
                // add this category into the effect menu hierarchy
                toolbar.Add(menu);
                menus[category] = menu; // track this category as existing
            }

            // append the effects into the menu
            menu.menu.AppendAction(effect, _ => _columnManager.AddEffectToSelectedColumn(effect));
        }
    }
}
}