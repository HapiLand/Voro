using UnityEngine;
using UnityEngine.UIElements;
using VoroUI.EditorTabs.Base;

namespace VoroUI.EditorTabs {
public class CameraTab : WindowTab {
    /// <summary>
    ///     element that shows the camera output
    /// </summary>
    readonly VisualElement _cameraDisplay;

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
    }
}
}