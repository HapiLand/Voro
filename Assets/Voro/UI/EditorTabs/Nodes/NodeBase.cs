using System;

namespace Voro.UI.EditorTabs.Nodes {
public abstract class NodeBase {
    /// <summary>
    ///     called any time a data value is changed
    /// </summary>
    public event Action OnUpdated;

    protected void OnValueChanged() {
        OnUpdated?.Invoke();
    }
}
}