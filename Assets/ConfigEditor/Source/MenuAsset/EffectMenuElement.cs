using System;
using System.Collections.Generic;
using Internal;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.MenuAsset {
[UxmlElement]
public partial class EffectMenuElement : VisualElement {
    readonly VisualElement _categoryRoot; // contains all categories
    readonly VisualElement _root; // the root element to contain the menu
    readonly Label _rootLabel; // label that shows what this menu element is

    Dictionary<string, VisualTreeAsset> _uxmlEffectDictionary;

    bool _value;

    public EffectMenuElement() {
        CreateMenuData();

        _rootLabel = new Label("Effect Menu");
        _rootLabel.name = "EffectMenu";
        _rootLabel.AddToClassList("element-label");

        // the container for all items in the menu
        _root = new VisualElement();
        _root.name = "Root";
        _root.AddToClassList("element-root");

        _categoryRoot = new VisualElement();
        _categoryRoot.name = "Category";
        _categoryRoot.AddToClassList("element-category-root");

        _root.Add(_rootLabel);
        _root.Add(_categoryRoot);
        Add(_root);

        // configure EffectMenuElement from MenuData
        // this will produce the collapsible menu elements
        ConfigureFromMenuData();

        // ToDo register mouse clicking to enable menu visibility
        // _node.RegisterCallback<MouseDownEvent>(e => Value = !Value);
    }

    public event Action<VisualTreeAsset> OnMenuButtonClicked;

    void CreateMenuData() {
        // construct the effect dictionary
        _uxmlEffectDictionary = new Dictionary<string, VisualTreeAsset>
        {
            { "EffectMenu/CategoryFoo/Slope", ResourceHelper.LoadEffectUXML("Slope") },
            { "EffectMenu/CategoryFoo/Noise", ResourceHelper.LoadEffectUXML("Noise") },
            { "EffectMenu/CategoryBar/Terrace", ResourceHelper.LoadEffectUXML("Terrace") }
        };
    }

    /// <summary>
    ///     add the elements to produce a collapsible set of options
    /// </summary>
    void ConfigureFromMenuData() {
        // dictionary to keep track of what categories have been added
        var categoryContainers = new Dictionary<string, VisualElement>();

        // for every category, create its contents
        foreach (var kvp in _uxmlEffectDictionary) {
            var pathParts = kvp.Key.Split('/');
            var categoryName = pathParts[1];
            var effectName = pathParts[2];

            // check if the container exists for this category
            if (!categoryContainers.TryGetValue(categoryName, out var categoryContainer)) {
                // the category does not exist, it must be created

                // the category gets its name from the menu path
                categoryContainer = new VisualElement();
                categoryContainer.name = categoryName;
                categoryContainer.AddToClassList("element-category");
                // differentiate each category by setting the background color of each
                categoryContainer.style.backgroundColor = categoryName switch
                {
                    "CategoryFoo" => new Color(153f / 255f, 176f / 255f, 159f / 255f),
                    "CategoryBar" => new Color(176f / 255f, 175f / 255f, 153f / 255f),
                    _ => Color.gray
                };

                // add a label so the user knows what this category is
                var label = new Label(categoryName);
                label.name = "CategoryLabel";
                label.AddToClassList("element-label");
                // add label to container hierarchy
                categoryContainer.Add(label);

                // add this category into the effect menu hierarchy
                _categoryRoot.Add(categoryContainer);
                categoryContainers[categoryName] = categoryContainer; // track this category as existing
            }

            // add the buttons to this category so effects can be created
            var visualTreeAsset = kvp.Value; // the uxml effect associated with this button
            var effectButton = new Button(() => {
                Debug.Log($"clicked: {effectName}");
                OnMenuButtonClicked?.Invoke(visualTreeAsset); // notify what uxml should be created
            });
            effectButton.text = effectName;
            effectButton.name = $"Effect{effectName}Button";
            // add button to category hierarchy
            categoryContainer.Add(effectButton);
        }
    }

    /*[UxmlAttribute]
    public string Text {
        get => _label.text;
        set => _label.text = value;
    }

    [UxmlAttribute]
    public bool Value {
        get => _value;
        set => Set(value);
    }
    public Action<bool> Selected { get; set; }
    void Set(bool value) {
        _value = value;
        Selected?.Invoke(value);
        SetState(value);
    }
    void SetState(bool value) {
        // ToDo toggle visibility of menu via style
        //_node.EnableInClassList("element-node_on", value);
    }*/
}
}