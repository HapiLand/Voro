using UnityEngine;

namespace VoroSystem.Voro.Utilities.Cameras {
public class Cam {
    /// <summary> Unity Camera in Scene </summary>
    readonly Camera _unityCam = Camera.main;

    public Transform CamTransform => _unityCam.transform;

    public Vector3 WorldToViewportPoint(Vector3 position) {
        return _unityCam.WorldToViewportPoint(position);
    }
}
}