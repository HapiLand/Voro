using UnityEditor;
using UnityEngine;

namespace Voro.UserInterface.Runtime {

[InitializeOnLoad]
public class UserInterfaceInitializer {
  static void Init() {
    Debug.Log("Loaded Voro package");
    EditorApplication.delayCall += () => {
      Debug.Log("Running setup");
    };
  }
}



}