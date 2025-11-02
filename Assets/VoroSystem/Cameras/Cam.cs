using UnityEngine;

namespace VoroSystem.Cameras {
public class Cam {
    /// <summary> Unity Camera in Scene </summary>
    readonly Camera _unityCam = Camera.main;

    public Vector3 WorldToViewportPoint(Vector3 position) {
        return _unityCam.WorldToViewportPoint(position);
    }
}
}