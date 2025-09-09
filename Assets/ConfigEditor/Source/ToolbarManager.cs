using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ConfigEditor.Source {
/// <summary>
///     creates the toolbar menu and callbacks
/// </summary>
public class ToolbarManager {
    readonly ColumnManager _columnManager;
    readonly EditorCompute _editorCompute;
    readonly NodeFactory _nodeFactory;

    /// <summary>
    ///     generates the GUI toolbar, the toolbar contains a set of effects to be added to the active column
    /// </summary>
    /// <param name="toolbarPanel"></param>
    /// <param name="manager"></param>
    /// <param name="factory"></param>
    /// <param name="compute"></param>
    public ToolbarManager(VisualElement toolbarPanel, ColumnManager manager, NodeFactory factory,
        EditorCompute compute) {
        _columnManager = manager;
        _nodeFactory = factory;
        _editorCompute = compute;

        var toolbar = new Toolbar();

        // add button to begin the refresh the scene
        var refreshSceneButton = new Button { text = "Refresh Scene" };
        refreshSceneButton.clicked += () => EditorSceneManager.OpenScene("Assets/Scenes/GameWorld.unity");

        // add button so a new column can be added into the editor
        var addColumnButton = new Button { text = "Add Column" };
        addColumnButton.clicked += () => _columnManager.AddColumn();

        // add button to begin the EditorCompute
        var doComputeButton = new Button { text = "Compute" };
        doComputeButton.clicked += () => WorldManager.Instance.ComputeWorldTiles();

        toolbar.Add(refreshSceneButton);
        toolbar.Add(addColumnButton);
        toolbar.Add(doComputeButton);
        toolbarPanel.Add(toolbar);

        SetupMenus(toolbar);
    }

    void SetupMenus(Toolbar toolbar) {
        // populate the toolbar menu with effects
        var menuPaths = new Dictionary<string, string>
        {
            #region Generation

            { "Generation/Slope", ResourceHelper.LoadNode("Slope") },
            { "Generation/SetHeight", ResourceHelper.LoadNode("SetHeight") },
            { "Generation/Noise", ResourceHelper.LoadNode("Noise") },

            #endregion

            #region Manipulate

            { "Manipulate/Terrace", ResourceHelper.LoadNode("Terrace") },

            #endregion

            #region Tags

            { "Tags/SetTag", ResourceHelper.LoadNode("SetTag") },

            #endregion

            #region Other

            { "Other/Null", ResourceHelper.LoadNode("Null") },

            #endregion
        };

        // store the menu for each category
        var menus = new Dictionary<string, ToolbarMenu>();

        // for every category, create its contents
        foreach (var kvp in menuPaths) {
            var pathParts = kvp.Key.Split('/');
            var category = pathParts[0];
            var effect = pathParts[1];

            // the actual node associated with this button
            var node = kvp.Value;

            // check if the menu already exists for this category
            if (!menus.TryGetValue(category, out var menu)) {
                // a menu does not exist yet, creating now

                menu = new ToolbarMenu { text = category };
                // add this category into the effect menu hierarchy
                toolbar.Add(menu);
                menus[category] = menu; // track this category as existing
            }

            // append the effects into the menu
            menu.menu.AppendAction(effect, _ => _columnManager.AddEffectToSelectedColumn(node));
        }
    }
}
}