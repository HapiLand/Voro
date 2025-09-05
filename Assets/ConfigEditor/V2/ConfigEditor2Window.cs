using Internal;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ConfigEditor2Window : EditorWindow {
    [SerializeField] VisualTreeAsset m_VisualTreeAsset;

    public void CreateGUI() {
        // each editor window contains a root VisualElement object
        var root = rootVisualElement;

        var styleSheet = ResourceHelper.LoadResource<StyleSheet>("config_editor");
        root.styleSheets.Add(styleSheet);

        // instantiate UXML designed in the UI Builder
        // VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        // while (labelFromUXML.childCount > 0) {
        //     root.Add(labelFromUXML.ElementAt(0));
        // }

        #region Add to GUI

        // create the panels that the GUI is split into

        var rootPanel = new VisualElement
        {
            name = "Root",
            style =
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1
            }
        };
        root.Add(rootPanel);

        var mainPanel = new VisualElement
        {
            name = "Main",
            style =
            {
                flexDirection = FlexDirection.Column,
                flexGrow = 1
            }
        };
        var inspectorPanel = new VisualElement
        {
            name = "Inspector"
        };
        inspectorPanel.AddToClassList("inspector");
        rootPanel.Add(mainPanel);
        rootPanel.Add(inspectorPanel);
        
        // ToDo change WorldPanel height when mouse hovers over
        var worldPanel = new VisualElement
        {
            name = "World"
        };
        worldPanel.AddToClassList("world");
        var canvasPanel = new VisualElement
        {
            name = "Canvas",
            style =
            {
                flexDirection = FlexDirection.Column,
                flexGrow = 1
            }
        };
        mainPanel.Add(worldPanel);
        mainPanel.Add(canvasPanel);

        var toolbarPanel = new VisualElement
        {
            name = "Toolbar"
        };
        toolbarPanel.AddToClassList("toolbar");

        // ToDo automatically populate the toolbar menus
        // toolbar with effects
        var toolbar = new Toolbar();
        toolbarPanel.Add(toolbar);
        // a toolbar menu for each category
        var toolbarFooMenu = new ToolbarMenu { text = "Foo" };
        toolbarFooMenu.menu.AppendAction("Foo item 1", a => { Debug.Log("Foo item 1 clicked"); });
        toolbarFooMenu.menu.AppendAction("Foo item 2", a => { Debug.Log("Foo item 2 clicked"); });
        toolbarFooMenu.menu.AppendAction("Foo item 3", a => { Debug.Log("Foo item 3 clicked"); });
        toolbar.Add(toolbarFooMenu);
        // a toolbar menu for each category
        var toolbarBarMenu = new ToolbarMenu { text = "Bar" };
        toolbarBarMenu.menu.AppendAction("Bar item 1", a => { Debug.Log("Bar item 1 clicked"); });
        toolbarBarMenu.menu.AppendAction("Bar item 2", a => { Debug.Log("Bar item 2 clicked"); });
        toolbarBarMenu.menu.AppendAction("Bar item 3", a => { Debug.Log("Bar item 3 clicked"); });
        toolbar.Add(toolbarBarMenu);

        // ToDo add button to create a new column for the container
        var columnContainer = new VisualElement
        {
            name = "ColumnContainer"
        };
        columnContainer.AddToClassList("column-container");
        canvasPanel.Add(toolbarPanel);
        canvasPanel.Add(columnContainer);

        var column = CreateColumn();
        columnContainer.Add(column);

        VisualElement CreateColumn() {
            var column = new VisualElement
            {
                name = "Column"
            };
            column.AddToClassList("column");

            // label to give the column a name
            var label = new Label
            {
                text = "Column name"
            };
            label.AddToClassList("label");
            column.Add(label);

            // a scrollable section where the effects are placed
            var scrollView = new ScrollView(ScrollViewMode.Vertical);
            scrollView.AddToClassList("scroll");

            // add the effects into the scroll view
            for (var i = 0; i < 5; ++i) {
                var node = new VisualElement
                {
                    name = "Node"
                };
                node.AddToClassList("node");
                scrollView.Add(node);
            }

            column.Add(scrollView);
            return column;
        }


        // toolbar has the effects that the user can select

        #endregion
    }


    [MenuItem("Voro/Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<ConfigEditor2Window>();
        wnd.titleContent = new GUIContent("Voro Config Editor");
    }
}