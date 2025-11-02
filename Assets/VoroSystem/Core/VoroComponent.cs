using UnityEngine;

namespace VoroSystem.Core {
[ExecuteAlways]
public class VoroComponent : MonoBehaviour {
    [SerializeField] VoroInput voroInput;
    Voro _voro;
    public static Transform Instance { get; private set; }

    void Awake() {
        Instance = transform;
        voroInput = new VoroInput(5, 5, 0.2f);
        _voro = new Voro(voroInput);
        _voro.Init();
    }

    void Start() {
        _voro.CreateLandscape();
    }
}
}