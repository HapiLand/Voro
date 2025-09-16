using System;
using System.Collections.Generic;
using System.Linq;
using EditorGUI.Source.Utility;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace OLD.VoroEditor.GUI {
public class ToolbarManager {
    readonly IMGUIContainer _container;

    public ToolbarManager(VisualElement root) {
        _container = root.Q<IMGUIContainer>("Toolbar");
    }

    public void BuildToolbar() {
        var toolbar = new Toolbar(); // ToDo CustomToolbar : VisualElement
        _container.Add(toolbar);

        // create each button
        var refreshButton = new Button { name = "Refresh", text = "Refresh Scene" };
        refreshButton.clicked += OnClick_Refresh;
        var createLayerButton = new Button { name = "CreateLayer", text = "Create Layer" };
        createLayerButton.clicked += () => OnClick_CreateLayer("foobar");
        var computeButton = new Button { name = "Compute", text = "Compute Terrain" };
        computeButton.clicked += OnClick_Compute;

        // add to toolbar
        toolbar.Add(refreshButton);
        toolbar.Add(createLayerButton);
        toolbar.Add(computeButton);

        CreateEffectMenu();
        return;

        void CreateEffectMenu() {
            var menuPaths = CreateEffectDictionary();

            // generate a toolbar menu for each effect category
            var menus = new Dictionary<string, ToolbarMenu>();
            foreach (var kvp in menuPaths) {
                // get the menu path data and the effect instance
                var pathParts = kvp.Key.Split('/');
                var effectCategory = pathParts[0];
                var effectName = pathParts[1];
                var effectInstance = kvp.Value;

                // create each category if it does not already exist
                if (!menus.TryGetValue(effectCategory, out var toolbarMenu)) {
                    toolbarMenu = new ToolbarMenu { text = effectCategory };
                    toolbar.Add(toolbarMenu);
                    menus[effectCategory] = toolbarMenu; // prevent duplicate categories
                }

                // append the effects to the toolbar, register when selected
                toolbarMenu.menu.AppendAction(effectInstance, _ => OnClick_AddEffect(effectInstance));
            }

            return;

            Dictionary<string, string> CreateEffectDictionary() {
                var effects = new[]
                {
                    EffectMaster.Slope,
                    EffectMaster.SetHeight,
                    EffectMaster.Noise,
                    EffectMaster.Terrace,
                    EffectMaster.SetTag
                };
                var menuPaths = effects.Select(AssetHelper.GetEffectMenuPath)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
                return menuPaths;
            }
        }
    }

    public static event Action OnRefresh;
    public static event Action<string> OnCreateLayer;
    public static event Action OnCompute;
    public static event Action<string> OnAddEffect;

    void OnClick_Refresh() {
        OnRefresh?.Invoke();
    }

    void OnClick_CreateLayer(string name) {
        OnCreateLayer?.Invoke(name);
    }

    void OnClick_Compute() {
        OnCompute?.Invoke();
    }

    void OnClick_AddEffect(string effectInstance) {
        OnAddEffect?.Invoke(effectInstance);
    }

    void AddEffectToLayer(string effectInstance) {
        Debug.Log($"adding {effectInstance} to active layer");
        // ToDo add effect to active layer
    }
}
}