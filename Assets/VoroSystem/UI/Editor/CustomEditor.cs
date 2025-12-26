using System;

namespace VoroSystem.UI.Editor {
[Serializable]
public abstract class CustomEditor : IDisposable {
  #region IDisposable Members
  public void Dispose() {
    // TODO release managed resources here
  }
  #endregion
}
}