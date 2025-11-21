using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Voro.Designer.Canvas {
[CreateAssetMenu(fileName = "CanvasTemplate", menuName = "Voro/Canvas Template")]
public class CanvasTemplate : ScriptableObject {
    public string graphName;
    public List<Layer> layers = new List<Layer>();
}
}
