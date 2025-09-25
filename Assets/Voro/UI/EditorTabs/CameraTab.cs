using System;
using UnityEngine;
using UnityEngine.UIElements;
using Voro.UI.EditorTabs.Base;

namespace Voro.UI.EditorTabs {
public class CameraTab : WindowTab {
    /// <summary>
    ///     element that shows the camera output
    /// </summary>
    readonly VisualElement _cameraDisplay;

    readonly Button _recomputeButton;

    public CameraTab() {
        style.flexDirection = FlexDirection.Column; // vertical layout
        style.flexGrow = 1; // full size
        // heading
        TabHeading = new Label("Camera View");
        Add(TabHeading);

        // create the display element to view the camera
        _cameraDisplay = new VisualElement();
        Add(_cameraDisplay);
        // convert the resource to a texture so it can be used as the background of the element
        var texture = Resources.Load<RenderTexture>("main_cam");
        if (texture != null) {
            _cameraDisplay.style.backgroundImage = new StyleBackground(Background.FromRenderTexture(texture));
            _cameraDisplay.style.flexGrow = 1;
        }
        else {
            Debug.Log("main_cam texture not found");
        }

        _recomputeButton = new Button();
        _recomputeButton.text = "Recompute";
        _recomputeButton.clicked += () => { ClickedRecompute?.Invoke(); };
        Add(_recomputeButton);
    }
    
    public event Action ClickedRecompute;

    public void Dispose() {
        _recomputeButton.clicked -= () => { ClickedRecompute?.Invoke(); };
    }
}
}