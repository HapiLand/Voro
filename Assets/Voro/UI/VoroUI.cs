using System;
using UnityEngine.UIElements;

namespace Voro.UI {
/// <summary>
///     - This is what the user interacts with.
///     - Produces instructions for terrain generation
/// </summary>
public class VoroUI : VisualElement {
    readonly Button _recomputeButton;

    public VoroUI() {
        _recomputeButton = new Button();
        _recomputeButton.text = "Recompute";
        _recomputeButton.clicked += () => { ClickedRecompute?.Invoke(); };
        Add(_recomputeButton);
    }

    public void Dispose() {
        _recomputeButton.clicked -= () => { ClickedRecompute?.Invoke(); };
    }

    public event Action ClickedRecompute;
}
}