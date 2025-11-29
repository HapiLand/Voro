namespace VoroSystem.Util.Cameras {
public static class CameraManager {
    static Cam _camera;
    public static Cam Camera => _camera ??= new Cam();
}
}