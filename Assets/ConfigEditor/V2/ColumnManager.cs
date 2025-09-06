using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
/// <summary>
///     creates/removes and tracks every column in the editor
/// </summary>
public class ColumnManager {
    readonly VisualElement _columnContainer;
    readonly NodeFactory _factory;
    VisualElement _selectedColumn;

    public ColumnManager(VisualElement container, NodeFactory factory) {
        _columnContainer = container;
        _factory = factory;
    }

    /// <summary>
    ///     on a toolbar callback, the new column is created and can store nodes
    /// </summary>
    public void AddColumn() {
        // ToDo the column is written into MyConfig.json
        var columnName = $"Column_{Random.Range(0, 9999)}";
        var column = new VisualElement { name = columnName };
        column.AddToClassList("column");

        // the elements of the column
        var label = new Label(columnName);
        var selectToggle = new Toggle("Select");
        var deleteButton = new Button(() => RemoveColumn(column)) { text = "X" };
        var scroll = new ScrollView(ScrollViewMode.Vertical);
        scroll.AddToClassList("scroll");

        // register the event on when the toolbar is selected by the user
        selectToggle.RegisterValueChangedCallback(evt => {
            if (evt.newValue) {
                SelectColumn(column);
            }
            else if (_selectedColumn == column) {
                _selectedColumn = null;
            }
        });

        // add to hierarchy
        column.Add(label);
        column.Add(selectToggle);
        column.Add(deleteButton);
        column.Add(scroll);
        _columnContainer.Add(column);
    }

    /// <summary>
    ///     removes the column from the GUI hierarchy
    /// </summary>
    /// <param name="column"></param>
    public void RemoveColumn(VisualElement column) {
        if (_selectedColumn == column) {
            _selectedColumn = null;
        }

        column.RemoveFromHierarchy();
    }

    /// <summary>
    ///     marks the column as selected, removing the selection of all other columns
    /// </summary>
    /// <param name="column"></param>
    public void SelectColumn(VisualElement column) {
        // ToDo repeat this with Nodes to deselect other nodes
        if (_selectedColumn != null && _selectedColumn != column) {
            _selectedColumn.Q<Toggle>()?.SetValueWithoutNotify(false);
            _selectedColumn.RemoveFromClassList("selected-column");
        }

        _selectedColumn = column;
        _selectedColumn.AddToClassList("selected-column");
    }

    /// <summary>
    ///     adds a new effect into the column
    /// </summary>
    /// <param name="effectName"></param>
    public void AddEffectToSelectedColumn(string effectName) {
        if (_selectedColumn == null) {
            Debug.LogWarning("no column selected");
            return;
        }

        var scroll = _selectedColumn.Q<ScrollView>();
        if (scroll == null) {
            return;
        }

        var node = _factory.Create(effectName);
        scroll.Add(node);
    }
}
}