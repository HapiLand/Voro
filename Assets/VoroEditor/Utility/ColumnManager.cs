using UnityEngine;
using UnityEngine.UIElements;

namespace VoroEditor.Utility {
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
    public void AddColumn(bool autoSelect = false) {
        // top row is where the label is displayed the column is set to be selected
        var topRow = new VisualElement();
        topRow.AddToClassList("column-top-row");

        var columnName = $"Column_{Random.Range(0, 999)}";
        var column = new VisualElement { name = columnName };
        column.AddToClassList("column");

        // the elements of the column
        var label = new Label(columnName);
        // var selectToggle = new Toggle();
        var deleteButton = new Button(() => RemoveColumn(column)) { text = "X" };
        var scroll = new ScrollView(ScrollViewMode.Vertical);
        scroll.name = "NodeScrollView";
        scroll.AddToClassList("scroll");

        // register the event on when the toolbar is selected by the user
        topRow.RegisterCallback<ClickEvent>(evt => { ColumnSelectEvent(); });

        // add to hierarchy
        column.Add(topRow);

        // topRow.Add(selectToggle);
        topRow.Add(label);
        topRow.Add(deleteButton);

        column.Add(scroll);
        _columnContainer.Add(column);

        // automatically select this column
        if (autoSelect) {
            ColumnSelectEvent();
        }

        return;

        void ColumnSelectEvent() {
            if (_selectedColumn == column) {
                _selectedColumn = null;
                column.RemoveFromClassList("selected-column");
            }
            else {
                SelectColumn(column);
            }
        }
    }


    /// <summary>
    ///     removes the column from the GUI hierarchy
    /// </summary>
    /// <param name="column"></param>
    public void RemoveColumn(VisualElement column) {
        if (_selectedColumn == column) {
            // if the column that is going to be removed
            // is the currently selected column (as multiple may exist)
            // set the selection to null as it will not exist after this method
            _selectedColumn = null;
        }

        column.RemoveFromHierarchy();
    }

    /// <summary>
    ///     marks the column as selected, removing the selection of all other columns
    /// </summary>
    /// <param name="column"></param>
    public void SelectColumn(VisualElement column) {
        if (_selectedColumn != null && _selectedColumn != column) {
            _selectedColumn.Q<Toggle>()?.SetValueWithoutNotify(false);
            _selectedColumn.RemoveFromClassList("selected-column");
        }

        _selectedColumn = column;
        _selectedColumn.AddToClassList("selected-column");
    }

    /// <summary>
    ///     the user selected an effect from the toolbar
    ///     this effect is now being added to the selected column
    ///     a new visual element for a node will be created
    /// </summary>
    /// <param name="effectName"></param>
    public void AddEffectToSelectedColumn(string effectName) {
        if (_selectedColumn == null) {
            Debug.LogWarning("no column selected");
            return;
        }

        // find the scroll view, this is the vertical section that the nodes exist inside
        var scroll = _selectedColumn.Q<ScrollView>();
        if (scroll == null) {
            return;
        }

        // the node factory will create the node that is added to the column
        var node = _factory.Create(effectName);
        scroll.Add(node);
    }
}
}