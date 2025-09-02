using UnityEngine;

namespace UnityComponents {
public class PlayerLocation : MonoBehaviour {
    static PlayerLocation _instance;

    GameObject _playerObject;
    Transform _playerTransform;

    public static GameObject PlayerObject => _instance?._playerObject;
    public static Transform PlayerTransform => _instance?._playerTransform;

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // to keep this object between scenes
        // DontDestroyOnLoad(gameObject);

        LocatePlayer();
    }

    void LocatePlayer() {
        _playerObject = GameObject.FindGameObjectWithTag("Player");

        if (_playerObject != null) {
            _playerTransform = _playerObject.transform;
        }
        else {
            Debug.Log("nothing with the 'Player' tag found");
        }
    }
}
}